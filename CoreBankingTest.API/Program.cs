using CoreBankingTest.Infra.Data;
using CoreBanking.Test.Core.Interfaces;
using CoreBankingTest.Infra.Repositories; 
using Microsoft.EntityFrameworkCore;

public class CoreBankingTestAPIProgram{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
         builder.Services.AddDbContext<BankingDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register the AccountRepository as a singleton service
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}