using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Gambling.Persistance
{
    public class CsvFileRepository : IFileRepository
    {
        public List<T> LoadList<T>(string filePath)
        {
            TextReader textReader = File.OpenText(filePath);
            return new CsvReader(textReader).GetRecords<T>().ToList();
        }
    }
}