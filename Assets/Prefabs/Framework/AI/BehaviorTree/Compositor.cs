using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Compositor : BTNode
{
    LinkedList<BTNode> children = new LinkedList<BTNode>();
    LinkedListNode<BTNode> currentChild = null;

    public void AddChild(BTNode newChild)
    {
        children.AddLast(newChild);
    }

    protected override NodeResult Execute()
    {
        if(children.Count == 0)
        {
            return NodeResult.Success;
        }

        currentChild = children.First;
        return NodeResult.Inprogress;
    }

    protected BTNode GetCurrentChild()
    {
        return currentChild.Value;
    }

    protected bool Next()
    {
        if(currentChild != children.Last)
        {
            currentChild = currentChild.Next;
            return true;
        }
        return false;
    }

    protected override void End()
    {
        if (currentChild == null)
            return;

        currentChild.Value.Abort();
        currentChild = null;
    }

    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);

        foreach(var child in children)
        {
            child.SortPriority(ref priorityConter);
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        foreach (var child in children)
        {
            child.Initialize();
        }
    }

    public override BTNode Get()
    {
        if(currentChild == null)
        {
            if(children.Count!=0)
            {
                return children.First.Value.Get();
            }
            else
            {
                return this;
            }
        }

        return currentChild.Value.Get();
    }
}
