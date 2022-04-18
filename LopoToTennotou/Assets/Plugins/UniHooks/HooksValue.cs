using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniHooks
{
    public class HooksValue<T>
    {
        public T Value { get; internal set; }

        internal int Id { get; set; }

        internal HooksValue(T initValue ,int id)
        {
            Value = initValue;
            Id = id;
        }
    }
}
