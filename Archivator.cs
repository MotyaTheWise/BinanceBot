using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceBot
{
    class Archivator
    {
        public void FilesToZip(string inputPath, string outputPath)
        {
            var zipFile = $"{outputPath}.zip";

            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(inputPath, Path.GetFileName(inputPath));
            }
            Console.WriteLine($"Archived! Zip path: {outputPath}");
        }
    }
}
