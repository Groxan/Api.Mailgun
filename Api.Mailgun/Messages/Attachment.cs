using System;
using System.IO;

namespace Api.Mailgun
{
    /// <summary>
    /// Email attachment
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Attachment binary data
        /// </summary>
        public byte[] Data
        { get; private set; }

        /// <summary>
        /// Attachment file info
        /// </summary>
        public FileInfo File
        { get; private set; }

        /// <summary>
        /// Attachment file name
        /// </summary>
        public string FileName
            => File.Name;

        /// <summary>
        /// Create an instanse of the Attachment
        /// </summary>
        /// <param name="path">Path to attachment file</param>
        public Attachment(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            File = new FileInfo(path);
            Data = System.IO.File.ReadAllBytes(path);
        }

        /// <summary>
        /// Creates an instanse of the Attachment
        /// </summary>
        /// <param name="file">Attachment file info</param>
        public Attachment(FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            File = file;
            Data = System.IO.File.ReadAllBytes(file.FullName);
        }

        /// <summary>
        /// Creates an instanse of the Attachment
        /// </summary>
        /// <param name="path">Path to attachment file</param>
        /// <param name="data">Attachment binary data</param>
        public Attachment(string path, byte[] data)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            File = new FileInfo(path);
            Data = data;
        }

        /// <summary>
        /// Creates an instanse of the Attachment
        /// </summary>
        /// <param name="file">Attachment file info</param>
        /// <param name="data">Attachment binary data</param>
        public Attachment(FileInfo file, byte[] data)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            File = file;
            Data = data;
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
