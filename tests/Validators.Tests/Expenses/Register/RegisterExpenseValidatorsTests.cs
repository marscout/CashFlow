﻿using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorsTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new ExpenseValidator();

        var request = RequestResgisterExpenseJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    public void ErrorTitleEmpty(string title)
    {
        //Arrange
        var validator = new ExpenseValidator();

        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.Title = title;
        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public void ErrorDateFuture()
    {
        //Arrange
        var validator = new ExpenseValidator();

        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(4);

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DATA_CANNOT_FOR_THE_FUTURE));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void ErrorAmountInvalid(decimal amount)
    {
        //Arrange
        var validator = new ExpenseValidator();

        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.Amount = amount;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }

    [Fact]
    public void ErrorPaymentTypeInvalid()
    {
        //Arrange
        var validator = new ExpenseValidator();

        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;
        
        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }

}
