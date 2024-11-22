﻿namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record UpdateProductResponse(Guid Id);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}",
            async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductResponse>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateProductResponse>();
            return Results.NoContent();
        })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");

    }
}
