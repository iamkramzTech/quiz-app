using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebQuizApp.Services;

namespace WebQuizApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

           // Add session services
            builder.Services.AddSession();

            // Add TempData providers
            builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            builder.Services.AddHttpClient<TriviaService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseStaticFiles() should be placed before app.UseRouting().
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            // UseSession should be placed before any middleware that might use session state
            app.UseSession();

            // If it has any authorization middleware, it should go here.
            // app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Quiz}/{action=StartQuiz}/{id?}");

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=TriviaQuiz}/{action=QuizSettings}/{id?}");
            });
            app.Run();
        }
    }
}
