//-----------------------------------------------------------------------
// <copyright file="DotNetFileSystemProvider.cs" company="Fubar Development Junker">
//     Copyright (c) Fubar Development Junker. All rights reserved.
// </copyright>
// <author>Mark Junker</author>
//-----------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.Extensions.Options;

namespace FubarDev.FtpServer.FileSystem.DotNet
{
    /// <summary>
    /// A <see cref="IFileSystemClassFactory"/> implementation that uses
    /// the standard .NET functionality to provide file system access.
    /// </summary>
    public class DotNetFileSystemProvider : IFileSystemClassFactory
    {
        private readonly string _rootPath;

        private readonly bool _useUserIdAsSubFolder;

        private readonly int _streamBufferSize;

        private readonly bool _deleteFileOnUploadTimeout;

        private readonly bool _allowNonEmptyDirectoryDelete;

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetFileSystemProvider"/> class.
        /// </summary>
        /// <param name="options">The file system options.</param>
        public DotNetFileSystemProvider([NotNull] IOptions<DotNetFileSystemOptions> options)
        {
            _rootPath = options.Value.RootPath;
            _useUserIdAsSubFolder = options.Value.UseUserIdAsSubFolder;
            _streamBufferSize = options.Value.StreamBufferSize ?? DotNetFileSystem.DefaultStreamBufferSize;
            _allowNonEmptyDirectoryDelete = options.Value.AllowNonEmptyDirectoryDelete;
            _deleteFileOnUploadTimeout = options.Value.DeleteFileOnUploadTimeout;
        }

        /// <inheritdoc/>
        public Task<IUnixFileSystem> Create(string userId, bool isAnonymous)
        {
            var path = _rootPath;
            if (_useUserIdAsSubFolder)
            {
                if (isAnonymous)
                {
                    userId = "anonymous";
                }

                path = Path.Combine(path, userId);
            }

            return Task.FromResult<IUnixFileSystem>(new DotNetFileSystem(path, _allowNonEmptyDirectoryDelete, _deleteFileOnUploadTimeout, _streamBufferSize));
        }
    }
}
