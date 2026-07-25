namespace Catalog.Api.Products;


public record GetProductQuery() : IQuery<GetProductQueryResult>;
public record GetProductQueryResult(IEnumerable<Product> Products);


internal class GetProductQueryHandler(
    IDocumentSession session,
    ILogger<GetProductQueryHandler> logger) :
    IQueryHandler<GetProductQuery, GetProductQueryResult>
{

    public async Task<GetProductQueryResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get product query handler invoked : {Query}", query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductQueryResult(products);
    }
}
