
namespace AdministracionApi.Inventory.Endpoints.CreateProduct;
public record CreateProductCommand(ProductDto Product) : IRequest<CreateProductsResult>;
public record CreateProductsResult(ProductDto ProductDto);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
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

public class CreateProductHandler(IInventoryRepository repository) : IRequestHandler<CreateProductCommand, CreateProductsResult>
{
    public async Task<CreateProductsResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)=>
        new (await repository.CreateProduct(command.Product, cancellationToken));
}