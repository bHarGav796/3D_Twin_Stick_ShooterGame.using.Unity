using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTaskGroup_AttackTarget : BTTask_Group
{
    float moveAcceptableDistance;
    float rotationAcceptableRaidus;
    float attackCooldownDuration;
    public BTTaskGroup_AttackTarget(BehaviorTree tree, float moveAcceptableDistance = 2f, float rotationAcceptableRaidus = 10f, float attackCooldownDuration = 0) : base(tree)
    {
        this.moveAcceptableDistance = moveAcceptableDistance;
        this.rotationAcceptableRaidus = rotationAcceptableRaidus;
        this.attackCooldownDuration = attackCooldownDuration;
    }

    protected override void ConstructTree(out BTNode Root)
    {
        Sequencer attackTargetSeq = new Sequencer();
        BTTask_MoveToTarget moveToTarget = new BTTask_MoveToTarget(tree, "Target", moveAcceptableDistance);

        BTTask_RotateTowardsTarget rotateTowardsTarget = new BTTask_RotateTowardsTarget(tree, "Target", rotationAcceptableRaidus);
        BTTask_AttackTarget attackTarget = new BTTask_AttackTarget(tree, "Target");
        CooldownDecorator attackCooldownDecorator = new CooldownDecorator(tree, attackTarget, attackCooldownDuration);

        attackTargetSeq.AddChild(moveToTarget);
        attackTargetSeq.AddChild(rotateTowardsTarget);
        attackTargetSeq.AddChild(attackCooldownDecorator);

        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(tree,
                                                                            attackTargetSeq,
                                                                            "Target",
                                                                            BlackboardDecorator.RunCondition.KeyExists,
                                                                            BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                            BlackboardDecorator.NotifyAbort.both
                                                                            );

        Root = attackTargetDecorator;
    }
}
