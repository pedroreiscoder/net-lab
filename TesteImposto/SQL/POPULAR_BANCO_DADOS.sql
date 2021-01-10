DECLARE @vIdNota int = 0
DECLARE @vNumeroNotaFiscal int = 1
DECLARE @vSerie int = 2
DECLARE @vNomeCliente varchar(50) = 'TESTE'
DECLARE @vEstadoDestino varchar(50) = 'SP'
DECLARE @vEstadoOrigem varchar(50) = 'RJ'
DECLARE @vCount INT = 0
 
DECLARE @vCfop varchar(5)
DECLARE @vTipoIcms varchar(20)
DECLARE @vBaseIcms decimal(18,5)
DECLARE @vAliquotaIcms decimal(18,5)
DECLARE @vValorIcms decimal(18,5)
DECLARE @vBaseIpi decimal(18,5)
DECLARE @vAliquotaIpi decimal(18,5)
DECLARE @vValorIpi decimal(18,5)
DECLARE @vNomeProduto varchar(50)
DECLARE @vCodigoProduto varchar(20)
DECLARE @vDesconto decimal(18,5)

WHILE (@vCount <= 1000) 
BEGIN
	SET @vIdNota = 0
	SET @vNumeroNotaFiscal  = @vCount + 1
	SET @vSerie = 1
	SET @vNomeCliente = 'TESTE ' + CONVERT(VARCHAR, @vCount)

	IF (@vCount % 2) = 0
	BEGIN		
		SET @vEstadoOrigem = 'RJ'
		SET @vEstadoDestino = 'SP'
	END
	ELSE
	BEGIN		
		SET @vEstadoOrigem = 'MG'
		SET @vEstadoDestino = 'PE'
	END

	EXEC [dbo].[P_NOTA_FISCAL] 
		@pId = @vIdNota OUTPUT,
		@pNumeroNotaFiscal = @vNumeroNotaFiscal,
		@pSerie = @vSerie,
		@pNomeCliente = @vNomeCliente,
		@pEstadoDestino = @vEstadoDestino,
		@pEstadoOrigem = @vEstadoOrigem
		

	IF (@vCount % 2) = 0
		SET @vCfop = '6.102'
	ELSE
		SET @vCfop = '5.100'
	
	SET @vTipoIcms = '60'
	SET @vBaseIcms = 100.00
	SET @vAliquotaIcms = 10
	SET @vValorIcms = 10
	SET @vBaseIpi = 200.00
	SET @vAliquotaIpi = 20
	SET @vValorIpi = 20
	SET @vNomeProduto = 'PRODUTO DE CARGA'
	SET @vCodigoProduto = '123-5548-555-22'
	SET @vDesconto = 0.10

	EXEC [dbo].[P_NOTA_FISCAL_ITEM]
		@pIdNotaFiscal = @vIdNota,
		@pCfop = @vCfop,
		@pTipoIcms = @vTipoIcms,
		@pBaseIcms = @vBaseIcms,
		@pAliquotaIcms = @vAliquotaIcms,
		@pValorIcms = @vValorIcms,
		@pBaseIpi = @vBaseIpi,
		@pAliquotaIpi = @vAliquotaIpi,
		@pValorIpi = @vValorIpi,
		@pNomeProduto = @vNomeProduto,
		@pCodigoProduto = @vCodigoProduto,
		@pDesconto = @vDesconto

	SET @vCount = @vCount + 1
END