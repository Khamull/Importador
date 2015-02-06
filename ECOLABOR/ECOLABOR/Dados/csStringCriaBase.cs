using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Dados
{
    static class csStringCriaBase
    {
        static public string criaBase(string DB)
        {
            string criaBase_ = @"USE ["+DB+"]";

                        criaBase_ += @" SET ANSI_NULLS ON
                            
                            SET QUOTED_IDENTIFIER ON
                            
                            SET ANSI_PADDING ON
                            
                            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USUARIO]') AND type in (N'U'))
                            BEGIN
                            CREATE TABLE [dbo].[USUARIO](
	                            [ID_USUARIO] [int] NOT NULL,
	                            [NOME] [varchar](100) NOT NULL,
	                            [SOBRENOME] [varchar](200) NOT NULL,
                            PRIMARY KEY CLUSTERED 
                            (
	                            [ID_USUARIO] ASC
                            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                            ) ON [PRIMARY]
                            END
                            
                            SET ANSI_PADDING OFF
                            
                            INSERT [dbo].[USUARIO] ([ID_USUARIO],[NOME], [SOBRENOME]) VALUES (0, N'ECOLABOR', N'ECOLABOR')
                            /****** Object:  Table [dbo].[ACESSOS]    Script Date: 04/28/2014 08:35:29 ******/
                            SET ANSI_NULLS ON
                            
                            SET QUOTED_IDENTIFIER ON
                            
                            SET ANSI_PADDING ON
                            
                            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACESSOS]') AND type in (N'U'))
                            BEGIN
                            CREATE TABLE [dbo].[ACESSOS](
	                            [ID_ACESSO] [int] NOT NULL,
	                            [DESCRICAO] [varchar](255) NOT NULL,
                            PRIMARY KEY CLUSTERED 
                            (
	                            [ID_ACESSO] ASC
                            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                            ) ON [PRIMARY]
                            END
                            
                            SET ANSI_PADDING OFF
                            
                            INSERT [dbo].[ACESSOS] ([ID_ACESSO], [DESCRICAO]) VALUES (0, N'ADMINISTRADOR DO SISTEMA')
                            INSERT [dbo].[ACESSOS] ([ID_ACESSO], [DESCRICAO]) VALUES (1, N'USUÁRIO DO SISTEMA')
                            
                            SET ANSI_NULLS ON
                            
                            SET QUOTED_IDENTIFIER ON
                            
                            SET ANSI_PADDING ON
                            
                            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MODELOS]') AND type in (N'U'))
                            BEGIN
                            CREATE TABLE [dbo].[MODELOS](
	                            [ID_MODELO] [int] NOT NULL,
	                            [NOME_MODELO] [varchar](max) NOT NULL,
	                            [NOME_WORKSHEET] [varchar](max) NOT NULL,
	                            [IT] [varchar](max) NOT NULL,
	                            [ID_USUARIO] [int] NOT NULL,
	                            [DATA_CRIACAO] [datetime] NOT NULL,
	                            [TIME_LAST_EVENT] [timestamp] NOT NULL,
                            PRIMARY KEY CLUSTERED 
                            (
	                            [ID_MODELO] ASC
                            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                            ) ON [PRIMARY]
                            END
                            
                            SET ANSI_PADDING OFF
                            

                            SET ANSI_NULLS ON
                            
                            SET QUOTED_IDENTIFIER ON
                            
                            SET ANSI_PADDING ON
                            
                            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOGIN_USUARIO]') AND type in (N'U'))
                            BEGIN
                            CREATE TABLE [dbo].[LOGIN_USUARIO](
	                            [ID_LOGIN] [int] NOT NULL,
	                            [ID_USUARIO] [int] NOT NULL,
	                            [LOGIN_USUARIO] [varchar](10) NOT NULL,
	                            [SENHA] [varchar](100) NOT NULL,
	                            [ID_ACESSO] [int] NOT NULL,
	                            [ATIVO] [bit] NOT NULL,
                            PRIMARY KEY CLUSTERED 
                            (
	                            [ID_LOGIN] ASC
                            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                            ) ON [PRIMARY]
                            END
                            
                            SET ANSI_PADDING OFF
                            
                            INSERT [dbo].[LOGIN_USUARIO] ([ID_LOGIN], [ID_USUARIO], [LOGIN_USUARIO], [SENHA], [ID_ACESSO], [ATIVO]) VALUES (0, 0, N'ecolabor', N'ecolabor', 0, 1)
                            /****** Object:  Table [dbo].[PARAMETROS]    Script Date: 04/28/2014 08:35:29 ******/
                            SET ANSI_NULLS ON
                            
                            SET QUOTED_IDENTIFIER ON
                            
                            SET ANSI_PADDING ON
                            
                            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PARAMETROS]') AND type in (N'U'))
                            BEGIN
                            CREATE TABLE [dbo].[PARAMETROS](
	                            [ID_PARAMETROS] [int] NOT NULL,
	                            [ID_MODELO] [int] NOT NULL,
	                            [PARAMETRO] [varchar](max) NOT NULL,
	                            [COLUNAS] [varchar](max) NOT NULL,
	                            [RANGE_INICIAL] [varchar](max) NOT NULL,
	                            [RANGE_FINAL] [varchar](max) NOT NULL,
                                [COORDENADAS_FINAL] [varchar](max) NOT NULL,
                             PRIMARY KEY CLUSTERED 
                            (
	                            [ID_PARAMETROS] ASC
                            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                            ) ON [PRIMARY]
                            END
                            
                            SET ANSI_PADDING OFF
                            
                            
                            IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_ACESSO_USUARIO]') AND parent_object_id = OBJECT_ID(N'[dbo].[LOGIN_USUARIO]'))
                            ALTER TABLE [dbo].[LOGIN_USUARIO]  WITH CHECK ADD  CONSTRAINT [fk_ACESSO_USUARIO] FOREIGN KEY([ID_ACESSO])
                            REFERENCES [dbo].[ACESSOS] ([ID_ACESSO])
                            
                            IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_ACESSO_USUARIO]') AND parent_object_id = OBJECT_ID(N'[dbo].[LOGIN_USUARIO]'))
                            ALTER TABLE [dbo].[LOGIN_USUARIO] CHECK CONSTRAINT [fk_ACESSO_USUARIO]
                            

                            IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_USUARIO]') AND parent_object_id = OBJECT_ID(N'[dbo].[LOGIN_USUARIO]'))
                            ALTER TABLE [dbo].[LOGIN_USUARIO]  WITH CHECK ADD  CONSTRAINT [fk_USUARIO] FOREIGN KEY([ID_USUARIO])
                            REFERENCES [dbo].[USUARIO] ([ID_USUARIO])
                            
                            IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_USUARIO]') AND parent_object_id = OBJECT_ID(N'[dbo].[LOGIN_USUARIO]'))
                            ALTER TABLE [dbo].[LOGIN_USUARIO] CHECK CONSTRAINT [fk_USUARIO]
                            

                            IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_IDUSUARIO]') AND parent_object_id = OBJECT_ID(N'[dbo].[MODELOS]'))
                            ALTER TABLE [dbo].[MODELOS]  WITH CHECK ADD  CONSTRAINT [fk_IDUSUARIO] FOREIGN KEY([ID_USUARIO])
                            REFERENCES [dbo].[USUARIO] ([ID_USUARIO])
                            
                            IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_IDUSUARIO]') AND parent_object_id = OBJECT_ID(N'[dbo].[MODELOS]'))
                            ALTER TABLE [dbo].[MODELOS] CHECK CONSTRAINT [fk_IDUSUARIO]
                            

                            IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_MODELO]') AND parent_object_id = OBJECT_ID(N'[dbo].[PARAMETROS]'))
                            ALTER TABLE [dbo].[PARAMETROS]  WITH CHECK ADD  CONSTRAINT [fk_MODELO] FOREIGN KEY([ID_MODELO])
                            REFERENCES [dbo].[MODELOS] ([ID_MODELO])

                            

                            IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_MODELO]') AND parent_object_id = OBJECT_ID(N'[dbo].[PARAMETROS]'))
                            ALTER TABLE [dbo].[PARAMETROS] CHECK CONSTRAINT [fk_MODELO] 
                            ";
           return criaBase_;
        }
    }
}
