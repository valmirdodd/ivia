------------------------------------------------------------------------------------------------------------------------------------------------------
USE dbAlazom
------------------------------------------------------------------------------------------------------------------------------------------------------


DROP TABLE IF EXISTS dbo.AGENDAMENTOS
DROP TABLE IF EXISTS dbo.VEICULOS
DROP TABLE IF EXISTS dbo.FORNECEDORES
DROP TABLE IF EXISTS dbo.SITUACAO_AGENDAMENTOS
GO

/*
DROP TABLE dbo.AGENDAMENTOS
DROP TABLE dbo.VEICULOS
DROP TABLE dbo.FORNECEDORS
DROP TABLE dbo.SITUACAO_AGENDAMENTOS
GO
*/

------------------------------------------------------------------------------------------------------------------------------------------------------
-- Tabela: SITUACAO_AGENDAMENTOS
------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE dbo.SITUACAO_AGENDAMENTOS(
	IdSituacaoAgendamento INT NOT NULL IDENTITY (0,1),
	DsSituacao VARCHAR(20),
)
GO

ALTER TABLE dbo.SITUACAO_AGENDAMENTOS ADD CONSTRAINT PK_SITUACAO_AGENDAMENTOS PRIMARY KEY (IdSituacaoAgendamento)

INSERT INTO SITUACAO_AGENDAMENTOS (DsSituacao) Values ('Agendado'), ('Em atendimento'), ('Concluído'), ('Atrasado'), ('Cancelado')
SELECT * FROM SITUACAO_AGENDAMENTOS

------------------------------------------------------------------------------------------------------------------------------------------------------
-- Tabela: FORNECEDORES
------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE dbo.FORNECEDORES(
	IdFornecedor INT NOT NULL IDENTITY (1,1),
	NmFornecedor VARCHAR(50)
)
GO

ALTER TABLE dbo.FORNECEDORES ADD CONSTRAINT PK_FORNECEDOR PRIMARY KEY (IdFornecedor)

INSERT INTO FORNECEDORES (NmFornecedor) Values ('Transportadora Marina'), ('Pé na Tábua Transportes'), ('Transportes XACOMIGO'), ('Caminhonete do Seu Zé')
SELECT * FROM FORNECEDORES

------------------------------------------------------------------------------------------------------------------------------------------------------
-- Tabela: VEICULOS
------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE dbo.VEICULOS(
	IdVeiculo    INT NOT NULL IDENTITY (1,1),
	Placa        VARCHAR(7) NOT NULL,
	DsVeiculo    VARCHAR(50) NOT NULL,
	IdFornecedor INT NOT NULL
)
GO

ALTER TABLE dbo.VEICULOS ADD CONSTRAINT PK_VEICULOS PRIMARY KEY (IdVeiculo)
ALTER TABLE dbo.VEICULOS ADD CONSTRAINT FK_VEICULOS01 FOREIGN KEY (IdFornecedor) REFERENCES dbo.FORNECEDORES (IdFornecedor)

INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('AAA0001', 'Mercedes-Benz L-312 ‘Torpedo’', 1)
INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('BBB0002', 'VW 24.250 ‘Bob Esponja’', 1)
INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('CCC0003', 'Ford F-14000 ‘Sapão’', 2)
INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('DDD0004', 'Scania 111 ‘Jacaré’', 2)
INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('EEE0005', 'Mercedes-Benz LP-321 ‘Cara-Chata’', 3)
INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('FFF0006', 'FNM ‘Boca de Bagre’', 3)
INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('GGG0007', 'Ford F-600 ‘Cara Larga’', 3)
INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('HHH0008', 'Scania L 76 ‘João de Barro’', 4)
INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('III0009', 'VW 19.320 ‘Kombi a diesel’', 4)
INSERT INTO VEICULOS (Placa, DsVeiculo, IdFornecedor) Values ('JJJ0010', 'Mercedes-Benz 1113 ‘Muriçoca’', 4)
SELECT * FROM VEICULOS AS v INNER JOIN FORNECEDORES AS f ON f.IdFornecedor = v.IdFornecedor

------------------------------------------------------------------------------------------------------------------------------------------------------
-- Tabela: AGENDAMENTOS
------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE dbo.AGENDAMENTOS(
	IdAgendamento         INT NOT NULL IDENTITY (1,1),
	IdVaga                INT NOT NULL,
	DhInicial             DATETIME NOT NULL,
	DhFinal               DATETIME NOT NULL,
	Obs                   Varchar(MAX) NULL,
	DhCriacao             DATETIME NOT NULL,
	DhAtualizacao         DATETIME NULL,
	IdSituacaoAgendamento INT NOT NULL,
	IdVeiculo             INT NOT NULL
)
GO

ALTER TABLE dbo.AGENDAMENTOS ADD CONSTRAINT PK_AGENDAMENTOS PRIMARY KEY (IdAgendamento)
ALTER TABLE dbo.AGENDAMENTOS ADD CONSTRAINT DhCriacaoDefaultAgendamento DEFAULT GETDATE() FOR DhCriacao
ALTER TABLE dbo.AGENDAMENTOS ADD CONSTRAINT IdSituacaoDefaultAgendamento DEFAULT 0 FOR IdSituacaoAgendamento
ALTER TABLE dbo.AGENDAMENTOS ADD CONSTRAINT FK_AGENDAMENTOS01 FOREIGN KEY (IdSituacaoAgendamento) REFERENCES dbo.SITUACAO_AGENDAMENTOS (IdSituacaoAgendamento)
ALTER TABLE dbo.AGENDAMENTOS ADD CONSTRAINT FK_AGENDAMENTOS02 FOREIGN KEY (IdVeiculo) REFERENCES dbo.VEICULOS (IdVeiculo)
INSERT INTO AGENDAMENTOS (IdVaga, DhInicial, DhFinal, Obs, IdVeiculo) Values (1, '2020-12-31 10:00', '2020-12-31 11:00', 'TESTE-01', 1), (2, '2020-12-31 14:00', '2020-12-31 15:00', 'TESTE-02', 3), (3, '2021-01-01 08:00', '2021-01-01 09:00', 'TESTE-03', 8)
--UPDATE AGENDAMENTOS SET IdSituacaoAgendamento=2 WHERE IdAgendamento = (SELECT MIN(IdAgendamento) FROM AGENDAMENTOS)
SELECT a.*, s.DsSituacao, v.DsVeiculo, f.NmFornecedor FROM AGENDAMENTOS AS a
INNER JOIN SITUACAO_AGENDAMENTOS AS s ON s.IdSituacaoAgendamento = a.IdSituacaoAgendamento INNER JOIN VEICULOS AS v ON v.IdVeiculo = a.IdVeiculo INNER JOIN FORNECEDORES AS f ON f.IdFornecedor = v.IdFornecedor
GO


------------------------------------------------------------------------------------------------------------------------------------------------------
-- Trigger TR_Atualizar_Agendamento
------------------------------------------------------------------------------------------------------------------------------------------------------
--DROP TRIGGER TR_ATUALIZAR_AGENDAMENTO
CREATE OR ALTER TRIGGER dbo.TR_ATUALIZAR_AGENDAMENTO ON dbo.AGENDAMENTOS AFTER UPDATE
AS

BEGIN

	IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted) BEGIN
		DECLARE @IdAgendamentoAlterado AS INT = (SELECT IdAgendamento FROM inserted)
		UPDATE AGENDAMENTOS
		SET DhAtualizacao = GETDATE()
		WHERE IdAgendamento = @IdAgendamentoAlterado
	END

END
GO

