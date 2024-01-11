// See https://aka.ms/new-console-template for more information
using SpirePdfWatermarkInvoices;

Console.WriteLine("Watermark");
Console.WriteLine(" - program app.exe {directoryWithInvoices:string} -invoices {year:string}");
Console.WriteLine(" - program app.exe {directoryWithKpir:string} -kpir {year:string}");

string folderWithInvoices = args.Length > 1? args[1] : "C:/Default/Path/Downloads";
string year = DateTime.Now.Year.ToString();
DocMode docMode = DocMode.Kpir;
IWatermarkDocuments watermarker;


if (args.Length > 2)
{
    if (args[2].ToLower() == DocMode.Invoices.ToString().ToLower() && args.Length > 3)
    {
        year = args[3];
    }
    else if (args[2].ToLower() == DocMode.Kpir.ToString().ToLower()) 
    {
        docMode = DocMode.Kpir;
        year = args[3];
    }
}

switch (docMode)
{
    case DocMode.Kpir:
        watermarker = new WatermarkKpirPages(year, new KpirWatermark());
        break;
    default:
        watermarker = new WatermarkInvoices(year, new InvoiceWatermark());
        break;
}

Console.WriteLine($"Starting watermark {docMode} for directory {folderWithInvoices}");
UnsignDocuments unsignDocuments = new UnsignDocuments();
unsignDocuments.Unsign(folderWithInvoices);

watermarker.Watermark(folderWithInvoices);

ConcatDocuments concater = new ConcatDocuments();
await concater.ConcatInDirectoryAsync(Path.Combine(folderWithInvoices, "output"), year);

Console.WriteLine("Finished");
