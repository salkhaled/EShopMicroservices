﻿namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);


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
