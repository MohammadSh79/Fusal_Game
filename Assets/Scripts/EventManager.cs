using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager current;
    public event Action onBallGoaled;
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private AudioSource goalWhistle;
    public bool gamePaused = false;

    [SerializeField]
    private float startTime;
    private float currentTime;
    [SerializeField]
    private Text timerText;

    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        onBallGoaled += playGoalWhistle;
        startGame();
    }

    private void Update()
    {
        if(!this.gamePaused)
        {
            currentTime -= Time.deltaTime;
            setTimeText(currentTime);
            if (currentTime <= 0)
                endGame();
        }
    }

    private void setTimeText(float currentTime)
    {
        int minute = (int)currentTime / 60;
        int second = (int)currentTime % 60;
        String time = minute.ToString("00") + ":" + second.ToString("00");
        timerText.text = time;
    }

    public void startGame()
    {
        currentTime = startTime;
    }

    public void endGame()
    {
        print("Game ended!");
        // Show Winner Screen
    }

    public void ballGoaled()
    {
        if(onBallGoaled != null)
        {
            onBallGoaled();
        }

        Invoke("resetBall", 1);
    }
    private void resetBall()
    {
        ball.transform.position = new Vector3(0, 0.28f, 0.14f);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.gamePaused = false;
    }

    private void playGoalWhistle()
    {
        goalWhistle.Play();
    }
}
