using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 30f;
    private int currentScore = 0;
    private enum State 
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        Target.OnHited += Target_OnHited;
        Target.NotHited += Target_NotHited;
    }

    private void Target_NotHited(int penaltyCost)
    {
        currentScore -= penaltyCost;
        if (currentScore < 0)
        {
            currentScore = 0;
        }
    }

    private void Target_OnHited(int targetScore)
    {
        currentScore += targetScore;   
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }

    
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetCountDownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }

    public int GetScore()
    {
        return currentScore;
    }
}
