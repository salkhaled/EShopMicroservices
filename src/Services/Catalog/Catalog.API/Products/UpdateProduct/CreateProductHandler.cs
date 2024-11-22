namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(Guid Id);


internal class UpdateProductCommandHandler(IDocumentSession session) 
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        // Update Product entity from command object
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

        return new UpdateProductResult(product.Id); 
    }
}
