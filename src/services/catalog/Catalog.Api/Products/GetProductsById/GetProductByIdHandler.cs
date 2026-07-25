namespace Catalog.Api.Products;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResult>;

public record GetProductByIdQueryResult(Product Product);

public class GetProductByIdHandler(
    IDocumentSession session,
    ILogger<GetProductByIdHandler> logger) :
    IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {

        var product = await session.LoadAsync<Product>(query.Id);
        if (product is null)
            return new GetProductByIdQueryResult(null);

        return new GetProductByIdQueryResult(product);

    }
}
