namespace AdministracionApi.Inventory.Endpoints.UpdateProduct;
public record UpdateProductCommand(ProductDto Product) : IRequest<UpdateProductsResult>;
public record UpdateProductsResult(ProductDto ProductDto);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Product.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity most be 0 or greater");
        RuleFor(x => x.Product.ProductPrices).NotEmpty().WithMessage("Prices are required");
        RuleForEach(x => x.Product.ProductPrices).ChildRules(prices =>
        {
            prices.RuleFor(pp => pp.Store)
                .NotEmpty()
                .WithMessage("Store name is required");
            prices.RuleFor(pp => pp.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
        });
    }
}


public class UpdateProductHandler(IInventoryRepository repository) : IRequestHandler<UpdateProductCommand,UpdateProductsResult >
{
    public async Task<UpdateProductsResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken) =>
        new (await repository.UpdateProduct(command.Product, cancellationToken));
}