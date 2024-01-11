# PdfWatermarkInvoices
This little command line app intends to fix book keeping for IFIRMA 
The book keeping KPIR in IFIRMA is not intended to be stored digitaly, however it is allowed by law.

By preparing invoices in a convention manner along with KPIR montly downloads, this will use Spire.NET (free version) and watermark all the invoices with "{number}/{month}/{year}" text according to the naming convention of the invoice file:
01-01_whatever_invoice.pdf
01-02_some_other.pdf
01-03....

{month}-{invoiceNumberInKPIR}...

For KPIR pdfs generated by IFIRMA it will add the page number for all provided.

