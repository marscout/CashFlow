using CashFlow.Application.UseCases.Expenses.DeleteById;
using CashFlow.Application.UseCases.Expenses.GetAllExpenses;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseUseCase useCase,
        [FromBody] RequestExpenseJson request)
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpensesUseCase useCase)
    {

        var response = await useCase.Execute();

        return Ok(response);
    }
    [HttpGet]
    [Route("{Id}")]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdExpenseUseCase useCase,
        [FromRoute] long Id)
    {

        var response = await useCase.Execute(Id);

        return Ok(response);
    }
    [HttpDelete]
    [Route("{Id}")]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById([FromServices] IDeleteByIdExpenseUseCase useCase,
    [FromRoute] long Id)
    {
        await useCase.Execute(Id);

        return NoContent();
    }

    [HttpPut]
    [Route("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateExpenseUseCase useCase,
    [FromBody] RequestExpenseJson expense,
    [FromRoute] long Id)
    {
        await useCase.Execute(Id, expense);

        return NoContent();
    }
}
