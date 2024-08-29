using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETeamRelation
{
    Friendly,
    Enemy,
    Neutral
}

public interface ITeamInterface
{
    public int GetTeamID() { return -1; }

    public ETeamRelation GetRelationTowards(GameObject other)
    {
        ITeamInterface otherTeamInterface = other.GetComponent<ITeamInterface>();
        if(otherTeamInterface == null)
        {
            return ETeamRelation.Neutral;
        }

        if (otherTeamInterface.GetTeamID() == GetTeamID())
            return ETeamRelation.Friendly;
        else if (otherTeamInterface.GetTeamID() == -1 || GetTeamID() == -1)
            return ETeamRelation.Neutral;

        return ETeamRelation.Enemy;
    }
}
