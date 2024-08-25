var builder = WebApplication.CreateBuilder(args);

// ע�� Carter ��Ϊ���� HTTP �˵�ķ���
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    // ע�� MediatR�������ڴ������֮���н��������Ӧ��
    // �ӵ�ǰ���򼯣�Program ���ڵĳ��򼯣�ע������ MediatR ��صķ���
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    // ���� Marten ��Ϊ�ĵ����ݿ⡣
    opts.Connection(builder.Configuration.GetConnectionString("Database")!); // ��ȡ���������ݿ������ַ�����
}).UseLightweightSessions(); // ʹ���������Ự�� Marten �ĵ��洢������

var app = builder.Build();

app.MapCarter(); // �� Carter ע�ᵽӦ�ó����У��Ա������Դ��� HTTP �˵�

app.Run();
