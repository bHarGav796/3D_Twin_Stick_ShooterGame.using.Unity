using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BTTask_MoveToLoc : BTNode
{
    NavMeshAgent agent;
    string locKey;
    Vector3 loc;
    float acceptableDistance = 1f;
    BehaviorTree tree;

    public BTTask_MoveToLoc(BehaviorTree tree, string locKey, float acceptableDistance = 1f)
    {
        this.tree = tree;
        this.locKey = locKey;
        this.acceptableDistance = acceptableDistance;
    }

    protected override NodeResult Execute()
    {
        Blackboard blackboard = tree.Blackboard;
        if(blackboard == null || !blackboard.GetBlackboardData(locKey, out loc))
            return NodeResult.Failure;

        agent = tree.GetComponent<NavMeshAgent>();
        if(agent == null)
            return NodeResult.Failure;

        if(IsLocInAcceptableDistance())
            return NodeResult.Success;

        agent.SetDestination(loc);
        agent.isStopped = false;
        return NodeResult.Inprogress;
    }

    protected override NodeResult Update()
    {
        if(IsLocInAcceptableDistance())
        {
            agent.isStopped = true;
            return NodeResult.Success;
        }
        return NodeResult.Inprogress;
    }

    private bool IsLocInAcceptableDistance()
    {
        return Vector3.Distance(loc, tree.transform.position) <= acceptableDistance;
    }

    protected override void End()
    {
        agent.isStopped = true;
        base.End();
    }
}
