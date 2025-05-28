using Microsoft.EntityFrameworkCore;
using Zulfikar.Solar.API.Data;
using Zulfikar.Solar.API.Interfaces.Repositories;
using Zulfikar.Solar.API.Interfaces.Services;
using Zulfikar.Solar.API.Repositories;
using Zulfikar.Solar.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Zulfikar.Solar.API.Models;
using Microsoft.Extensions.Configuration; // أضف هذا الـ using

var builder = WebApplication.CreateBuilder(args);

// تعريف اسم لسياسة CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// إضافة خدمات CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200", // هذا هو النطاق الافتراضي لتطبيق Angular
                                              "http://localhost:8080") // إذا كنت تستخدم Vue/React أو أي بورت آخر
                                 .AllowAnyHeader() // السماح بجميع رؤوس الطلبات
                                 .AllowAnyMethod() // السماح بجميع أساليب HTTP (GET, POST, PUT, DELETE)
                                 .AllowCredentials(); // السماح ببيانات الاعتماد (مثل الكوكيز أو رؤوس Authorization)
                      });
});

builder.Services.AddControllers();

// إضافة دعم ProblemDetails لتحسين استجابات الأخطاء
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// إضافة Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// إضافة JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["ValidIssuer"],
        ValidAudience = jwtSettings["ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await SeedData.SeedAsync(dbContext);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // في وضع التطوير، يمكننا عرض صفحة استثناءات تفصيلية
}
else
{
    app.UseExceptionHandler("/Error"); // في وضع الإنتاج، استخدم UseExceptionHandler
    // app.UseHsts(); 
}

// بما أنك تعمل على HTTP، أزيل app.UseHttpsRedirection(); مؤقتًا من الكود الذي قدمته لي في المرة السابقة.
// إذا كنت تريد إعادة تفعيل HTTPS، قم بإلغاء تعليق هذا السطر وتأكد من إعداد الشهادات بشكل صحيح.
// app.UseHttpsRedirection(); 

// يجب أن يأتي استخدام CORS Middleware بعد UseRouting (إذا كنت تستخدمه) وقبل UseAuthorization.
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();