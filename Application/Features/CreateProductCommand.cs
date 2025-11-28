namespace Application.Features
{
    public record CreateProductCommand(
        string Name,
        decimal Price,
        int Stock,
        string? ImageUrl
    );
}
