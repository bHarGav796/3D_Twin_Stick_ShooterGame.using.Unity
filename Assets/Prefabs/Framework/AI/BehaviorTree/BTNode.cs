using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeResult
{
    Success,
    Failure,
    Inprogress
}

public abstract class BTNode
{
    public NodeResult UpdateNode()
    {
        //one off thing
        if(!started)
        {
            started = true;
            NodeResult execResult = Execute();
            if(execResult != NodeResult.Inprogress)
            {
                EndNode();
                return execResult;
            }
        }

        //time based
        NodeResult updateResult = Update();
        if(updateResult != NodeResult.Inprogress)
        {
            EndNode();
        }
        return updateResult;
    }

    //override in child class
    protected virtual NodeResult Execute()
    {
        //one off thing
        return NodeResult.Success;
    }

    protected virtual NodeResult Update()
    {
        //time based
        return NodeResult.Success;
    }

    protected virtual void End()
    {
        //reset and clean up
    }

    private void EndNode()
    {
        started = false;
        End();
    }

    public void Abort()
    {
        EndNode();
    }

    bool started = false;
    int priority;

    public int GetPriority()
    {
        return priority;
    }

    public virtual void SortPriority(ref int priorityConter)
    {
        priority = priorityConter++;
        Debug.Log($"{this} has priorty {priority}");
    }

    public virtual void Initialize(){}

    public virtual BTNode Get()
    {
        return this;
    }
}
