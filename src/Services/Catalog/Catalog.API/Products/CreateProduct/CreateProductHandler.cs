﻿namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage($"{nameof(CreateProductCommand.Name)} is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage($"{nameof(CreateProductCommand.Category)} is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage($"{nameof(CreateProductCommand.ImageFile)} is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage($"{nameof(CreateProductCommand.Price)} must be greater than 0");
    }
}


internal class CreateProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create Product entity from command object
        Product product = new()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // Save to the database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
