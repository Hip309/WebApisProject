using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using StoreMVC.Client.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

//claims stay the same as we define them, instead of being mapped to different claims
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserHttpClient, UserHttpClient>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies";
    opt.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies")//register the cookie handler and the cookie-based authentication for our default scheme
.AddOpenIdConnect("oidc", opt =>
{
    opt.SignInScheme = "Cookies";// has the same value as DefaultScheme
    opt.Authority = "https://localhost:7288";
    opt.ClientId = "mvc-client";
    opt.ResponseType = "code id_token";
    opt.SaveTokens = true;//to store the token after successful authorization
    opt.ClientSecret = "MVCSecret";
    //middleware to communicate with the /userinfo endpoint to retrieve additional user data.
    opt.GetClaimsFromUserInfoEndpoint = true;
    opt.ClaimActions.DeleteClaim("sid");
    opt.ClaimActions.DeleteClaim("idp");
    opt.Scope.Add("address");
    //map new claim to claims
    //opt.ClaimActions.MapUniqueJsonKey("address", "address");
    opt.Scope.Add("roles");
    opt.ClaimActions.MapUniqueJsonKey("role", "role");

    opt.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role"
    };

    opt.Scope.Add("StoreApi");

    opt.Scope.Add("position");
    opt.Scope.Add("city");
    opt.ClaimActions.MapUniqueJsonKey("position", "position");
    opt.ClaimActions.MapUniqueJsonKey("city", "city");
});

builder.Services.AddAuthorization(authOpt =>
{
    authOpt.AddPolicy("CanCreateAndModifyData", policyBuilder =>
    {
        policyBuilder.RequireAuthenticatedUser();
        policyBuilder.RequireClaim("position", "Administrator");
        policyBuilder.RequireClaim("city", "HCM");
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    
}else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
