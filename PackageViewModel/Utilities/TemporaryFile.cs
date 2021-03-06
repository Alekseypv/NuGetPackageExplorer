﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet.Packaging;

namespace PackageExplorerViewModel.Utilities
{
    public class TemporaryFile : IDisposable
    {
        public TemporaryFile(Stream stream, string extension)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (string.IsNullOrWhiteSpace(extension) || extension[0] != '.')
                throw new ArgumentException("message", nameof(extension));

            FileName = Path.GetTempFileName() + extension;

            stream.CopyToFile(FileName);
        }

        public string FileName { get; }
        
        bool disposed;

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                try
                {
                    File.Delete(FileName);
                }
                catch // best effort
                {
                }
            }
        }

    }
}
