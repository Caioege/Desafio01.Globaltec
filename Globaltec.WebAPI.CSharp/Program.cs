using Globaltec.Dominio.Constantes;
using Globaltec.Servicos.Servicos;
using Globaltec.Servicos.Servicos.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Globaltec.WebAPI.CSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(opcoes =>
            {
                opcoes.InvalidModelStateResponseFactory = contexto =>
                {
                    Dictionary<string, List<string>> errosDeValidacao = new();
                    foreach (var item in contexto.ModelState)
                    {
                        var mensagensDeErro = item.Value.Errors.Select(c => c.ErrorMessage);
                        var itemNoDicionario = errosDeValidacao.FirstOrDefault(c => c.Key == item.Key);
                        if (!string.IsNullOrEmpty(itemNoDicionario.Key))
                            itemNoDicionario.Value.AddRange(mensagensDeErro);
                        else
                            errosDeValidacao.Add(item.Key, mensagensDeErro.ToList());
                    }
                    return new BadRequestObjectResult(errosDeValidacao);
                };
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opcoes =>
            {
                opcoes.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Globaltec - API .NET 6",
                    Description = "API em .NET 6 para o teste de desenvolvedor da Globaltec.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "caiohenrick231@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/caio-henrick/")
                    }
                });

                var caminhoDoXML = Path.Combine(AppContext.BaseDirectory, "Globaltec.WebAPI.CSharp.xml");
                opcoes.IncludeXmlComments(caminhoDoXML);

                opcoes.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Insira o token de autenticação.",
                    Name = "Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "Bearer "
                });

                opcoes.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            {
                                Type = ReferenceType.SecurityScheme, 
                                Id = "Bearer"
                            }, 
                            Scheme = "oauth2", 
                            Name = "Bearer", 
                            In = ParameterLocation.Header,
                        },
                        new string[] { }
                }});

                opcoes.MapType<DateTime>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("yyyy-MM-dd")
                });
            });

            var chaveDeSeguranca = Encoding.ASCII.GetBytes(Chaves.ChaveParaGeracaoDeTokenDeAutenticacao);
            builder.Services.AddAuthentication(opcoes =>
            {
                opcoes.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opcoes.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opcoes =>
            {
                opcoes.RequireHttpsMetadata = false;
                opcoes.SaveToken = true;
                opcoes.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chaveDeSeguranca),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddSingleton<IUsuarioServico, UsuarioServico>();
            builder.Services.AddSingleton<IPessoaServico, PessoaServico>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}