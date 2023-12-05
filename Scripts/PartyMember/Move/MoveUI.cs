using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text moveText;
    public Text moveDescriptionText;

    public MemberMove moveObject;

    public Button buttonReference;

    public Member memberReference;
    
    private void Start() {
        buttonReference = GetComponent<Button>();    
    }
    public void SetUI(MemberMove move, Member member)
    {
        moveObject = move;
        memberReference = member;
        moveText.text = move.moveName;
        moveDescriptionText.text = move.moveDescription;
        
        buttonReference.onClick.AddListener(StartMoveForUI);

        switch(move.moveType)
        {
            case TARGET_TYPE.SELF:
            buttonReference.GetComponent<Image>().color = Color.green;
            break;

            case TARGET_TYPE.SINGLETOALL:
            buttonReference.GetComponent<Image>().color = Color.magenta;
            break;

            case TARGET_TYPE.ALLENEMIES:
            buttonReference.GetComponent<Image>().color = Color.blue;
            break;
        }
    }
    public void StartMoveForUI()
    {
        StartMove(moveObject, memberReference);
    }

    public static void StartMove(MemberMove moveObject, Member memberReference)
    {
        var commandUIReference = GameObject.Find("CommandUI").GetComponent<CommandUI>();
        commandUIReference.Refresh();

        //Create targets.

        switch(moveObject.moveType)
        {
            case TARGET_TYPE.SELF:
            moveObject.DoMove(memberReference, memberReference);
            break;

            case TARGET_TYPE.SINGLETOALL:
            commandUIReference.ShowSingleTargets(moveObject, memberReference);
            break;

            case TARGET_TYPE.ALLENEMIES:
            foreach(var member in GetAllOtherMembers(memberReference))
            {
                moveObject.DoMove(member, memberReference);
            }
            break;
        }

    }
    public static List<Member> GetAllOtherMembers(Member currentMember)
    {
        List<Member> members = new List<Member>();
        var foundMembers = GameObject.FindObjectsOfType<Member>();
        foreach(var member in foundMembers)
        {
            if(member.team != currentMember.team)
            {
                members.Add(member);
            }
        }
        return members;
    }
}
