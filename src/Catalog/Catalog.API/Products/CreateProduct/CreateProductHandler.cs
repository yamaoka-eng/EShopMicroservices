namespace Catalog.API.Products.CreateProduct
{
    // 定义创建产品的命令，包括产品的名称、类别列表、描述、图片文件和价格。
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    // 定义创建产品命令的结果，包括新创建产品的唯一标识符（Id）。
    public record CreateProductResult(Guid Id);

    // 创建产品命令的处理程序，实现了 ICommandHandler 接口，处理 CreateProductCommand 并返回 CreateProductResult。
    internal class CreateProductComandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        // 实现 Handle 方法，处理创建产品的业务逻辑。
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            // 将新产品对象存储到会话中。
            session.Store(product);
            // 异步保存更改到数据库，并在保存完成后返回新创建产品的 Id。
            await session.SaveChangesAsync(cancellationToken);
            // 返回包含新产品 Id 的 CreateProductResult 对象。
            return new CreateProductResult(product.Id);
        }
    }
}
