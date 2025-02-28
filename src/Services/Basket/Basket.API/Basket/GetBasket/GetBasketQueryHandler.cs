namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Basket);


internal class GetBasketQueryHandler
    //(IBasketRepsitory repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // TODO:
        //var basket = await _repository.GetBasket(query.UserName);

        //var product = await session.LoadAsync<ShoppingCart>(query.UserName, cancellationToken);
        //        return product == null ? throw new BasketNotFoundException(UserName: query.UserName)
        //    : new GetBasketResult(product);

        return new GetBasketResult(new ShoppingCart(query.UserName));
    }
}
