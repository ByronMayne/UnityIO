using System;

namespace UnityIO.Exceptions
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException(string message) : base(message)
        {

        }
    }

    public class FileAlreadyExistsException : Exception
    {
        public FileAlreadyExistsException(string message) : base(message)
        {

        }
    }

    public class DirectoryNotFoundException : Exception
    {
        public DirectoryNotFoundException(string message) : base(message)
        {

        }
    }

    public class MoveException : Exception
    {
        public static string Format(string message, string from, string to)
        {
            return "Unable to move " + from + " to " + to + " because " + message;
        }

        public MoveException(string message, string from, string to) : base(Format(message, from, to))
        {
        }
    }

    public class RenameException : Exception
    {
        public static string Format(string message, string from, string to)
        {
            return "Unable to rename " + from + " to " + to + " because " + message;
        }

        public RenameException(string message, string from, string to) : base(Format(message, from, to))
        {
        }
    }

    /// <summary>
    /// This exception is thrown when you try to rename or move a directory and one
    /// already exists at that location. 
    /// </summary>
    public class DirectroyAlreadyExistsException : Exception
    {
        public DirectroyAlreadyExistsException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// This exception is thrown when you try to name a file or directory that contains
    /// invalid characters. 
    /// </summary>
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message)
        {
        }
    }
}
