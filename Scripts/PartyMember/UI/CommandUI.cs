using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandUI : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Text textReference;

    public GameObject commandButtons;

    public GameObject commandButtonReference;

    public GameObject targetButtons;
    public GameObject targetButtonReference;
    public void ShowCommandUI(Member memberReference)
    {
        Refresh();
        textReference.text = memberReference.gameObject.name;         

        foreach(var move in memberReference.GetMoves())
        {
            if(move != null)
            {
                var moveComponent = move.GetComponent<MemberMove>();                
                
                if(!string.IsNullOrEmpty(moveComponent.moveName))
                {
                
                    textReference.text += $"\n {moveComponent.moveName}";
                    var command = Instantiate(commandButtonReference, Vector3.zero, Quaternion.identity, commandButtons.transform);
                    command.GetComponent<MoveUI>().SetUI(moveComponent, memberReference);
                }                    
                
            }
        }
    }

    public void Refresh()
    {
        textReference.text = "";
        foreach (Transform child in commandButtons.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in targetButtons.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static List<Member> GetSingleTargets(MemberMove move, Member memberOwner)
    {
         Member[] targets = GameObject.FindObjectsOfType<Member>();
         List<Member> members = new List<Member>();

        foreach(var memberToTarget in targets)
        {
            if(memberOwner != memberToTarget && memberToTarget.health.IsDead == false && memberOwner.team != memberToTarget.team)
            {
                members.Add(memberToTarget);
            }
        }
        return members;
    }
    public void ShowSingleTargets(MemberMove move, Member memberOwner)
    {
        Refresh();
        List<Member> targets = GetSingleTargets(move, memberOwner);
        foreach(var memberToTarget in targets)
        {
            if(memberOwner != memberToTarget && memberToTarget.health.IsDead == false)
            {
                var target = Instantiate(targetButtonReference, Vector3.zero, Quaternion.identity, targetButtons.transform);
                target.GetComponent<TargetUI>().SetUI(move, memberToTarget, memberOwner);
            }
        }
    }

    public void ShowAllTargets()
    {
        Refresh();
    }
}
