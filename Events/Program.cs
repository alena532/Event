using System.Text;
using Events.Common.Attributes;
using Events.ConfigurationOptions;
using Events.Contracts.Requests.Users;
using Events.DBContext;
using Events.DBContext.Repositories;
using Events.DBContext.Repositories.Base;
using Events.Models;
using Events.Profiles;
using Events.Service;
using Events.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var  MyAllowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowedOrigins,
        policy  =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen(c =>
{
    OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
    {
        Name = "Bearer",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Description = "Specify the authorization token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
    };

    c.AddSecurityDefinition("jwt_auth", securityDefinition);
    
    OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference()
        {
            Id = "jwt_auth",
            Type = ReferenceType.SecurityScheme
        }
    };
    OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
    {
        {securityScheme, new string[] { }},
    };
    c.AddSecurityRequirement(securityRequirements);

});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

//builder.Services.Co
builder.Services.AddScoped<ValidationEventAttribute>();
builder.Services
    .AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();

var jwtOptions = new JwtOptions();
builder.Configuration.GetSection(JwtOptions.Path).Bind(jwtOptions);
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Path));

builder.Services.AddAuthorization(
    options => options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).Build()
);
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
        };
    });
builder.Services.AddScoped<IRepository<Event>,Repository>();

builder.Services.AddTransient<IUsersService,UsersService>();
builder.Services.AddTransient<ISpeakersService,SpeakersService>();
builder.Services.AddTransient<IEventsService,EventsService>();
builder.Services.AddTransient<IAuthService,AuthService>();
builder.Services.AddTransient<IJwtService,JwtService>();
builder.Services.AddTransient<IEventsService,EventsService>();
builder.Services.AddTransient<ICompaniesService,CompaniesService>();

//builder.Services.AddGraphQLServer()
//    .AddQueryType<Query>()
//    .AddMutationType<Mutation>();
    
builder.Services.AddAutoMapper( typeof(EventsMapper),typeof(AdminMapper),typeof(SpeakersMapper),typeof(CompaniesMapper));
var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
    
    var createService = scope.ServiceProvider.GetRequiredService<IUsersService>();
    await createService
            .CreateAdminAsync(
                new CreateAdminRequest
                {
                    FirstName = "Main",
                    LastName = "Admin",
                    UserName = "Admin",
                    Role = "Admin",
                    Password = "123@Qa123"
                }
            );
    
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API"));
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseWebSockets();
//app.MapGraphQL();
app.UseRouting();
   // .UseEndpoints(endpoints =>
   // {
   //     endpoints.MapGraphQL();
   // });

app.UseCors(MyAllowedOrigins);

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();