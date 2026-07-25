namespace Catalog.Api.Products;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", DeleteProduct)
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .WithSummary("Deletes a product")
            .WithDescription("Delete Product");

        async Task<IResult> DeleteProduct(Guid id, ISender sender)
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command);
            return Results.Ok(new DeleteProductResponse(result.IsSuccess));
        }
    }
}

