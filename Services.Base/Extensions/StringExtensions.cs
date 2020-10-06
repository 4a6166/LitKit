using System;
using System.IO;
using System.Text;

namespace Tools.Extensions
{
    public static class StringExtensions
    {
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string RemoveLastCharacter(this String instr)
        {
            return instr.Substring(0, instr.Length - 1);
        }
        public static string RemoveLast(this String instr, int number)
        {
            return instr.Substring(0, instr.Length - number);
        }
        public static string RemoveFirstCharacter(this String instr)
        {
            return instr.Substring(1);
        }
        public static string RemoveFirst(this String instr, int number)
        {
            return instr.Substring(number);
        }


        public static Stream ToStream(this string str)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            //byte[] byteArray = Encoding.ASCII.GetBytes(str);
            return new MemoryStream(byteArray);
        }
        public static string ToString(this Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        /// <summary>
        /// Copy from one stream to another.
        /// Example:
        /// using(var stream = response.GetResponseStream())
        /// using(var ms = new MemoryStream())
        /// {
        ///     stream.CopyTo(ms);
        ///      // Do something with copied data
        /// }
        /// </summary>
        /// <param name="fromStream">From stream.</param>
        /// <param name="toStream">To stream.</param>
        public static void CopyTo(this Stream fromStream, Stream toStream)
        {
            if (fromStream == null)
                throw new ArgumentNullException("fromStream");
            if (toStream == null)
                throw new ArgumentNullException("toStream");
            var bytes = new byte[8092];
            int dataRead;
            while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)
                toStream.Write(bytes, 0, dataRead);
        }

        /// <summary>
        /// Returns null if string is null or whitespace. Use with ? operator.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IsNullOrWhiteSpace(this string str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            else return str;
        }

        /// <summary>
        /// Returns null if string is null or empty. Use with ? operator.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IsNullOrEmpty(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }
            else return str;
        }

        
    }
}
