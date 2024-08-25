namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                // 将接收到的 CreateProductRequest 对象转换为 CreateProductCommand 对象
                var command = request.Adapt<CreateProductCommand>();
                // 使用 MediatR 中的 ISender 接口发送创建产品的命令
                var result = await sender.Send(command);
                // 将 MediatR 返回的结果转换为 CreateProductResponse 对象
                var response = result.Adapt<CreateProductResponse>();
                // 返回 HTTP 201 Created 状态码，并包含创建的产品信息
                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct") // 为路由命名，方便后续引用和管理
            .Produces<CreateProductResponse>(StatusCodes.Status201Created) // 指定成功创建产品时返回的响应类型和状态码
            .ProducesProblem(StatusCodes.Status400BadRequest) // 指定出现错误时返回的问题详情，状态码为 400 Bad Request
            .WithSummary("Create Product") // 指定路由的简要描述
            .WithDescription("Create Product"); // 指定路由的详细描述
        }
    }
}
