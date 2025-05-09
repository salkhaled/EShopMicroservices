﻿
namespace Basket.API.Basket.DeleteBasket;


public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool isSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

internal sealed class DeleteBasketCommandHandler(IBasketRepository respository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await respository.DeleteBasket(command.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}
