using System;
using UnityEngine;

namespace tomfulghum.Optional
{
    [Serializable]
    public struct Optional<T>
    {
        [SerializeField] bool enabled;
        [SerializeField] T value;

        public Optional(T initialValue)
        {
            enabled = true;
            value = initialValue;
        }

        public bool Enabled => enabled;
        public T Value => value;
    }
}
