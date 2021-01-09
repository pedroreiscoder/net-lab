using Imposto.Core.Domain;

namespace Imposto.Core.Data
{
    public interface INotaFiscalRepository
    {
        void EmitirNotaFiscalXML(NotaFiscal notaFiscal);
        void EmitirNotaFiscal(NotaFiscal notaFiscal);
    }
}
