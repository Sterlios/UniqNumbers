using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniqNumbers.DataHandler
{
    class FileHandler : IHandler
    {
        private StreamReader _reader;

        public void Open(string file)
        {
            if (_reader != null)
                Close();

            _reader = new StreamReader(file);
        }

        public string ReadElement()
        {
            if (_reader == null)
                return null;

            return _reader.ReadLine();
        }

        public void Close()
        {
            _reader.Close();
            _reader.Dispose();

            _reader = null;
        }
    }
}
