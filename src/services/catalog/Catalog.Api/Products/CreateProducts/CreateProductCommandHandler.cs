namespace Catalog.Api.Products.CreateProducts;

public record CreateProductCommand(
    List<string> Category,
    string Name,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(
    Guid Id
);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IDocumentStore _store;
    public CreateProductCommandValidator(IDocumentStore store)
    {
        _store = store;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MustAsync(async (name, cancellationToken) =>
            {
                await using var session = _store.LightweightSession();
                var normalizedName = name;
                var exists = await session.Query<Product>()
                    .AnyAsync(p => p.Name == normalizedName, cancellationToken);
                return !exists;
            }).WithMessage("A product with this name already exists.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");

    }
}


public class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var document = command.Adapt<CreateProductCommand, Product>();
        session.Store(document);
        await session.SaveChangesAsync();
        var result = new CreateProductResult(document.Id);
        return result;
    }
}
