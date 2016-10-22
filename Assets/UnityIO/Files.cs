using UnityIO.Interfaces;
using System.Collections.Generic;

namespace UnityIO.Classes
{
    public class Files : List<File>
    {
        public File FirstOrDefault()
        {
            if (Count > 0)
            {
                return this[0];
            }
            else
            {
                return null;
            }
        }
    }
}
