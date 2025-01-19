using Newtonsoft.Json;

namespace Factory_Management_System_MVC_2.Helper
{
    public class ApiResponse<T> where T : class
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }

    public class JsonOperation<T> where T : class
    {
        public async Task<ApiResponse<T>> jsonDeserialization(dynamic response)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            var jsonresponse = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
            var jsonData = JsonConvert.SerializeObject(jsonresponse.data);
            apiResponse.Data = JsonConvert.DeserializeObject<T>(jsonData);
            apiResponse.Success = jsonresponse.success;
            apiResponse.Message = jsonresponse.message;
            apiResponse.StatusCode = jsonresponse.statusCode;
            return apiResponse;
        }
        public static string JsonSerialization(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
