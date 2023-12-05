using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMoveUI : MonoBehaviour
{
    public GameObject moveToAdd;
    public Button moveButton;

    // Start is called before the first frame update

    private void Start() {
        SetUI();    
    }

    public void SetUI()
    {
        moveButton.onClick.AddListener(HandleButtonClick);
    }

    public void HandleButtonClick()
    {
        GameObject.Find("Bill").GetComponent<Member>().AddMove(moveToAdd.GetComponent<MemberMove>());
    }
}
