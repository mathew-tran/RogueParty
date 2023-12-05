using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostMatchRewards : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> rewards;

    public GameObject[] possibleRewards;
    
    public MemberUpgradeList listReference;    

    public GameObject rewardButtonReference;

    public GameObject rewardButtons;

    public delegate void OnFinishPostMatchRewards();
    public event OnFinishPostMatchRewards onFinishPostMatchRewards;

    public void PopulateRewards()
    {
        rewards = new List<GameObject>();
        for(int i = 0; i < Random.Range(2, 4); ++i)
        {
            rewards.Add(possibleRewards[Random.Range(0, possibleRewards.Length)]);
        }
    }
    public void SetUI()
    {
        gameObject.SetActive(true);
        foreach (Transform child in rewardButtons.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
        for(int i = 0; i < rewards.Count; ++i)
        {
            if(rewards[i] != null)
            {
                var button = Instantiate(rewardButtonReference, Vector3.zero, Quaternion.identity, rewardButtons.transform);
                var reward =  button.GetComponent<PostMatchReward>();
                reward.SetUI(rewards[i].GetComponent<MemberMove>(), this, i);
                reward.onUsePostMatchReward += HandleOnUsePostMatchReward;
            }  
        }        
    }
    public void HandleOnUsePostMatchReward(int index)
    {
        rewards.RemoveAt(index);
        SetUI();
    }

    public void Close()
    {
        onFinishPostMatchRewards();
        gameObject.SetActive(false);

    }
}
