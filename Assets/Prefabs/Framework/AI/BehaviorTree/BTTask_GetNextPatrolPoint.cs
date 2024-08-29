using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_GetNextPatrolPoint : BTNode
{
    PatrollingComponent patrollingComp;
    BehaviorTree tree;
    string patrolPointKey;
    public BTTask_GetNextPatrolPoint(BehaviorTree tree, string patrolPointKey)
    {
        patrollingComp = tree.GetComponent<PatrollingComponent>();
        this.tree = tree;
        this.patrolPointKey = patrolPointKey;
    }

    protected override NodeResult Execute()
    {
        if(patrollingComp!=null && patrollingComp.GetNextPatrolPoint(out Vector3 point))
        {
            tree.Blackboard.SetOrAddData(patrolPointKey, point);
            return NodeResult.Success;
        }

        return NodeResult.Failure;
    }
}
