using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IForbiddenPhraseRepository, ForbiddenPhraseRepository>();
builder.Services.AddScoped<ISpecification<ProductEntity>, ProductNameSpecification>();
builder.Services.AddScoped<ISpecification<ProductEntity>, ProductPriceSpecification>();
builder.Services.AddScoped<ISpecification<ProductEntity>, ProductQuantitySpecification>();

var configuration = builder.Configuration;
builder.Services.AddDbContext<ProductDbContext>(options => {
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}, ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
