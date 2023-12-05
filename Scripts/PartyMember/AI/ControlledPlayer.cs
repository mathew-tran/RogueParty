using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlledPlayer : MonoBehaviour
{
    public List<Member> members;
    
    public void Setup()
    {
        members = GameManager.Instance.badMembers;
    }
    public void TakeTurn()
    {
        StartCoroutine(RunTurn());
    }
    private IEnumerator RunTurn()
    {
        yield return new WaitForSeconds(1.0f); // Need to wait for refresh, maybe find a better way

        foreach(var member in members)
        {
            if(member.canMove)
            {
                var moveToDo = GetRandomMove(member);            
                ExecuteMove(moveToDo, member);    
                yield return new WaitForSeconds(1.0f);           
            }
            
        }
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.NextTurn();
    }

    private MemberMove GetRandomMove(Member memberReference)
    {
        var moves = new List<MemberMove>();
        foreach(var move in memberReference.GetMoves())
        {
            if(!string.IsNullOrEmpty(move.moveName))
            {
                moves.Add(move);
            }
        }

        return moves[Random.Range(0, moves.Count)];
    }
    private void ExecuteMove(MemberMove move, Member moveOwner)
    {
        switch(move.moveType)
        {
            case TARGET_TYPE.SELF:
            move.DoMove(moveOwner, moveOwner);
            break;

            case TARGET_TYPE.SINGLETOALL:
            move.DoMove(CommandUI.GetSingleTargets(move, moveOwner)[0], moveOwner);
            break;

            case TARGET_TYPE.ALLENEMIES:
            move.DoMove(CommandUI.GetSingleTargets(move, moveOwner), moveOwner);
            break;

        }
    }
}

