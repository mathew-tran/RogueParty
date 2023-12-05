using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager managerReference;
    public Text currentTeamText;
    public Text winText;

    private void Start() {
        managerReference.onTeamChange += UpdateTeamTextUI;
        managerReference.onGameEnd += UpdateEndGameUI;
        UpdateTeamTextUI();
    }
    public void UpdateTeamTextUI()
    {
        currentTeamText.text = managerReference.currentTeam.ToString();
    }
    public void UpdateEndGameUI()
    {
        winText.text = $"{managerReference.winningTeam.ToString()} team has won";
    }
}
