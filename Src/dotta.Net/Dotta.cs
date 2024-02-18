using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace dotta.Net
{
    public class Dotta
    {
        private readonly HttpClient _httpClient;
        private readonly string[] _allowFileExtensions = { ".png", ".jpeg", ".jpg" };

        /// <summary>
        /// To instantiate, register the service in the program.cs and pass the arguments. All parameters are required.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="environment"></param>
        /// <param name="httpClient"></param>
        public Dotta(string apiKey, DottaEnvironment environment, HttpClient httpClient)
        {
            ApiKey = apiKey;
            Environment = environment;
            _httpClient = httpClient;
        }

        /// <summary>
        /// To instantiate, register the service in the program.cs and pass the arguments. All parameters are required.
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="privateKey"></param>
        /// <param name="environment"></param>
        /// <param name="httpClient"></param>
        public Dotta(string publicKey, string privateKey, DottaEnvironment environment, HttpClient httpClient)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"{publicKey}:{privateKey}");
            var base64String = Convert.ToBase64String(plainTextBytes);

            ApiKey = base64String;
            Environment = environment;
            _httpClient = httpClient;
        }

        public string ApiKey { get; }
        public DottaEnvironment Environment { get; }
        public string BaseUrlProduction { get; } = "https://apps.securedrecords.com/dotta-biometrics/api/";
        public string BaseUrlSandbox { get; } = "https://apps.securedrecords.com/DevDottaBiometrics/api/";

        /// <summary>
        /// Makes network request to dotta api and return facial attributes
        /// </summary>
        /// <param name="photo"></param>
        public async Task<DottaResponse<FaceAttributesResponse>> FaceAttributes(IFormFile photo)
        {
            var faceAttributesResponse = new DottaResponse<FaceAttributesResponse>();

            try
            {
                // check if photo file is empty
                if (photo is null)
                    return new DottaResponse<FaceAttributesResponse>
                    {
                        Status = false,
                        Message = "Photo with a face is required"
                    };

                // check for allow photo file extension
                var photoExtension = Path.GetExtension(photo.FileName);
                if (!_allowFileExtensions.Contains(photoExtension))
                    return new DottaResponse<FaceAttributesResponse>
                    {
                        Status = false,
                        Message = $"File extension not allowed. Allowed extensions are {string.Join(" ", _allowFileExtensions)}"
                    };

                // get photo mime type. This is needed when making multipart http request
                var photoMimeType = GetPhotoMimeType(photoExtension);
                if (string.IsNullOrEmpty(photoMimeType))
                    return new DottaResponse<FaceAttributesResponse>
                    {
                        Status = false,
                        Message = "Photo has an invalid mimetype"
                    };

                // build content for multipart http request
                var imageContent = new StreamContent(photo.OpenReadStream());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(photoMimeType);

                var formData = new MultipartFormDataContent
                {
                    { imageContent, "Photo", photo.FileName }
                };

                // make network request and serialize response
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ApiKey);
                var httpResponse = await _httpClient.PostAsync($"{(Environment == DottaEnvironment.Production ? BaseUrlProduction : BaseUrlSandbox)}Face/Attributes", formData);
                var response = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseDTO = JsonSerializer.Deserialize<HttpDottaFaceAttributesResponse>(response);
                    faceAttributesResponse = new DottaResponse<FaceAttributesResponse>
                    {
                        Status = responseDTO.status,
                        Message = responseDTO.message,
                        Data = new FaceAttributesResponse
                        {
                            Age = responseDTO.data.age,
                            ErrorCode = responseDTO.data.errorCode,
                            ErrorMessage = responseDTO.data.errorMessage,
                            EyesOpen = responseDTO.data.eyesOpen,
                            Female = responseDTO.data.female,
                            Male = responseDTO.data.male,
                            Orientation = responseDTO.data.orientation,
                            PassiveLiveness = responseDTO.data.passiveLiveness,
                            Smile = responseDTO.data.smile
                        }
                    };
                }
                else
                {
                    var errorResponse = JsonSerializer.Deserialize<DottaResponse>(response);
                    faceAttributesResponse.Status = errorResponse.Status;
                    faceAttributesResponse.Message = errorResponse.Message;
                }

                imageContent.Dispose();
                formData.Dispose();

                return faceAttributesResponse;
            }
            catch (Exception ex)
            {
                faceAttributesResponse.Status = false;
                faceAttributesResponse.Message = ex.Message;

                return faceAttributesResponse;
            }
        }

        /// <summary>
        /// Makes network request to dotta api and return face position paramters
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public async Task<DottaResponse<FaceDetectResponse>> FaceDetection(IFormFile photo)
        {
            var faceDetectResponse = new DottaResponse<FaceDetectResponse>();

            try
            {
                // check if photo file is empty
                if (photo is null)
                    return new DottaResponse<FaceDetectResponse>
                    {
                        Status = false,
                        Message = "Photo with a face is required"
                    };

                // check for allow photo file extension
                var photoExtension = Path.GetExtension(photo.FileName);
                if (!_allowFileExtensions.Contains(photoExtension))
                    return new DottaResponse<FaceDetectResponse>
                    {
                        Status = false,
                        Message = $"File extension not allowed. Allowed extensions are {string.Join(" ", _allowFileExtensions)}"
                    };

                // get photo mime type. This is needed when making multipart http request
                var photoMimeType = GetPhotoMimeType(photoExtension);
                if (string.IsNullOrEmpty(photoMimeType))
                    return new DottaResponse<FaceDetectResponse>
                    {
                        Status = false,
                        Message = "Photo has an invalid mimetype"
                    };

                // build content for multipart http request
                var imageContent = new StreamContent(photo.OpenReadStream());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(photoMimeType);

                var formData = new MultipartFormDataContent
                {
                    { imageContent, "Photo", photo.FileName }
                };

                // make network request and serialize response
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ApiKey);
                var httpResponse = await _httpClient.PostAsync($"{(Environment == DottaEnvironment.Production ? BaseUrlProduction : BaseUrlSandbox)}Face/Detect", formData);
                var response = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseDTO = JsonSerializer.Deserialize<HttpDottaFaceDetectResponse>(response);
                    faceDetectResponse = new DottaResponse<FaceDetectResponse>
                    {
                        Status = responseDTO.status,
                        Message = responseDTO.message,
                        Data = new FaceDetectResponse
                        {
                            Angle = responseDTO.data.angle,
                            ErrorCode = responseDTO.data.errorCode,
                            ErrorMessage = responseDTO.data.errorMessage,
                            Orientation = responseDTO.data.orientation,
                            Padding = responseDTO.data.padding,
                            PointX = responseDTO.data.pointX,
                            PointY = responseDTO.data.pointY,
                            Width = responseDTO.data.width
                        }
                    };
                }
                else
                {
                    var errorResponse = JsonSerializer.Deserialize<DottaResponse>(response);
                    faceDetectResponse.Status = errorResponse.Status;
                    faceDetectResponse.Message = errorResponse.Message;
                }

                imageContent.Dispose();
                formData.Dispose();

                return faceDetectResponse;
            }
            catch (Exception ex)
            {
                faceDetectResponse.Status = false;
                faceDetectResponse.Message = ex.Message;

                return faceDetectResponse;
            }
        }

        /// <summary>
        /// Makes network request to dotta api to compare the two photos and return similarity score
        /// </summary>
        /// <param name="photoOne"></param>
        /// <param name="photoTwo"></param>
        /// <returns></returns>
        public async Task<DottaResponse<FaceMatchResponse>> FaceMatch(IFormFile photoOne, IFormFile photoTwo)
        {
            var faceMatchResponse = new DottaResponse<FaceMatchResponse>();

            try
            {
                // check if photo files is empty
                if (photoOne is null || photoTwo is null)
                    return new DottaResponse<FaceMatchResponse>
                    {
                        Status = false,
                        Message = "Reference photo and probe photo with a face is required"
                    };

                // check for allow photo files extension
                var photoOneExtension = Path.GetExtension(photoOne.FileName);
                var photoTwoExtension = Path.GetExtension(photoTwo.FileName);
                if (!_allowFileExtensions.Contains(photoOneExtension) || !_allowFileExtensions.Contains(photoTwoExtension))
                    return new DottaResponse<FaceMatchResponse>
                    {
                        Status = false,
                        Message = $"File extension not allowed. Allowed extensions are {string.Join(" ", _allowFileExtensions)}"
                    };

                // get photos mime type. This is needed when making multipart http request
                var photoOneMimeType = GetPhotoMimeType(photoOneExtension);
                var photoTwoMimeType = GetPhotoMimeType(photoTwoExtension);
                if (string.IsNullOrEmpty(photoOneMimeType) || string.IsNullOrEmpty(photoTwoMimeType))
                    return new DottaResponse<FaceMatchResponse>
                    {
                        Status = false,
                        Message = "Photo has an invalid mimetype"
                    };

                // build content for multipart http request
                var imageOneContent = new StreamContent(photoOne.OpenReadStream());
                imageOneContent.Headers.ContentType = MediaTypeHeaderValue.Parse(photoOneMimeType);
                
                var imageTwoContent = new StreamContent(photoTwo.OpenReadStream());
                imageTwoContent.Headers.ContentType = MediaTypeHeaderValue.Parse(photoTwoMimeType);

                var formData = new MultipartFormDataContent
                {
                    { imageOneContent, "PhotoOne", photoOne.FileName },
                    { imageTwoContent, "PhotoTwo", photoTwo.FileName }
                };

                // make network request and serialize response
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ApiKey);
                var httpResponse = await _httpClient.PostAsync($"{(Environment == DottaEnvironment.Production ? BaseUrlProduction : BaseUrlSandbox)}Face/Match", formData);
                var response = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseDTO = JsonSerializer.Deserialize<HttpDottaFaceMatchResponse>(response);
                    faceMatchResponse = new DottaResponse<FaceMatchResponse>
                    {
                        Status = responseDTO.status,
                        Message = responseDTO.message,
                        Data = new FaceMatchResponse
                        {
                            ErrorCode = responseDTO.data.errorCode,
                            ErrorMessage = responseDTO.data.errorMessage,
                            SimilarityScore = responseDTO.data.similarityScore
                        }
                    };
                }
                else
                {
                    var errorResponse = JsonSerializer.Deserialize<DottaResponse>(response);
                    faceMatchResponse.Status = errorResponse.Status;
                    faceMatchResponse.Message = errorResponse.Message;
                }

                imageOneContent.Dispose();
                imageTwoContent.Dispose();
                formData.Dispose();

                return faceMatchResponse;
            }
            catch (Exception ex)
            {
                faceMatchResponse.Status = false;
                faceMatchResponse.Message = ex.Message;

                return faceMatchResponse;
            }
        }

        public DottaResponse LivenessCheck()
        {
            throw new NotImplementedException();
        }

        private string GetPhotoMimeType(string photoExtension)
        {
            if (photoExtension == ".jpg") return MediaTypeNames.Image.Jpeg;
            else if (photoExtension == ".jpeg") return MediaTypeNames.Image.Jpeg;
            else if (photoExtension == ".png") return "image/png";
            else return null;
        }
    }
}
