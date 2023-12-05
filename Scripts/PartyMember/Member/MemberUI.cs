using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Member memberReference;
    public GameObject actionToken;

    public Text nameText;

    public GameObject playerCanvas;

    public Transform backTransform;

    public Transform frontTransform;
    
    public void UpdateUI()
    {
        actionToken.SetActive((memberReference.canMove));
    }
    void Start()
    {
        memberReference.onDoMove += UpdateUI;
        memberReference.onRefresh += UpdateUI;

        nameText.text = memberReference.memberName;

        if(memberReference.team == Team.BAD)
        {
            playerCanvas.transform.position = frontTransform.position;
            playerCanvas.transform.rotation = frontTransform.rotation;
            playerCanvas.transform.localScale = frontTransform.localScale;
        }
        else
        {
            playerCanvas.transform.position = backTransform.position;
            playerCanvas.transform.rotation = backTransform.rotation;
            playerCanvas.transform.localScale = backTransform.localScale;
        }
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
