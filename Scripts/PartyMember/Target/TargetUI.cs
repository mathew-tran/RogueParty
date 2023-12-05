using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetUI : MonoBehaviour
{
    public Button buttonReference;
    private Member memberTarget;
    private Member owningMember;

    private MemberMove moveReference;

    public Text targetText;

    private void Start() {
        
    }
    public void SetUI(MemberMove move, Member memberToTarget, Member memberOwner)
    {
        if(memberOwner.team != memberToTarget.team)
        {
            buttonReference.GetComponent<Image>().color = Color.red;
        }
        else
        {
            buttonReference.GetComponent<Image>().color = Color.green;
        }
        moveReference = move;
        targetText.text = memberToTarget.memberName;
        buttonReference.onClick.AddListener(PerformAction);
        owningMember = memberOwner;
        memberTarget = memberToTarget;
    }

    public void PerformAction()
    {   
        moveReference.DoMove(memberTarget, owningMember);
        var commandUIReference = GameObject.Find("CommandUI").GetComponent<CommandUI>();
        commandUIReference.Refresh();
    }
}
