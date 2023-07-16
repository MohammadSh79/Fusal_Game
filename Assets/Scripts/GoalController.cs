using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    GameObject ball;
    [SerializeField]
    Text score_team_red;
    [SerializeField]
    Text score_team_blue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            // Event ball goaled
            EventManager.current.ballGoaled();

            // Add score to the team
            if (tag == "GoalTeamRed" && !EventManager.current.gamePaused)
            {
                int currentScore = int.Parse(score_team_red.text);
                currentScore++;
                score_team_red.text = currentScore.ToString();
            }
            else if (tag == "GoalTeamBlue" && !EventManager.current.gamePaused)
            {
                int currentScore = int.Parse(score_team_blue.text);
                currentScore++;
                score_team_blue.text = currentScore.ToString();
            }
            EventManager.current.gamePaused = true;
        }
    }
}
