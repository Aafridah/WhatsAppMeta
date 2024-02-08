using Newtonsoft.Json;
using System.Dynamic;
using WhatsAppMeta.Enums;
using WhatsAppMeta.Helper;
using WhatsAppMeta.Interfaces;
using WhatsAppMeta.Models;

namespace WhatsAppMeta;
public class WhatsApp
{
    #region Template

    public static async Task<WhatsAppCreateTemplateResponseModel> CreateNewTemplate(CancellationToken cancellationToken, IUnitOfWork unitOfWork, WhatsAppCreateTemplateRequestModel requestModel, short userId, int companyId, string version)
    {
        var customerWhatsAppCredentials = await unitOfWork.WhatsAppRepository.GetCustomerWhatsAppCredentialsByCompanyId<CustomerWhatsAppCredentials>(cancellationToken, companyId);

        var json = JsonConvert.SerializeObject(requestModel);

        var HttpCallerModel = new HttpCallerModel<string>
        {
            Url = $"https://graph.facebook.com/{version}/{customerWhatsAppCredentials.WabaId}/message_templates",
            Method = HttpRequestType.Post,
            ContentType = ContentType.json,
            Body = json,
            TimeOut = 20,
            AuthenticationType = HttpAuthenticationType.Bearer,
            AuthKey = customerWhatsAppCredentials.FbApiKey
        };



        var restResponse = await HttpCaller<string>.Request(unitOfWork, userId, HttpCallerModel, cancellationToken);

        if (restResponse.IsSuccessful)
        {
            if (restResponse.Content != null)
            {

                var responseModel = JsonConvert.DeserializeObject<WhatsAppCreateTemplateResponseModel>(restResponse.Content);

                return responseModel;
            }
        }

        return default;
    }

    public static async Task<CommonWhatsAppResponseModel> EditTemplate(CancellationToken cancellationToken, IUnitOfWork unitOfWork, WhatsAppCreateTemplateRequestModel requestModel, short userId, int companyId, string version, string templateId)
    {
        var customerWhatsAppCredentials = await unitOfWork.WhatsAppRepository.GetCustomerWhatsAppCredentialsByCompanyId<CustomerWhatsAppCredentials>(cancellationToken, companyId);

        var json = JsonConvert.SerializeObject(requestModel);

        var HttpCallerModel = new HttpCallerModel<string>
        {
            Url = $"https://graph.facebook.com/{version}/{templateId}",
            Method = HttpRequestType.Post,
            ContentType = ContentType.json,
            Body = json,
            TimeOut = 20,
            AuthenticationType = HttpAuthenticationType.Bearer,
            AuthKey = customerWhatsAppCredentials.FbApiKey
        };

        var restResponse = await HttpCaller<string>.Request(unitOfWork, userId, HttpCallerModel, cancellationToken);

        if (restResponse.IsSuccessful)
        {
            if (restResponse.Content != null)
            {

                var responseModel = JsonConvert.DeserializeObject<CommonWhatsAppResponseModel>(restResponse.Content);

                return responseModel;
            }
        }

        return default;
    }

    public static async Task<WhatsAppCreateTemplateRequestModel> GetTemplateById(CancellationToken cancellationToken, IUnitOfWork unitOfWork, short userId, int companyId, string version, string templateId)
    {

        var customerWhatsAppCredentials = await unitOfWork.WhatsAppRepository.GetCustomerWhatsAppCredentialsByCompanyId<CustomerWhatsAppCredentials>(cancellationToken, companyId);

        var HttpCallerModel = new HttpCallerModel<WhatsAppCreateTemplateRequestModel>
        {
            Url = $"https://graph.facebook.com/{version}/{templateId}",
            Method = HttpRequestType.Get,
            TimeOut = 20,
            AuthenticationType = HttpAuthenticationType.Bearer,
            AuthKey = customerWhatsAppCredentials.FbApiKey
        };
        //947984580025547
        var restResponse = await HttpCaller<WhatsAppCreateTemplateRequestModel>.Request(unitOfWork, userId, HttpCallerModel, cancellationToken);

        if (restResponse.IsSuccessful)
        {
            if (restResponse.Content != null)
            {

                var responseModel = JsonConvert.DeserializeObject<WhatsAppCreateTemplateRequestModel>(restResponse.Content);

                return responseModel;
            }
        }

        return default;
    }

