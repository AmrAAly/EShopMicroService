

using System.Reflection;

namespace Catalog.Api.Products
{

    public record CreateProductResponse(Guid Id);
    public record CreateProductRequest(
        List<string> Category,
        string Name,
        string Description,
        string ImageFile,
        decimal Price
    );

    public class CreateProductEndPoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", CreateProduct)
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .WithSummary("Creates product")
            .WithDescription("Create Product");


             async Task<IResult> CreateProduct(CreateProductRequest request, ISender sender)
            {
                var command = request.Adapt<CreateProductRequest, CreateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}", response);
            }
        }
    }
}
