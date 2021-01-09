using Imposto.Core.Data;
using Imposto.Core.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteImposto
{
    static class Program
    {
        static IServiceProvider ServiceProvider { get; set; }

        static void ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<INotaFiscalService, NotaFiscalService>();
            services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
            ServiceProvider = services.BuildServiceProvider();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigureServices();
            Application.Run(new FormImposto((INotaFiscalService)ServiceProvider.GetService(typeof(INotaFiscalService))));
        }
    }
}
