using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public class GetByIdExpenseUseCase : IGetByIdExpenseUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        public GetByIdExpenseUseCase(IExpensesReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseExpenseJson?> Execute(long Id)
        {
            var result = await _repository.GetById(Id);

            if(result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_EXPENSE);
            }

            return _mapper.Map<ResponseExpenseJson?>(result);
        }
    }
}
