using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace TeaminalApp_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // �s�v�ȃ��_�C���N�g�ݒ肪�Ȃ����Ƃ��m�F
            // ���_�C���N�g���K�v�ȏꍇ��App.razor�Ŏ���
            await builder.Build().RunAsync();
        }
    }
}
