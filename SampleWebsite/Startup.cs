using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoicDreams.Middleware;

namespace SampleWebsite
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			Configuration = configuration;
			WebHostEnvironment = webHostEnvironment;
		}

		public IConfiguration Configuration { get; }
		private IWebHostEnvironment WebHostEnvironment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddFileProxyOptions(options => {
				options.WebRoot = WebHostEnvironment.WebRootPath;
				options.Routes = new StoicDreams.FileProxy.Interface.IRoute[] {
					new StoicDreams.FileProxy.Routing.FileRoute("/manifest.webmanifest", "/manifest.json")
				};
			});
			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseFileProxy();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
