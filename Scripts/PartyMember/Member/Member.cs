using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team {
    GOOD,
    BAD
}
public class Member : MonoBehaviour
{

    public DamageComponent health;

    public Animator animatorReference;

    public Team team;

    public GameObject[] startingMoves;
    public List<MemberMove> currentMoveList;

    private int maxMoves = 4;
    // Start is called before the first frame update

    public bool canMove = false;

    private CommandUI commandUIReference;
    public string memberName;

    public delegate void OnDoMove();
    public event OnDoMove onDoMove;

    public delegate void OnRefresh();
    public event OnRefresh onRefresh;


    public void HandleDoMove() 
    {
        canMove = false;
        onDoMove();
    }

    public void HandleRefresh()
    {
        if(!health.IsDead)
        {
            canMove = true;
        }        
        onRefresh();
    }

    void Start()
    {
        currentMoveList = new List<MemberMove>();
        foreach(var move in startingMoves)
        {
            currentMoveList.Add(move.GetComponent<MemberMove>());
        }
        while(CanAddMove())
        {
            GameObject go = new GameObject("move");
            go.transform.parent = gameObject.transform;
             MemberMove move = go.AddComponent<MemberMove>();
            currentMoveList.Add(move);
        }

        health.onDeath += HandleOnDeath;
        health.onRevive += HandleOnRevive;

        commandUIReference = GameObject.Find("CommandUI").GetComponent<CommandUI>();
        
        gameObject.name = memberName;
    }

    public List<MemberMove> GetMoves()
    {
        return currentMoveList;
    }
    public void AddMove(MemberMove move)
    {
        currentMoveList.Add(move);
    }

    public bool CanAddMove()
    {
        return currentMoveList.Count < maxMoves;
    }

    public void ReplaceMove(int index, MemberMove move)
    {
        currentMoveList[index] = move;//(currentMoveList[index]);
        //currentMoveList.Add(move);
    }
    public void AddToGameList()
    {
        GameManager.Instance.AddMember(this);
    }

    public void HandleOnDeath()
    {
        animatorReference.SetBool("IsDead", true);
    }
    public void HandleOnRevive()
    {
        animatorReference.SetBool("IsDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!health.IsDead)
        {
            if(canMove && GameManager.Instance.currentTeam == team && GameManager.Instance.hasGameEnded == false && GameManager.Instance.hasGameStarted == true)
            {
                if (Input.GetMouseButtonDown(0))
                { // if left button pressed...
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit)){
                        if(hit.collider.gameObject)
                        {
                            if(hit.collider.gameObject == this.gameObject)
                            {
                                commandUIReference.ShowCommandUI(this);
                            }
                        }
                    }
                }
            }
        }
 }
}
