using System;

using JetBrains.Annotations;

namespace FubarDev.FtpServer
{
    /// <summary>
    /// Event arguments for a file upload event
    /// </summary>
    public class FileUploadEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadEventArgs"/> class.
        /// </summary>
        /// <param name="connection">The connection of the event</param>
        /// <param name="fileName">The filename of the event</param>
        public FileUploadEventArgs([NotNull] FtpConnection connection, [NotNull] string fileName)
        {
            Connection = connection;
            FileName = fileName;
        }

        /// <summary>
        /// Gets the connection for this event
        /// </summary>
        [NotNull]
        public FtpConnection Connection { get; }

        /// <summary>
        /// Gets the filename for this event
        /// </summary>
        [NotNull]
        public string FileName { get; }
    }
}
