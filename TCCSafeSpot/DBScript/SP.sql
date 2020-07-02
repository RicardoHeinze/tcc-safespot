USE [TCCSafeSpot.Models.ProjetoContext]
GO

/****** Object: SqlProcedure [dbo].[TipoCrimePorMes_Anual] Script Date: 18/05/2019 00:05:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[TipoCrimePorMes_Anual]
AS

	DECLARE @AnoMax DATETIME

	SELECT @AnoMax = MAX(Data) FROM CrimeSSP

	DECLARE @AnoRef VARCHAR(4) = DATEPART(YEAR, @AnoMax)
	DECLARE @MesRef VARCHAR(2) = DATEPART(MONTH, @AnoMax)
	
	DECLARE @DataInicial DATETIME = CONVERT(DATETIME, @AnoRef + '-' + @MesRef + '-01 00:00:00')
	
	SELECT
		COUNT(cssp.id) as QtdCrime
		, tc.nome AS Nome
		, DATEPART(month, cssp.Data) AS Data
	FROM
		CrimeSSP cssp
	INNER JOIN
		TipoCrime tc ON cssp.TipoCrimeId = tc.id
	WHERE 
		Data Between 
			 @DataInicial 
		AND  DATEADD(MONTH, 1, @DataInicial) 
	GROUP BY tc.nome, DATEPART(month, cssp.Data)

----------------------------------------------------------------------------------------

USE [TCCSafeSpot.Models.ProjetoContext]
GO

/****** Object: SqlProcedure [dbo].[TipoCrimePorMes] Script Date: 18/05/2019 00:05:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[TipoCrimePorMes]
AS

	DECLARE @AnoMax DATETIME

	SELECT @AnoMax = MAX(Data) FROM CrimeSSP

	DECLARE @AnoRef VARCHAR(4) = DATEPART(YEAR, @AnoMax)
	DECLARE @MesRef VARCHAR(2) = DATEPART(MONTH, @AnoMax)
	
	DECLARE @DataInicial DATETIME = CONVERT(DATETIME, @AnoRef + '-' + @MesRef + '-01 00:00:00')

	SELECT
		COUNT(cssp.id) AS QtdCrime
		, tc.nome AS Nome
	FROM
		CrimeSSP cssp
	INNER JOIN
		TipoCrime tc ON cssp.TipoCrimeId = tc.id
	WHERE 
		Data Between 
			 @DataInicial 
		AND  DATEADD(MONTH, 1, @DataInicial)
	GROUP BY tc.nome

-----------------------------------------------------------------

USE [TCCSafeSpot.Models.ProjetoContext]
GO

/****** Object: SqlProcedure [dbo].[QtdCrimePorDia] Script Date: 18/05/2019 00:05:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[QtdCrimePorDia]
AS

	DECLARE @AnoMax DATETIME

	SELECT @AnoMax = MAX(Data) FROM CrimeSSP

	DECLARE @AnoRef VARCHAR(4) = DATEPART(YEAR, @AnoMax)
	DECLARE @MesRef VARCHAR(2) = DATEPART(MONTH, @AnoMax)
	
	DECLARE @DataInicial DATETIME = CONVERT(DATETIME, @AnoRef + '-' + @MesRef + '-01 00:00:00')

	SELECT
		count(id) AS QtdCrime, 
		CONVERT(VARCHAR(10), Data, 111) AS Data
	FROM
	CrimeSSP
	WHERE 
	Data Between 
			@DataInicial 
		AND  DATEADD(MONTH, 1, @DataInicial)
	GROUP BY CONVERT(VARCHAR(10), Data, 111)
