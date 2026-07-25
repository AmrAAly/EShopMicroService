namespace Catalog.Api.Products;

public record GetProductResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint : ICarterModule
{
    public async void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", GetProducts);

        async Task<IResult> GetProducts(ISender sender)
        {
            var result = await sender.Send(new GetProductQuery());
            var response = result.Adapt<GetProductResponse>();
            return Results.Ok(response);
        }
    }
}
