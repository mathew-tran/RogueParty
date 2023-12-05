using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostMatchReward : MonoBehaviour
{
    public Text text;
    public MemberMove moveReference;
    public int rewardIndex;

    public Button buttonReference;

    public PostMatchRewards rewardsReference;

    public delegate void OnUsePostMatchReward(int index);
    public event OnUsePostMatchReward onUsePostMatchReward;

    public void SetUI(MemberMove move, PostMatchRewards reference, int rewardsIndex)
    {
        moveReference = move;
        text.text = moveReference.moveName;
        buttonReference.onClick.RemoveAllListeners();
        buttonReference.onClick.AddListener(HandleOnClick);
        rewardsReference = reference;
        rewardIndex = rewardsIndex;
    }


    public void UseReward()
    {
        onUsePostMatchReward(rewardIndex);
    }
    public void HandleOnClick()
    {
        rewardsReference.listReference.SetupCards(this);
        
    }
}
