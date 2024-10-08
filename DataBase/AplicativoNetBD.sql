USE [AplicativoNet]
GO
/****** Object:  Table [dbo].[Atendimento]    Script Date: 27/09/2024 16:59:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Atendimento](
	[AtendimentoId] [uniqueidentifier] NOT NULL,
	[PacienteId] [uniqueidentifier] NOT NULL,
	[Token] [int] IDENTITY(1,1) NOT NULL,
	[DataHoraChegada] [datetime] NOT NULL,
	[Status] [varchar](25) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AtendimentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Especialidade]    Script Date: 27/09/2024 16:59:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especialidade](
	[EspecialidadeId] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Descricao] [varchar](255) NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EspecialidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medico]    Script Date: 27/09/2024 16:59:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medico](
	[MedicoId] [uniqueidentifier] NOT NULL,
	[EspecialidadeId] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Disponivel] [bit] NOT NULL,
	[Ativo] [bit] NOT NULL,
	[CrmUf] [varchar](13) NULL,
PRIMARY KEY CLUSTERED 
(
	[MedicoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 27/09/2024 16:59:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paciente](
	[PacienteId] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Sexo] [char](1) NOT NULL,
	[Email] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PacienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Triagem]    Script Date: 27/09/2024 16:59:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Triagem](
	[TriagemId] [uniqueidentifier] NOT NULL,
	[AtendimentoId] [uniqueidentifier] NOT NULL,
	[Sintomas] [varchar](1000) NOT NULL,
	[PressaoSistolica] [decimal](5, 1) NOT NULL,
	[PressaoDiastolica] [decimal](5, 1) NOT NULL,
	[Peso] [decimal](5, 1) NOT NULL,
	[Altura] [decimal](3, 2) NOT NULL,
	[EspecialidadeId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TriagemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 27/09/2024 16:59:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[UsuarioId] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Senha] [varchar](255) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[DataUltimoLogin] [datetime] NULL,
 CONSTRAINT [PK_ST_ST_Usuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Atendimento] ON 

INSERT [dbo].[Atendimento] ([AtendimentoId], [PacienteId], [Token], [DataHoraChegada], [Status]) VALUES (N'5cf6bc4f-f1a7-4712-a2da-26551168846c', N'9474d3d5-5a5c-43f2-8aca-583e2fbe9c9e', 1, CAST(N'2024-09-27T11:30:54.897' AS DateTime), N'Aguardando Consulta')
INSERT [dbo].[Atendimento] ([AtendimentoId], [PacienteId], [Token], [DataHoraChegada], [Status]) VALUES (N'39fe2535-917e-432a-9802-438202b65a24', N'02a3a5fa-f7ae-4519-bd44-c37067259936', 1006, CAST(N'2024-09-27T16:26:58.420' AS DateTime), N'Aguardando Atendimento')
INSERT [dbo].[Atendimento] ([AtendimentoId], [PacienteId], [Token], [DataHoraChegada], [Status]) VALUES (N'f5924eba-421f-489a-89d6-5a8879f618ea', N'03283a37-11d9-4be8-ab58-a841437f8eeb', 1003, CAST(N'2024-09-27T16:26:18.170' AS DateTime), N'Em consulta')
INSERT [dbo].[Atendimento] ([AtendimentoId], [PacienteId], [Token], [DataHoraChegada], [Status]) VALUES (N'1420f715-13fe-40fb-8e31-5f29d31c91a7', N'246793a1-d418-4b68-97d8-a72e04197d1a', 1004, CAST(N'2024-09-27T16:26:33.600' AS DateTime), N'Em consulta')
INSERT [dbo].[Atendimento] ([AtendimentoId], [PacienteId], [Token], [DataHoraChegada], [Status]) VALUES (N'b87a2901-1479-4686-84f2-6151de5a91da', N'ab96fde7-5207-4ac5-8f48-ee26de8298fe', 1007, CAST(N'2024-09-27T16:48:13.837' AS DateTime), N'Aguardando Atendimento')
INSERT [dbo].[Atendimento] ([AtendimentoId], [PacienteId], [Token], [DataHoraChegada], [Status]) VALUES (N'4389c5b6-2252-4571-8a6c-7a88dddb23c2', N'c4caeb6f-a631-4c78-8c8a-aac9af0a9160', 3, CAST(N'2024-09-27T11:32:54.987' AS DateTime), N'Aguardando Consulta')
INSERT [dbo].[Atendimento] ([AtendimentoId], [PacienteId], [Token], [DataHoraChegada], [Status]) VALUES (N'faf78deb-f94b-4310-9b9b-e86655338e34', N'02a3a5fa-f7ae-4519-bd44-c37067259936', 1005, CAST(N'2024-09-27T16:26:42.767' AS DateTime), N'Aguardando Atendimento')
SET IDENTITY_INSERT [dbo].[Atendimento] OFF
GO
INSERT [dbo].[Especialidade] ([EspecialidadeId], [Nome], [Descricao], [Ativo]) VALUES (N'677f357b-c3fe-4d35-afec-5ad45186cc0c', N'Pediatra', N'Pediatria', 1)
INSERT [dbo].[Especialidade] ([EspecialidadeId], [Nome], [Descricao], [Ativo]) VALUES (N'bf7fa293-fbe8-4b9d-9379-92f84c683bf5', N'Neurologista', N'Neurologia', 1)
INSERT [dbo].[Especialidade] ([EspecialidadeId], [Nome], [Descricao], [Ativo]) VALUES (N'f72933a7-ea69-4493-ac11-b5c0f45f81d3', N'Cardiologista', N'Cardiologia', 1)
INSERT [dbo].[Especialidade] ([EspecialidadeId], [Nome], [Descricao], [Ativo]) VALUES (N'080fbfae-111c-4226-b1b4-e270f3e4bdfb', N'Psiquiatra', N'Psiquiatraia', 1)
INSERT [dbo].[Especialidade] ([EspecialidadeId], [Nome], [Descricao], [Ativo]) VALUES (N'a3b59922-1775-439d-ab6c-eaf142be89cd', N'Oftalmologista', N'Oftalmologia', 1)
GO
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'779c607e-9a7f-4097-8f15-291f13757adb', N'Luiza Lima', N'F', N'luiza.lima@example.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'a4909cf5-7865-47da-a363-39987173c901', N'João da Silva', N'M', N'joao.silva@example.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'9474d3d5-5a5c-43f2-8aca-583e2fbe9c9e', N'Sara Martins', N'F', N'Sara@Martins.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'a08be735-9537-4bf2-8d17-836102029db4', N'Maria Oliveira', N'F', N'maria.oliveira@example.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'246793a1-d418-4b68-97d8-a72e04197d1a', N'Lucas Pereira', N'M', N'lucas.pereira@example.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'03283a37-11d9-4be8-ab58-a841437f8eeb', N'Carlos Fernandes', N'M', N'carlos.fernandes@example.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'c4caeb6f-a631-4c78-8c8a-aac9af0a9160', N'Diego Almeida', N'M', N'Diego@Almeida.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'd83f641f-0a3d-42e1-898c-aec7e27e15ef', N'Marcos Castro', N'M', N'marcos@castro.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'02a3a5fa-f7ae-4519-bd44-c37067259936', N'Ricardo Alves', N'M', N'ricardo.alves@example.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'fb872719-94e8-4403-874b-e63f1771d3af', N'Ana Souza', N'F', N'ana.souza@example.com')
INSERT [dbo].[Paciente] ([PacienteId], [Nome], [Sexo], [Email]) VALUES (N'ab96fde7-5207-4ac5-8f48-ee26de8298fe', N'João da Silva', N'M', N'joao.silva@example.com')
GO
INSERT [dbo].[Triagem] ([TriagemId], [AtendimentoId], [Sintomas], [PressaoSistolica], [PressaoDiastolica], [Peso], [Altura], [EspecialidadeId]) VALUES (N'e1768c96-1016-4d1d-b5e1-12a9c005e7d9', N'4389c5b6-2252-4571-8a6c-7a88dddb23c2', N'Dor de cabeça e febre', CAST(14.2 AS Decimal(5, 1)), CAST(11.7 AS Decimal(5, 1)), CAST(47.5 AS Decimal(5, 1)), CAST(1.30 AS Decimal(3, 2)), N'677f357b-c3fe-4d35-afec-5ad45186cc0c')
INSERT [dbo].[Triagem] ([TriagemId], [AtendimentoId], [Sintomas], [PressaoSistolica], [PressaoDiastolica], [Peso], [Altura], [EspecialidadeId]) VALUES (N'7d447966-4771-4927-8b2a-298716dac531', N'5cf6bc4f-f1a7-4712-a2da-26551168846c', N'Febre, dor de cabeça e tontura', CAST(14.3 AS Decimal(5, 1)), CAST(16.1 AS Decimal(5, 1)), CAST(93.1 AS Decimal(5, 1)), CAST(1.80 AS Decimal(3, 2)), N'f72933a7-ea69-4493-ac11-b5c0f45f81d3')
INSERT [dbo].[Triagem] ([TriagemId], [AtendimentoId], [Sintomas], [PressaoSistolica], [PressaoDiastolica], [Peso], [Altura], [EspecialidadeId]) VALUES (N'c90ec93a-7488-437d-ba77-9e001afff952', N'5cf6bc4f-f1a7-4712-a2da-26551168846c', N'Dor de cabeça e febre', CAST(12.3 AS Decimal(5, 1)), CAST(11.2 AS Decimal(5, 1)), CAST(55.5 AS Decimal(5, 1)), CAST(1.22 AS Decimal(3, 2)), N'677f357b-c3fe-4d35-afec-5ad45186cc0c')
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Nome], [Email], [Senha], [Ativo], [DataCriacao], [DataAtualizacao], [DataUltimoLogin]) VALUES (N'f7339a4b-55e4-43f7-8ef8-3d84082e19ed', N'Caio Castro', N'caiocastro234544@outlook.com', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', 1, CAST(N'2024-09-26T16:05:12.257' AS DateTime), NULL, NULL)
INSERT [dbo].[Usuario] ([UsuarioId], [Nome], [Email], [Senha], [Ativo], [DataCriacao], [DataAtualizacao], [DataUltimoLogin]) VALUES (N'4bb47427-4f08-4b4a-a2c8-8de6d8b94212', N'Carlos Braz', N'carlos.braz@aplicativo.net', N'955a979634c393a4803c3391230ff41a39a78f70dc8f7b5514ed58f5fc521ea5aa24faf74d6d62070d73f9c3fa381d06616e9696e3f70bee0ca18a71621c8cee', 1, CAST(N'2024-09-27T15:19:58.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Usuario] ([UsuarioId], [Nome], [Email], [Senha], [Ativo], [DataCriacao], [DataAtualizacao], [DataUltimoLogin]) VALUES (N'8bf934bb-972e-468a-9716-c0ba8e85455b', N'Leonardo Melo', N'leonardo.melo.dev@outlook.com', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', 1, CAST(N'2024-09-26T16:04:42.253' AS DateTime), NULL, NULL)
GO
ALTER TABLE [dbo].[Atendimento]  WITH CHECK ADD  CONSTRAINT [Paciente_Atendimento] FOREIGN KEY([PacienteId])
REFERENCES [dbo].[Paciente] ([PacienteId])
GO
ALTER TABLE [dbo].[Atendimento] CHECK CONSTRAINT [Paciente_Atendimento]
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD  CONSTRAINT [Especialidade_Medico] FOREIGN KEY([EspecialidadeId])
REFERENCES [dbo].[Especialidade] ([EspecialidadeId])
GO
ALTER TABLE [dbo].[Medico] CHECK CONSTRAINT [Especialidade_Medico]
GO
ALTER TABLE [dbo].[Triagem]  WITH CHECK ADD  CONSTRAINT [Atendimento_Triagem] FOREIGN KEY([AtendimentoId])
REFERENCES [dbo].[Atendimento] ([AtendimentoId])
GO
ALTER TABLE [dbo].[Triagem] CHECK CONSTRAINT [Atendimento_Triagem]
GO
ALTER TABLE [dbo].[Triagem]  WITH CHECK ADD  CONSTRAINT [Especialidade_Triagem] FOREIGN KEY([EspecialidadeId])
REFERENCES [dbo].[Especialidade] ([EspecialidadeId])
GO
ALTER TABLE [dbo].[Triagem] CHECK CONSTRAINT [Especialidade_Triagem]
GO