    public static async Task<IEnumerable<WhatsAppCreateTemplateRequestModel>> GetAllTemplates(CancellationToken cancellationToken, IUnitOfWork unitOfWork, short userId, int companyId, string version)
    {

        var customerWhatsAppCredentials = await unitOfWork.WhatsAppRepository.GetCustomerWhatsAppCredentialsByCompanyId<CustomerWhatsAppCredentials>(cancellationToken, companyId);

        var HttpCallerModel = new HttpCallerModel<WhatsAppCreateTemplateRequestModel>
        {
            Url = $"https://graph.facebook.com/{version}/{customerWhatsAppCredentials.WabaId}/message_templates",
            Method = HttpRequestType.Get,
            TimeOut = 20,
            AuthenticationType = HttpAuthenticationType.Bearer,
            AuthKey = customerWhatsAppCredentials.FbApiKey
        };

        var restResponse = await HttpCaller<WhatsAppCreateTemplateRequestModel>.Request(unitOfWork, userId, HttpCallerModel, cancellationToken);

        if (restResponse.IsSuccessful)
        {
            if (restResponse.Content != null)
            {

                var responseModel = JsonConvert.DeserializeObject<GetAllTemplatesResponseModel>(restResponse.Content);

                return responseModel.Data;
            }
        }

        return default;
    }

    public static async Task<CommonWhatsAppResponseModel> DeleteTemplate(CancellationToken cancellationToken, IUnitOfWork unitOfWork, WhatsAppCreateTemplateRequestModel requestModel, short userId, int companyId, string version, string templateId, string templateName)
    {

        var customerWhatsAppCredentials = await unitOfWork.WhatsAppRepository.GetCustomerWhatsAppCredentialsByCompanyId<CustomerWhatsAppCredentials>(cancellationToken, companyId);

        var json = JsonConvert.SerializeObject(requestModel);

        var HttpCallerModel = new HttpCallerModel<string>
        {
            Url = $"https://graph.facebook.com/{version}/{customerWhatsAppCredentials.WabaId}/message_templates?hsm_id={templateId}&name={templateName}",
            Method = HttpRequestType.Delete,
            TimeOut = 20,
            AuthenticationType = HttpAuthenticationType.Bearer,
            AuthKey = customerWhatsAppCredentials.FbApiKey,

        };

        var restResponse = await HttpCaller<string>.Request(unitOfWork, userId, HttpCallerModel, cancellationToken);

        if (restResponse.IsSuccessful)
        {
            if (restResponse.Content != null)
            {

                var responseModel = JsonConvert.DeserializeObject<CommonWhatsAppResponseModel>(restResponse.Content);

                return responseModel;
            }
        }

        return default;
    }

    public static async Task<string> CreateHeaderHandle(CancellationToken cancellationToken, IUnitOfWork unitOfWork, int companyId, short userId, string version, IFormFile file)
    {
        var customerWhatsAppCredentials = await unitOfWork.WhatsAppRepository.GetCustomerWhatsAppCredentialsByCompanyId<CustomerWhatsAppCredentials>(cancellationToken, companyId);

        //create session
        var HttpCallerModel = new HttpCallerModel<string>
        {
            Url = $"https://graph.facebook.com/{version}/{customerWhatsAppCredentials.AppId}/uploads?file_length={file.Length}&file_type={file.ContentType}",
            Method = HttpRequestType.Post,
            TimeOut = 20,
            AuthenticationType = HttpAuthenticationType.Bearer,
            AuthKey = customerWhatsAppCredentials.FbApiKey,

        };

        var restResponse = await HttpCaller<string>.Request(unitOfWork, userId, HttpCallerModel, cancellationToken);

        if (!restResponse.IsSuccessful)
        {
            return default;
        }

        if (restResponse.Content == null)
        {
            return default;
        }

        var sessionId = JsonConvert.DeserializeObject<MediaIdResponseModel>(restResponse.Content).Id;

        //initiate upload
        byte[] actualFile;
        await using (var target = new MemoryStream())
        {
            await file.CopyToAsync(target, cancellationToken);
            actualFile = target.ToArray();
        }

        var HttpCallerModelFileUpload = new HttpCallerModel<byte[]>
        {
            Url = $"https://graph.facebook.com/{version}/{sessionId}",
            Method = HttpRequestType.Post,
            TimeOut = 20,
            ContentType = ContentType.multipartForm,
            Body = actualFile,
            AuthenticationType = HttpAuthenticationType.OAuth,
            AuthKey = customerWhatsAppCredentials.FbApiKey,

        };

        var initiateUploadResponse = await HttpCaller<byte[]>.Request(unitOfWork, userId, HttpCallerModelFileUpload, cancellationToken);
        if (initiateUploadResponse.IsSuccessful)
        {
            if (initiateUploadResponse.Content != null)
            {
                var initiateUploadResponseModel = JsonConvert.DeserializeObject<InitiateUploadResponseModel>(initiateUploadResponse.Content);
                return initiateUploadResponseModel.HeaderHandle;
            }
        }

        return default;
    }

