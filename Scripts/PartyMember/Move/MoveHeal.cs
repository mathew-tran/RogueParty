using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHeal : MemberMove
{
    public int amount = 4;
   public override void DoMove(Member memberToUseMoveOn, Member memberOwner) {
        base.DoMove(memberToUseMoveOn, memberOwner);
        memberToUseMoveOn.health.GiveHealth(amount);
    }
}
