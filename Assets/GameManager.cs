using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState state = GameState.Ready;
    private AudioSource audioSource;
    private HUDController HUD;
    public GameObject ReadyText;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        HUD = GameObject.FindGameObjectWithTag("Controller").GetComponent<HUDController>();
        PlayIntroMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Ready)
        {
            if (audioSource.time >= audioSource.clip.length)
            {
                StartGame();
            }
        }
    }

    void StartGame()
    {
        // Update game state
        state = GameState.Play;
        
        // Hide ready text
        ReadyText.SetActive(false);

        // Enable PacMan and Ghosts
        GameObject model = GameObject.FindGameObjectWithTag("Model");
        PacManModel pacManModel = model.GetComponent<PacManModel>();
        pacManModel.IsEnabled = true;

        GhostModel ghostModel = model.GetComponent<GhostModel>();
        ghostModel.IsEnabled = true;
    }

    public void WinGame()
    {
        // Update game state
        state = GameState.Win;

        HUD.ShowWinPanel();
    }

    public void LoseGame()
    {
        // Update game state
        state = GameState.Lose;

        HUD.ShowLosePanel();
    }

    void PlayIntroMusic()
    {
        audioSource.Play();
        //audio.time
    }
}

enum GameState
{
    Ready,
    Play,
    Win,
    Lose
}