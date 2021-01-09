USE [Teste]
GO
IF OBJECT_ID('dbo.P_NOTA_FISCAL_ITEM') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.P_NOTA_FISCAL_ITEM
    IF OBJECT_ID('dbo.P_NOTA_FISCAL_ITEM') IS NOT NULL
        PRINT '<<< FALHA APAGANDO A PROCEDURE dbo.P_NOTA_FISCAL_ITEM >>>'
    ELSE
        PRINT '<<< PROCEDURE dbo.P_NOTA_FISCAL_ITEM APAGADA >>>'
END
go
SET QUOTED_IDENTIFIER ON
GO
SET NOCOUNT ON 
GO 
CREATE PROCEDURE P_NOTA_FISCAL_ITEM
(
    @pIdNotaFiscal int,
    @pCfop varchar(5),
    @pTipoIcms varchar(20),
    @pBaseIcms decimal(18,5),
    @pAliquotaIcms decimal(18,5),
    @pValorIcms decimal(18,5),
	@pBaseIpi decimal(18,5),
    @pAliquotaIpi decimal(18,5),
    @pValorIpi decimal(18,5),
    @pNomeProduto varchar(50),
    @pCodigoProduto varchar(20)
)
AS
BEGIN
	INSERT INTO [dbo].[NotaFiscalItem]
	   ([IdNotaFiscal]
	   ,[Cfop]
	   ,[TipoIcms]
	   ,[BaseIcms]
	   ,[AliquotaIcms]
	   ,[ValorIcms]
	   ,[BaseIpi]
	   ,[AliquotaIpi]
	   ,[ValorIpi]
	   ,[NomeProduto]
	   ,[CodigoProduto])
	VALUES
	   (@pIdNotaFiscal,
		@pCfop,
		@pTipoIcms,
		@pBaseIcms,
		@pAliquotaIcms,
		@pValorIcms,
		@pBaseIpi,
		@pAliquotaIpi,
		@pValorIpi,
		@pNomeProduto,
		@pCodigoProduto)
END
GO
GRANT EXECUTE ON dbo.P_NOTA_FISCAL_ITEM TO [public]
go
IF OBJECT_ID('dbo.P_NOTA_FISCAL_ITEM') IS NOT NULL
    PRINT '<<< PROCEDURE dbo.P_NOTA_FISCAL_ITEM CRIADA >>>'
ELSE
    PRINT '<<< FALHA NA CRIACAO DA PROCEDURE dbo.P_NOTA_FISCAL_ITEM >>>'
go