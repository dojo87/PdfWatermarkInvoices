using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirePdfWatermarkInvoices
{
    internal class ConcatDocuments
    {
        public async Task ConcatInDirectoryAsync(string directory, string year)
        {
            //"pdftk *.pdf cat output 022023.pdf"

            var directories = Directory.GetDirectories(directory).ToList();
            List<Task> tasks = new List<Task>();
           

            foreach (var subDirectory in directories)
            {
                string month = Path.GetFileName(subDirectory);
                string name = $"{month}{year}.pdf";
                string fullPdfPath = Path.Combine(Path.GetFullPath(directory), name);
                Console.Write($"Concatenating directory {subDirectory} into {name}");
                var process = Process.Start(new ProcessStartInfo("pdftk.exe", $"*.pdf cat output \"{fullPdfPath}\"") { WorkingDirectory = subDirectory });
                tasks.Add(process.WaitForExitAsync());
            }

            await Task.WhenAll(tasks.ToArray());

            Console.Write($"Concatenating full file {directory} into {year}.pdf");
            var fullProcess = Process.Start(new ProcessStartInfo("pdftk.exe", $"*.pdf cat output \"{year}.pdf\"") { WorkingDirectory = directory });
            await fullProcess.WaitForExitAsync();
        }
    }
}
