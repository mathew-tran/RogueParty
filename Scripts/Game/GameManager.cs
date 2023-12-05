using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public List<Member> goodMembers;
    public List<Member> badMembers;

    public GameObject[] badguyPool;
    public int badGuyPoolIndex = 0;

    public Transform badguySpawn;

    public Team currentTeam;
    public ControlledPlayer AIPlayer;

    public GameObject nextButton;

    public delegate void OnTeamChange();
    public event OnTeamChange onTeamChange;

    public PostMatchRewards rewards;

    public delegate void OnGameEnd();
    public event OnGameEnd onGameEnd;
    

    public bool hasGameEnded = false;
    public bool hasGameStarted = false;
    public Team winningTeam;

    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

   
    void Start()
    {   
        rewards.onFinishPostMatchRewards += StartNextRound;
        StartNextRound();
    }

     public void StartNextRound()
    {
        if(badGuyPoolIndex >= badguyPool.Length)
        {
            Debug.Log("Game complete!");
        }
        else
        {
            // Assumes enemies are cleaned up already.
            Instantiate(badguyPool[badGuyPoolIndex], badguySpawn.position, badguySpawn.rotation, badguySpawn);
            badGuyPoolIndex++;
            Setup();
        }
    }

    public void Setup() 
    {
        goodMembers = new List<Member>();
        badMembers = new List<Member>();
        currentTeam = Team.BAD;   
        hasGameStarted = false;
        hasGameEnded = false;

        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.0f);
        var members = GameObject.FindObjectsOfType<Member>();
        foreach(var member in members)
        {
            GameManager.Instance.AddMember(member);
        }
        hasGameStarted = true;
        AIPlayer.Setup();
        NextTurn();
    }

    public void NextTurn()
    {
        if(!hasGameEnded)
        {
            var commandUIReference = GameObject.Find("CommandUI").GetComponent<CommandUI>();
            commandUIReference.Refresh();
            if(currentTeam == Team.GOOD)
            {
                nextButton.SetActive(false);
                currentTeam = Team.BAD;
                AIPlayer.TakeTurn();
            }
            else
            {
                currentTeam = Team.GOOD;
                nextButton.SetActive(true);
            }     
            RefreshTeam();       
            onTeamChange();
        }
    }

    public void RefreshTeam()
    {
        if(currentTeam == Team.GOOD)
        {
            foreach(var member in goodMembers)
            {
                member.HandleRefresh();
            }
            foreach(var member in badMembers)
            {
                member.HandleDoMove();
            }
        }
        else
        {
            foreach(var member in goodMembers)
            {
                member.HandleDoMove();
            }
            foreach(var member in badMembers)
            {
                member.HandleRefresh();
            }
        }
    }

    public void AddMember(Member memberReference)
    {
        if(memberReference.team == Team.GOOD)
        {
            goodMembers.Add(memberReference);
        }
        else
        {
            badMembers.Add(memberReference);
        }
        memberReference.health.onDeath += DecideWinner;
    }

    public bool IsTeamAlive(List<Member> teamMembers)
    {
        foreach(var member in teamMembers)
        {
            if(member.health.IsDead == false)
            {
                return true;
            }
        }
        return false;
    }

    public void DecideWinner()
    {
       StopCoroutine(WaitToDecideWinner());
       StartCoroutine(WaitToDecideWinner());
    }
    public IEnumerator WaitToDecideWinner()
    {
        yield return new WaitForSeconds(5.0f);
        if(!hasGameEnded)
        {        
            var goodGuysAlive = IsTeamAlive(goodMembers);
            if(!goodGuysAlive)
            {
                hasGameEnded = true;
                winningTeam = Team.BAD;
                onGameEnd();            
            }
            var badGuysAlive = IsTeamAlive(badMembers);
            if(!badGuysAlive)
            {
                hasGameEnded = true;
                winningTeam = Team.GOOD;
                GivePlayerRewards();
                //onGameEnd();
            }
        }
    }

    public void GivePlayerRewards()
    {
        rewards.gameObject.SetActive(true);
        rewards.PopulateRewards();
        rewards.SetUI();
        foreach (Transform child in badguySpawn.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach(var member in goodMembers)
        {
            member.canMove = false;
            member.HandleRefresh();
        }
    }

    public void Restart()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
    }

   
}
