namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", UpdateProduct)
            .WithName("UpdateProduct")
            .Produces<UpdateProductResult>(StatusCodes.Status200OK)
            .WithSummary("Updates product")
            .WithDescription("Update Product");
      
        async Task<IResult> UpdateProduct(UpdateProductRequest request, ISender sender)
        {
            var command = request.Adapt<UpdateProductRequest, UpdateProductCommand>();
            var result = await sender.Send(command);
            return Results.Ok(result);
        }

    }
}
