using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    [SerializeField] protected abstract ElementTypes Type { get; }

    protected abstract HashSet<Type> Beats { get; }
    protected abstract HashSet<Type> WeakTo { get; }

    public void React(Element other)
    {
        if (Beats.Contains(other.GetType()))
        {
            OnWin(other);
        }
        else if (WeakTo.Contains(other.GetType()))
        {
            OnLose(other);
        }
        else
        {
            OnNeutral(other);
        }
    }

    protected virtual void OnWin(Element other)
    {
    }

    protected virtual void OnLose(Element other)
    {
    }

    protected virtual void OnNeutral(Element other)
    {
    }
}
