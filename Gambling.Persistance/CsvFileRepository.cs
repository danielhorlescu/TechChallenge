using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Gambling.Persistance
{
    public class CsvFileRepository : IFileRepository
    {
        public List<T> LoadList<T>(string filePath, Func<T, bool> predicate = null)
        {
            TextReader textReader = File.OpenText(filePath);
            IEnumerable<T> records = new CsvReader(textReader).GetRecords<T>();
            if (predicate == null)
            {
                return records.ToList();
            }
            return records.Where(predicate).ToList();
        }
    }
}