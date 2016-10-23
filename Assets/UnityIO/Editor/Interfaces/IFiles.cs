using System.Collections.Generic;
using UnityEngine;
using UnityIO.Classes;

namespace UnityIO.Interfaces
{
    public interface IFiles : IList<File>
    {
        IList<T> LoadAllofType<T>() where T : Object;
        IFile FirstOrDefault();
    }


}
