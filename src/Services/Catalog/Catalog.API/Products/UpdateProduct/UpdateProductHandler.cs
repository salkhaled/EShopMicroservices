using Catalog.API.Products.CreateProduct;
using FluentValidation;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage($"{nameof(UpdateProductCommand.Id)} is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage($"{nameof(UpdateProductCommand.Name)} is required")
            .Length(2, 150).WithMessage($"{nameof(UpdateProductCommand.Name)} must be betwwen 2 and 150 charachters");

        RuleFor(x => x.Category).NotEmpty().WithMessage($"{nameof(UpdateProductCommand.Category)} is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage($"{nameof(UpdateProductCommand.ImageFile)} is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage($"{nameof(UpdateProductCommand.Price)} must be greater than 0");
    }
}


internal class UpdateProductCommandHandler
    (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) 
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@command}", command);

        // Update Product entity from command object
        Product product = await session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException();

        product.Name = command.Name;
        product.Category = product.Category;
        product.Description = product.Description;
        product.ImageFile = product.ImageFile;
        product.Price = product.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(IsSuccess: true); 
    }
}
