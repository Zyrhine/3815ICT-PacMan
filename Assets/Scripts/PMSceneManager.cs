using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The manager for the scenes in the game.
/// </summary>
public class PMSceneManager : MonoBehaviour
{
    public static bool isRandomMaze = true;

    public static void SetRandom(bool value) { isRandomMaze = value; }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadMaze()
    {
        if (isRandomMaze)
        {
            SceneManager.LoadScene("RandomMaze");
        } else
        {
            SceneManager.LoadScene("PresetMaze");
        }
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
