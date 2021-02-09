using System.Threading.Tasks;

namespace Library.Services
{
    public static class JSRuntimeExtensions
    {
        private static Task<bool> Confirm(this IJSRuntime jsRuntime, string message)
        {
            return jsRuntime.InvokeAsync<bool>("confirm", message);

        }

    }
    public interface IJSRuntime
    {
        Task<T> InvokeAsync<T>(string v, string message);
    }
}