using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Wait : BTNode
{
    float WaitTime = 2f;

    float timeElapsed = 0f;

    public BTTask_Wait(float WaitTime)
    {
        this.WaitTime = WaitTime;
    }

    protected override NodeResult Execute()
    {
        if(WaitTime <= 0)
        {
            return NodeResult.Success;
        }
        Debug.Log($"wait started with duration: {WaitTime}");
        timeElapsed = 0f;
        return NodeResult.Inprogress;
    }

    protected override NodeResult Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= WaitTime)
        {
            Debug.Log("Wait finished");
            return NodeResult.Success;
        }
        //Debug.Log($"Waiting for {timeElapsed}");
        return NodeResult.Inprogress;
    }
}
