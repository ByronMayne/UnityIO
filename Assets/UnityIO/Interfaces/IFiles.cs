using System.Collections.Generic;
using UnityEngine;
using UnityIO.Classes;

namespace UnityIO.Interfaces
{
    public interface IFiles : IList<File>
    {
        List<T> LoadAllofType<T>() where T : Object;
    }
}
