using ControllerAPI_1721030861.Models;

namespace ControllerAPI_1721030861.Consume
{
    public interface IConsumeAPI
    {
        Task<APIResponse> GetAsync(string endpoint, Dictionary<string, dynamic> dictPars, string accessToken = "");
        Task<APIResponse> PostAsync(string endpoint, Dictionary<string, dynamic> dictPars, string accessToken = "");
        Task<APIResponse> DeleteAsync(string endpoint, Dictionary<string, dynamic> dictPars, string accessToken = "");
        Task<APIResponse> PutAsync(string endpoint, Dictionary<string, dynamic> dictPars, string accessToken = "");
    }
}
