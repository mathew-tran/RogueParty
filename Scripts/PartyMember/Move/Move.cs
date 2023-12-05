using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum TARGET_TYPE
{
    SELF, // Do something to self
    SINGLETOALL, // Do something to one target
   // SINGLEFORENEMIES, // Do something to on target enemy

    // SINGLEFORALLIES, // Do something to on target ally
    ALLENEMIES, // Do something to all enemies
    // ALLALLIES, // Do Something to all allies

}
public class MemberMove : MonoBehaviour{
    public string moveName;
    public TARGET_TYPE moveType;

    public string moveDescription;

    public virtual void DoMove(Member memberToUseMoveOn, Member memberOwner) {
        memberOwner.HandleDoMove();
    }
    public void DoMove(List<Member> membersToUseMoveOn, Member memberOwner)
    {
        foreach(var member in membersToUseMoveOn)
        {
            DoMove(member, memberOwner);
        }
    }

}
