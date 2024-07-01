using CashFlow.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public interface IGetByIdExpenseUseCase
    {
        Task<ResponseExpenseJson> Execute(long Id);
    }
}
