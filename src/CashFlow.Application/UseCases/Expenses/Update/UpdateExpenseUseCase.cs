using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExpensesUpdateOnlyRepository _repository;
    public UpdateExpenseUseCase(IMapper mapper,
        IUnitOfWork unitOfWork,
        IExpensesUpdateOnlyRepository repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    public async Task Execute(long Id, RequestExpenseJson request)
    {
        Validate(request);
        var expense = await _repository.GetById(Id);
        if(expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_EXPENSE);
        }
        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _unitOfWork.Commit();
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
