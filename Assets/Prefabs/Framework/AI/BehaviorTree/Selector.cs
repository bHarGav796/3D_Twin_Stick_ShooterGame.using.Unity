using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Compositor
{
    protected override NodeResult Update()
    {
        NodeResult result = GetCurrentChild().UpdateNode();
        if(result == NodeResult.Success)
        {
            return NodeResult.Success;
        }

        if(result == NodeResult.Failure)
        {
            if (Next())
                return NodeResult.Inprogress;
            else
                return NodeResult.Failure;
        }

        return NodeResult.Inprogress;
    }
}
