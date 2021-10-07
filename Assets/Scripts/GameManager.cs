using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState state = GameState.Ready;
    private AudioSource sound;
    private HUDController HUD;
    public GameObject ReadyText;

    [Header("Sounds")]
    public AudioClip ClipStart;
    public AudioClip ClipSiren;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        HUD = GameObject.FindGameObjectWithTag("Controller").GetComponent<HUDController>();
        sound.loop = false;
        sound.clip = ClipStart;
        sound.Play();
    }

    void Update()
    {
        if (state == GameState.Ready)
        {
            if (sound.time >= sound.clip.length)
            {
                StartGame();
            }
        }
    }

    void StartGame()
    {
        sound.loop = true;
        sound.clip = ClipSiren;
        sound.Play();

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
}

enum GameState
{
    Ready,
    Play,
    Win,
    Lose
}