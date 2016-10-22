using System;
using UnityIO.Interfaces;
using UnityEngine;

namespace UnityIO.Classes
{
    public class File : IFile
    {
        public IDirectory Directory
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public IFile Duplicate()
        {
            throw new NotImplementedException();
        }

        public IFile Duplicate(string newName)
        {
            throw new NotImplementedException();
        }

        public void Rename(string name)
        {
            throw new NotImplementedException();
        }
    }
}
