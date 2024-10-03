using System.Data.SqlClient;
using System.Data;
using Business.Interfaces;
using Business.Services;
using AutoMapper;
using Business.Mappings;
using Data.Interfaces;
using Data.Repositories;
using Data.Interfaces.Util;
using Data.Util;
using Api.Provider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//var origins = builder.Configuration.GetSection("Cors")["Origins"].Split(';');
var origins = "https://localhost:3000";

builder.Services.AddCors(options =>
options.AddDefaultPolicy(builder => builder.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod()));

var tokenSettingsSection = builder.Configuration.GetSection("TokenSettings");

builder.Services.Configure<TokenSettings>(tokenSettingsSection);

var tokenSettings = tokenSettingsSection.Get<TokenSettings>();
var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		//options.RequireHttpsMetadata = true;
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = tokenSettings.Issuer,
			ValidAudience = tokenSettings.ValidIn,
			IssuerSigningKey = new SymmetricSecurityKey(key)
		};

		options.Events = new JwtBearerEvents
		{
			OnAuthenticationFailed = context =>
			{
				Console.Write("OnAuthenticationFaled: " + context.Exception.Message);
				return Task.CompletedTask;
			},
			OnTokenValidated = context =>
			{
				Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
				return Task.CompletedTask;
			}
		};
	});

builder.Services.AddSignalR(o =>
{
	o.EnableDetailedErrors = true;
});
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "API",
		Description = "7.0",
		TermsOfService = new Uri("https://example.com/terms"),
		Contact = new OpenApiContact
		{
			Name = "API",
		},
		License = new OpenApiLicense
		{
			Name = "Example License",
		}
	});

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below. \r\n\r\nExample: \"Bearer 12345abcdef\"",
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
	options.ResolveConflictingActions(x => x.First());
	options.OperationFilter<AddRequiredHeaderParameter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient(provider => new Func<IDbConnection>(() => new SqlConnection(connectionString)));

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IHashSenha, HashSenha>();
builder.Services.AddTransient<IVerificacoes, Verificacoes>();
builder.Services.AddTransient<IPacienteRepository, PacienteRepository>();
builder.Services.AddTransient<IAtendimentoRepository, AtendimentoRepository>();
builder.Services.AddTransient<ITriagemRepository, TriagemRepository>();
builder.Services.AddTransient<IEspecialidadeRepository, EspecialidadeRepository>();
builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();

//ConfigureAutoMapper
builder.Services.AddTransient(provider => new MapperConfiguration(mc =>
{
	mc.AddProfile(new UsuarioMapper());
	mc.AddProfile(new PacienteMapper());
	mc.AddProfile(new AtendimentoMapper());
	mc.AddProfile(new TriagemMapper());
	mc.AddProfile(new EspecialidadeMapper());
	mc.AddProfile(new MedicoMapper());

}).CreateMapper());

builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IPacienteService, PacienteService>();
builder.Services.AddTransient<IAtendimentoService, AtendimentoService>();
builder.Services.AddTransient<ITriagemService, TriagemService>();
builder.Services.AddTransient<IEspecialidadeService, EspecialidadeService>();
builder.Services.AddTransient<IMedicoService, MedicoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
