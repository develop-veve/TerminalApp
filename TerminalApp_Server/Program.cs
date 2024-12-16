using Microsoft.EntityFrameworkCore;
using TerminalApp_Server.Data;

namespace TerminalApp_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // DbContextを追加し、SQLite接続を設定
            builder.Services.AddDbContext<ProductResultsContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); // HTTPSリダイレクトを追加

            app.UseAuthorization();
            app.UseCors("AllowAllOrigins"); // CORS ポリシーを適用

            app.MapControllers(); // コントローラーをマッピングする

            app.Run();
        }
    }
}
