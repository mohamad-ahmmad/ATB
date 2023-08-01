using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Extensions
{
    public static class ArrayExtenstions
    {
        public static T[] SubArray<T> (this T[] array, int offset, int length)
        {
            if(array == null)
                throw new ArgumentNullException("array");

            return array.Skip(offset)
                   .Take(length)
                   .ToArray();
        }
    }
}
