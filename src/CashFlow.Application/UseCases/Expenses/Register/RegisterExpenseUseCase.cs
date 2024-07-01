using AutoMapper;
using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterExpenseUseCase(IExpensesWriteOnlyRepository expensesRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _expensesRepository = expensesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _expensesRepository.Add(entity);
        await _unitOfWork.Commit();
        return _mapper.Map<ResponseRegisteredExpenseJson>(entity);
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorsList = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsList);
        }
    }
}
