using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberUpgradeList : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject memberCardReference;
    public GameObject memberCards;
    
    public GameObject newMoveReference;
    public Text textPrompt;

    public delegate void OnUse();
    public event OnUse onUse;

    public GameObject backButton;

    public void SetupCards(PostMatchReward matchReward)
    {
        backButton.SetActive(true);
        // TO DO, turn this into a separate object and give more details
        var moveName = matchReward.moveReference.moveName;
        textPrompt.text = $"Who should learn {moveName}?";

        foreach (Transform child in memberCards.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach(var member in GameManager.Instance.goodMembers)
        {
            var command = Instantiate(memberCardReference, Vector3.zero, Quaternion.identity, memberCards.transform);
            var upgradeCard = command.GetComponent<MemberUpgradeCard>();
            upgradeCard.SetUI(member, matchReward);
            upgradeCard.onUse += HandleOnUse;
            
        }
    }

    public void HandleOnUse()
    {
        backButton.SetActive(false);
        textPrompt.text = "";
        //onUse();
        foreach (Transform child in memberCards.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
        
    }

}
