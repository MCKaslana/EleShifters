using System;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : Element
{
    protected override ElementTypes Type => throw new NotImplementedException();

    protected override HashSet<Type> Beats => throw new NotImplementedException();

    protected override HashSet<Type> WeakTo => throw new NotImplementedException();

    public void ActivateReaction(Element opposingElement)
    {

    }
}
