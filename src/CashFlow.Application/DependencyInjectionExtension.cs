using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.DeleteById;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetAllExpenses;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
    public static void AddAplication(this IServiceCollection service)
    {
        AddUseCases(service);
        AddAutoMapper(service);
    }

    private static void AddAutoMapper(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(this IServiceCollection service)
    {
        service.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        service.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
        service.AddScoped<IGetByIdExpenseUseCase, GetByIdExpenseUseCase>();
        service.AddScoped<IDeleteByIdExpenseUseCase, DeleteByIdExpenseUseCase>();
        service.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
    }
}
