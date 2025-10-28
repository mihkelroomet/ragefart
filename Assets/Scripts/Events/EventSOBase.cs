using System;
using UnityEngine;

public class EventSOBase<T> : ScriptableObject
{
    public event Action<T> OnEvent;
    public void Raise(T payload) => OnEvent?.Invoke(payload);
}
