using Imposto.Core.Domain;
using System.IO;
using System.Xml.Serialization;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        public void EmitirNotaFiscal(NotaFiscal notaFiscal)
        {
            XmlSerializer xs = new XmlSerializer(typeof(NotaFiscal));

            using (StreamWriter wr = new StreamWriter(@"C:\Users\pedro\OneDrive\Documentos\net-lab\TesteImposto\TesteImposto\bin\Debug\notafiscal.xml"))
                xs.Serialize(wr, notaFiscal);
        }
    }
}