    #endregion

    #region SendSms
    public static async Task<string> UploadMedia(CancellationToken cancellationToken, IUnitOfWork unitOfWork, short userId, int companyId, string version, IFormFile file)
    {
        var customerWhatsAppCredentials = await unitOfWork.WhatsAppRepository.GetCustomerWhatsAppCredentialsByCompanyId<CustomerWhatsAppCredentials>(cancellationToken, companyId);

        var formData = new
        {
            messaging_product = "whatsapp",
            file = file,
        };

        var HttpCallerModel = new HttpCallerModel<object>
        {
            Url = $"https://graph.facebook.com/{version}/{customerWhatsAppCredentials.PhoneNumberId}/media",
            Method = HttpRequestType.Post,
            ContentType = ContentType.json,
            Body = formData,
            TimeOut = 20,
            AuthenticationType = HttpAuthenticationType.Bearer,
            AuthKey = customerWhatsAppCredentials.FbApiKey
        };

        var restResponse = await HttpCaller<object>.Request(unitOfWork, userId, HttpCallerModel, cancellationToken);

        if (restResponse.IsSuccessful)
        {
            if (restResponse.Content != null)
            {

                var responseModel = JsonConvert.DeserializeObject<MediaIdResponseModel>(restResponse.Content);

                return responseModel.Id;
            }
        }

        return default;
    }

    public static async Task<WhatsAppSendMessageResponseModel> SendSms<T>(CancellationToken cancellationToken, IUnitOfWork unitOfWork, WhatsAppSendMessageRequestModel requestModel, T typeViewModel, short userId, int companyId, string version)
    {
        try
        {
            var customerWhatsAppCredentials = await unitOfWork.WhatsAppRepository.GetCustomerWhatsAppCredentialsByCompanyId<CustomerWhatsAppCredentials>(cancellationToken, companyId);

            dynamic sendSmsBody = new ExpandoObject();

            sendSmsBody.messaging_product = requestModel.MessagingProduct;
            sendSmsBody.recipient_type = requestModel.RecipientType;
            sendSmsBody.to = requestModel.To;
            sendSmsBody.type = EnumHelper.GetDescription<WhatsAppSendMessageType>(requestModel.Type.ToString());


            switch (requestModel.Type)
            {
                case WhatsAppSendMessageType.Text:
                    sendSmsBody.text = typeViewModel;
                    break;
                case WhatsAppSendMessageType.Image:
                    sendSmsBody.image = typeViewModel;
                    break;
                case WhatsAppSendMessageType.Audio:
                    sendSmsBody.audio = typeViewModel;
                    break;
                case WhatsAppSendMessageType.Document:
                    sendSmsBody.document = typeViewModel;
                    break;
                case WhatsAppSendMessageType.Sticker:
                    sendSmsBody.sticker = typeViewModel;
                    break;
                case WhatsAppSendMessageType.Video:
                    sendSmsBody.video = typeViewModel;
                    break;
                case WhatsAppSendMessageType.Contacts:
                    sendSmsBody.contacts = typeViewModel;
                    break;
                case WhatsAppSendMessageType.Location:
                    sendSmsBody.location = typeViewModel;
                    break;
                case WhatsAppSendMessageType.Template:
                    sendSmsBody.template = typeViewModel;
                    break;
                case WhatsAppSendMessageType.Interactive:
                    sendSmsBody.interactive = typeViewModel;
                    break;
                default:
                    sendSmsBody.template = typeViewModel;
                    break;
            }

            var json = JsonConvert.SerializeObject(sendSmsBody);

            var HttpCallerModel = new HttpCallerModel<string>
            {
                Url = $"https://graph.facebook.com/{version}/{customerWhatsAppCredentials.PhoneNumberId}/messages",
                Method = HttpRequestType.Post,
                ContentType = ContentType.json,
                Body = json,
                TimeOut = 20,
                AuthenticationType = HttpAuthenticationType.Bearer,
                AuthKey = customerWhatsAppCredentials.FbApiKey
            };

            var restResponse = await HttpCaller<string>.Request(unitOfWork, userId, HttpCallerModel, cancellationToken);

            if (restResponse.IsSuccessful)
            {
                if (restResponse.Content != null)
                {

                    var responseModel =
                        JsonConvert.DeserializeObject<WhatsAppSendMessageResponseModel>(restResponse.Content);

                    return responseModel;
                }
            }

            return default;
        }
        catch (Exception)
        {
            return default;
        }
    }



    #endregion
}

