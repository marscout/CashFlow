using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCases.Expenses.Update;

public interface IUpdateExpenseUseCase
{
    Task Execute(long Id,RequestExpenseJson request);
}
