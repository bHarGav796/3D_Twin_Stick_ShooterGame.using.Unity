using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTaskGroup_MoveToLastSeenLoc : BTTask_Group
{
    float acceptableDistance;
    public BTTaskGroup_MoveToLastSeenLoc(BehaviorTree tree, float acceptableDistance = 3) : base(tree)
    {
        this.acceptableDistance = acceptableDistance;
    }

    protected override void ConstructTree(out BTNode Root)
    {
        Sequencer CheckLastSeenLocSeq = new Sequencer();
        BTTask_MoveToLoc MoveToLastSeenLoc = new BTTask_MoveToLoc(tree, "LastSeenLoc", acceptableDistance);
        BTTask_Wait WaitAtLastSeenLoc = new BTTask_Wait(2f);
        BTTask_RemoveBlackboardData removeLastSeenLoc = new BTTask_RemoveBlackboardData(tree, "LastSeenLoc");
        CheckLastSeenLocSeq.AddChild(MoveToLastSeenLoc);
        CheckLastSeenLocSeq.AddChild(WaitAtLastSeenLoc);
        CheckLastSeenLocSeq.AddChild(removeLastSeenLoc);

        BlackboardDecorator CheckLastSeenLocDeocorator = new BlackboardDecorator(tree,
                                                                                 CheckLastSeenLocSeq,
                                                                                 "LastSeenLoc",
                                                                                 BlackboardDecorator.RunCondition.KeyExists,
                                                                                 BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                                 BlackboardDecorator.NotifyAbort.none
                                                                                 );

        Root = CheckLastSeenLocDeocorator;
    }
}
