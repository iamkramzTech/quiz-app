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

            builder.Services.AddHttpClient<TriviaService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseStaticFiles() should be placed before app.UseRouting().

            app.UseStaticFiles();

            app.UseRouting();

            // If it has any authorization middleware, it should go here.
            // app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Quiz}/{action=StartQuiz}/{id?}");

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Quiz}/{action=QuizSettings}/{id?}");
            });
            app.Run();
        }
    }
}
