﻿using AutoMapper;
using CashFlow.Application.UseCases.Expenses.GetAllExpenses;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.GetAll;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpensesUseCase : IGetAllExpensesUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        public GetAllExpensesUseCase(IExpensesReadOnlyRepository expensesRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = expensesRepository;
        }
        public async Task<ResponseExpensesJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseExpensesJson()
            {
                Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result),
            };
        }
    }
}
