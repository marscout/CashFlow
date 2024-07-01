using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.GetAll;

namespace CashFlow.Application.UseCases.Expenses.DeleteById;

public interface IDeleteByIdExpenseUseCase
{
    Task Execute(long Id);
}
