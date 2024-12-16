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

            // DbContext��ǉ����ASQLite�ڑ���ݒ�
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

            app.UseHttpsRedirection(); // HTTPS���_�C���N�g��ǉ�

            app.UseAuthorization();
            app.UseCors("AllowAllOrigins"); // CORS �|���V�[��K�p

            app.MapControllers(); // �R���g���[���[���}�b�s���O����

            app.Run();
        }
    }
}
