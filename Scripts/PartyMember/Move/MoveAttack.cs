using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttack : MemberMove
{
    public int damage = 4;
   public override void DoMove(Member memberToUseMoveOn, Member memberOwner) {
        base.DoMove(memberToUseMoveOn, memberOwner);
        memberToUseMoveOn.health.TakeDamage(damage);
    }
}
