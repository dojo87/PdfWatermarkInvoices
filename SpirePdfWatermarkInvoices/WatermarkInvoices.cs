using Spire.Pdf.Graphics;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirePdfWatermarkInvoices
{
    internal class WatermarkInvoices : IWatermarkDocuments
    {
        public WatermarkInvoices(string year, InvoiceWatermark invoiceWatermarkWriter) 
        {
            Year = year;
            InvoiceWatermarkWriter = invoiceWatermarkWriter;
        }

        public string Year { get; }
        public InvoiceWatermark InvoiceWatermarkWriter { get; }

        public void Watermark(string inDirectory)
        {
            var invoices = Directory.GetFiles(inDirectory,"*",SearchOption.AllDirectories).OrderBy(s => s);
            
            foreach (string invoice in invoices)
            {
                string fileName = Path.GetFileName(invoice);
                using (PdfDocument pdf = new PdfDocument())
                {
                    pdf.LoadFromFile(invoice);
                    if (pdf.Security.OriginalEncrypt)
                    {
                        Console.WriteLine($"Signed {fileName} - skipping");
                        continue;
                    }

                    Console.Write($"Doing {fileName}");
                    string month = fileName.Substring(0, 2);
                    string docNumber = fileName.Substring(3, 2);
                    string watermark = $"{docNumber}/{month}/{Year}";

                    InvoiceWatermarkWriter.Watermark(pdf, watermark);

                    string outputPath = Path.Combine(inDirectory, "output", month, fileName);
                    //Save the changes to another file
                    pdf.SaveToFile(outputPath);
                    Console.WriteLine($" - DONE");
                }
            }
        }
    }
}
