using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_AttackTarget : BTNode
{
    BehaviorTree tree;
    string targetKey;
    GameObject target;
    public BTTask_AttackTarget(BehaviorTree tree, string targetKey)
    {
        this.tree = tree;
        this.targetKey = targetKey;
    }
    protected override NodeResult Execute()
    {
        if (!tree || tree.Blackboard == null || !tree.Blackboard.GetBlackboardData(targetKey, out target))
            return NodeResult.Failure;

        IBehaviorTreeInterface BehaviorInterface = tree.GetBehaviorTreeInterface();
        if (BehaviorInterface == null)
            return NodeResult.Failure;

        BehaviorInterface.AttackTarget(target);
        return NodeResult.Success;
    }
}
