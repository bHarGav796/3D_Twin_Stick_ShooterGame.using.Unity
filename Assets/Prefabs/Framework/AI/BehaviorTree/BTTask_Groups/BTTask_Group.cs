using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTTask_Group : BTNode
{
    BTNode Root;
    protected BehaviorTree tree;
    public BTTask_Group(BehaviorTree tree)
    {
        this.tree = tree;
    }

    protected abstract void ConstructTree(out BTNode Root);

    protected override NodeResult Execute()
    {
        return NodeResult.Inprogress;
    }

    protected override NodeResult Update()
    {
        return Root.UpdateNode();
    }

    protected override void End()
    {
        Root.Abort();
        base.End();
    }

    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);
        Root.SortPriority(ref priorityConter);
    }

    public override void Initialize()
    {
        base.Initialize();
        ConstructTree(out Root);
    }

    public override BTNode Get()
    {
        return Root.Get();
    }
}
