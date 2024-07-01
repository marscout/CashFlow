using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.DeleteById
{
    public class DeleteByIdExpenseUseCase : IDeleteByIdExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteByIdExpenseUseCase(IExpensesWriteOnlyRepository expensesRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = expensesRepository;
        }
        public async Task Execute(long Id)
        {
            var result = await _repository.DeleteById(Id);
            if (!result)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_EXPENSE);
            }
            await _unitOfWork.Commit();
        }
    }
}
