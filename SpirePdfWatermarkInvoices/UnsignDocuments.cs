using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirePdfWatermarkInvoices
{
    internal class UnsignDocuments
    {
        
        public void Unsign(string directoryPath) 
        {
            IEnumerable<string> invoices = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            UnsignDocument unsigner = new UnsignDocument();

            foreach (string invoice in invoices)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(invoice);

                using (PdfDocument pdf = new PdfDocument())
                {
                    pdf.LoadFromFile(invoice);
                    if (pdf.Security.OriginalEncrypt)
                    {
                        string unsignedPath = Path.Combine(Path.GetDirectoryName(invoice), $"{fileNameWithoutExtension} unsigned.pdf");
                        if (!File.Exists(unsignedPath))
                        {
                            Console.WriteLine($"Unsigning signed document {fileNameWithoutExtension}.pdf");
                            using var unsignedDocument = unsigner.Unsign(pdf);
                            unsignedDocument.SaveToFile(unsignedPath);
                        }
                    }
                }
            }
        }
    }
}
