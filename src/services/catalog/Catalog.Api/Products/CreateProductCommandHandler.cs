using BuildingBlocks.CQRS;

namespace Catalog.Api.Products
{
    public record CreateProductCommand(
        List<string> Category,
        string Name,
        string Description,
        string ImageFile,
        decimal Price
    ): ICommand<CreateProductResult>;

    public record CreateProductResult(
        Guid Id
    );

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Implement the logic to create a product here
            var result = new CreateProductResult(Guid.NewGuid());
            return Task.FromResult(result);
        }
    }
}
