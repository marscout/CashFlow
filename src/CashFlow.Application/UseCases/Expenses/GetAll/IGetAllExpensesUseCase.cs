using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.GetAll;

namespace CashFlow.Application.UseCases.Expenses.GetAllExpenses;

public interface IGetAllExpensesUseCase
{
    Task<ResponseExpensesJson> Execute();
}
