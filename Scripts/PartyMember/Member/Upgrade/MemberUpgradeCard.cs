using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberUpgradeCard : MonoBehaviour
{

    public Text memberName;

    public GameObject moves;

    public GameObject moveButtonReference;
    
    public delegate void OnUse();
    public event OnUse onUse;


    public void SetUI(Member member, PostMatchReward matchReward)
    {
        memberName.text = member.memberName;
        foreach (Transform child in moves.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
        for(int i = 0; i < member.GetComponent<Member>().GetMoves().Count; ++i)
        {
            var button = Instantiate(moveButtonReference, Vector3.zero, Quaternion.identity, moves.transform);
            var upgradeMove =  button.GetComponent<MemberUpgradeMoveButton>();
            upgradeMove.SetUI(member, i);
            upgradeMove.onUse += HandleOnUse;
            upgradeMove.onUse += matchReward.UseReward;
            upgradeMove.newMoveReference = matchReward.moveReference;
        }        
    }

    public void HandleOnUse()
    {
        onUse();
        
         foreach (Transform child in moves.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
