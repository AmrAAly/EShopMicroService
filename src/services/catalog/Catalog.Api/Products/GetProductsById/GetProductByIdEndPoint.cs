namespace Catalog.Api.Products;

public record GetProductByIdQueryResponse(Product Product);

public class GetProductByIdEndPoint : ICarterModule
{
    public async void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id:guid}", GetProductById);

        async Task<IResult> GetProductById(Guid id , ISender sender)
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdQueryResponse>();
            return Results.Ok(response);
        }
    }
}
