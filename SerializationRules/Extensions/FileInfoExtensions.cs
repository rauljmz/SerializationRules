using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SerializationRules.Extensions
{
    public static class FileInfoExtensions
    {
        const int BytesToRead = sizeof(Int64);
        public static bool Compare(this FileInfo file1, FileInfo file2)
        {
            if (!file1.Exists || !file2.Exists) return false;
            if (file1.Length != file2.Length) return false;

            var iterations = (int)Math.Ceiling((double)file1.Length / BytesToRead);

            using (var fs1 = file1.OpenRead())
            using (var fs2 = file2.OpenRead())
            {
                var one = new byte[BytesToRead];
                var two = new byte[BytesToRead];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BytesToRead);
                    fs2.Read(two, 0, BytesToRead);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }
    }
}