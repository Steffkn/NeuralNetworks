using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BigData
{
    public class FileManager
    {
        public IEnumerable<string> ReadLine(string filePath)
        {
            string line;

            // Read the file and display it line by line.  
            using (StreamReader file = new StreamReader(@"c:\test.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
