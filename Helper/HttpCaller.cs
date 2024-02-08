using System.Net;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using RestSharp;
using RestSharp.Authenticators;
using WhatsAppMeta.Enums;
using WhatsAppMeta.Interfaces;
using WhatsAppMeta.Models;
using ContentType = WhatsAppMeta.Enums.ContentType;

namespace WhatsAppMeta.Helper;
public static class HttpCaller<T> where T : class
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly AsyncRetryPolicy<RestResponse> RetryPolicy =
        Policy<RestResponse>
            .Handle<HttpRequestException>()
            .Or<TimeoutException>()
            .OrResult(x => x.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3));

    public static async Task<RestResponse> Request(IUnitOfWork unitOfWork, short userId, HttpCallerModel<T> httpCallerModel, CancellationToken cancellationToken)
    {
        RestClientOptions restClientOptions;

        if (httpCallerModel.AuthenticationType is HttpAuthenticationType.Basic)
        {
            restClientOptions = new RestClientOptions(httpCallerModel.Url)
            {
                ThrowOnAnyError = true,
                MaxTimeout = httpCallerModel.TimeOut * 1000,
                Authenticator = new HttpBasicAuthenticator(httpCallerModel.AuthKey, httpCallerModel.AuthValue)
            };
        }
        else
        {
            restClientOptions = new RestClientOptions(httpCallerModel.Url)
            {
                ThrowOnAnyError = true,
                MaxTimeout = httpCallerModel.TimeOut * 1000
            };
        }

        if (httpCallerModel.RemoteCertificateValidationCallback)
            restClientOptions.RemoteCertificateValidationCallback = (_, _, _, _) => true;

        var client = new RestClient(restClientOptions);
        var response = new RestResponse();
        var request = new RestRequest();

        try
        {
            if (httpCallerModel.ContentType != null)
            {
                var contentType = EnumHelper.GetDescription<ContentType>(httpCallerModel.ContentType.ToString());
                request.AddHeader("Content-Type", contentType);

                switch (httpCallerModel.ContentType)
                {
                    case ContentType.json:
                        request.AddJsonBody(httpCallerModel.Body);
                        break;
                    case ContentType.form:
                        request.AddParameter("application/x-www-form-urlencoded", httpCallerModel.Body, ParameterType.RequestBody);
                        break;
                    case ContentType.xml:
                        request.AddXmlBody(httpCallerModel.Body);
                        break;
                    case ContentType.multipartForm:
                        request.AddParameter("multipart/form-data", httpCallerModel.Body, ParameterType.RequestBody);
                        break;
                }
            }

            if (httpCallerModel.Headers != null)
            {
                foreach (var httpHeaders in httpCallerModel.Headers)
                {
                    request.AddHeader(httpHeaders.Key, httpHeaders.Value);
                }
            }

            if (httpCallerModel.AuthenticationType != null)
            {
                switch (httpCallerModel.AuthenticationType)
                {
                    case HttpAuthenticationType.Api:
                        request.AddHeader(httpCallerModel.AuthKey, httpCallerModel.AuthValue);
                        break;
                    case HttpAuthenticationType.Bearer:
                        request.AddHeader("Authorization", $"Bearer {httpCallerModel.AuthKey}");
                        break;
                    case HttpAuthenticationType.OAuth:
                        request.AddHeader("Authorization", $"OAuth {httpCallerModel.AuthKey}");
                        break;
                }
            }


            response = httpCallerModel.Method switch
            {
                HttpRequestType.Get => await RetryPolicy
                    .ExecuteAsync(() => client.GetAsync(request, cancellationToken)).ConfigureAwait(false),
                HttpRequestType.Post => await RetryPolicy
                    .ExecuteAsync(() => client.PostAsync(request, cancellationToken)).ConfigureAwait(false),
                HttpRequestType.Delete => await RetryPolicy
                    .ExecuteAsync(() => client.DeleteAsync(request, cancellationToken)).ConfigureAwait(false),
                _ => response
            };


        }
        catch (Exception ex)
        {
           //
            throw;
        }

        return response;
    }
}