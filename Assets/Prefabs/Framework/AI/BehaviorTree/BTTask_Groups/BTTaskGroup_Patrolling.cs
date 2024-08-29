using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTaskGroup_Patrolling : BTTask_Group
{
    float acceptableDistance;
    public BTTaskGroup_Patrolling(BehaviorTree tree, float acceptableDistance = 3) : base(tree)
    {
        this.acceptableDistance = acceptableDistance;
    }

    protected override void ConstructTree(out BTNode Root)
    {
        Sequencer patrollingSeq = new Sequencer();

        BTTask_GetNextPatrolPoint getNextPatrolPoint = new BTTask_GetNextPatrolPoint(tree, "PatrolPoint");
        BTTask_MoveToLoc moveToPatrolPoint = new BTTask_MoveToLoc(tree, "PatrolPoint", acceptableDistance);
        BTTask_Wait waitAtPatrolPoint = new BTTask_Wait(2f);

        patrollingSeq.AddChild(getNextPatrolPoint);
        patrollingSeq.AddChild(moveToPatrolPoint);
        patrollingSeq.AddChild(waitAtPatrolPoint);

        Root = patrollingSeq;
    }
}
