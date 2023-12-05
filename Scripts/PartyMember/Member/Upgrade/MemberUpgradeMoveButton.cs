using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberUpgradeMoveButton : MonoBehaviour
{
    public Text moveText;
 
    public Member memberToUpgradeReference;
    public int moveIndex;

    public Button buttonReference;

    public MemberMove newMoveReference;

    public delegate void OnUse();
    public event OnUse onUse;

    public bool hasConfirmed = false;

    // Start is called before the first frame update
    public void SetUI(Member memberReference, int index)
    {
        var moveReference = memberReference.GetMoves()[index];
        moveIndex = index;
        moveText.text = moveReference.moveName;
        memberToUpgradeReference = memberReference;

        buttonReference.onClick.AddListener(HandleOnCick);
    }

    public IEnumerator GoBackToDefault()
    {
        yield return new WaitForSeconds(3.0f);
        var moveReference = memberToUpgradeReference.GetMoves()[moveIndex];
        moveText.text = moveReference.moveName;
        hasConfirmed = false;
    }
    public void HandleOnCick()
    {

        if(!hasConfirmed)
        {
            hasConfirmed = true;
            moveText.text = "Click Again To Confirm";
            StopCoroutine(GoBackToDefault());
            StartCoroutine(GoBackToDefault());
        }
        else
        {
            // TODO add a check if button already has a move in there.
            memberToUpgradeReference.ReplaceMove(moveIndex, newMoveReference);
            onUse();
            SetUI(memberToUpgradeReference, moveIndex);
        }
       
    }
}
