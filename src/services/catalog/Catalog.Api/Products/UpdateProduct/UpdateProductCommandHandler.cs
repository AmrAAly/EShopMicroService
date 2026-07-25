namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(Guid Id);
public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
            throw new Exception($"Product with Id {command.Id} not found.");
        command.Adapt(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(product.Id);
    }
}
