using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages main states of game
public class GameManager : MonoBehaviour
{
    [SerializeField] GameInput gameInput;

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private State state;

    private float waitingToStartTimer = 1f;

    private float countdownToStartTimer = 3f;

    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 60f;

    private bool isGamePaused = false;

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }


    private void Start()
    {
        //when game paused...
        gameInput.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Awake()
    {
        //when game loads  = wait to start stae
        state = State.WaitingToStart;
        
    }

    //handles state - waits to start then starts countdown (3,2,1) then game is in Playing state and then after 60 secs - game over
    private void Update()
    {
        
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    
                    //so that you can count down
                    gamePlayingTimer = gamePlayingTimerMax;
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
    }

    //method used by other classes to see if state is Game Playing
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    //method used by other classes to see if state is countdowning
    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownToStart;
    }

    //gets 3,2,1 timer (but not normalised)
    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer; 
    }

    //method used by other classes to see if state if Game over
    public bool IsGameOver()
    {
        
        return state == State.GameOver;
        
    }

    //method used by other classes to see if state is Game waiting to start
    public bool IsGameWaitingToStart()
    {

        return state == State.WaitingToStart;

    }
    
    //normalises weird timer number like (0.333) to normal 3,2,1 numbers
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public float GetCountdownToGameOver()
    {
        return gamePlayingTimer;
    }

    public void TogglePauseGame()
    {
        //basically flip state
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            //time stop
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //time normal
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        
        
    }
}


