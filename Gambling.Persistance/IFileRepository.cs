using System;
using System.Collections.Generic;

namespace Gambling.Persistance
{
    public interface IFileRepository
    {
        List<T> LoadList<T>(string filePath, Func<T, bool> predicate = null);
    }
}