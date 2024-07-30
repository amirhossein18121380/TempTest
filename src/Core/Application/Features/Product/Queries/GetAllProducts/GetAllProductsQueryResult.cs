namespace Application.Features.Product.Queries.GetAllProducts;

public record GetAllProductsQueryResult(int ProductId, string ProductName,
    string ManufacturePhone, string ManufactureEmail, DateTime ProduceDate, bool IsAvailable);