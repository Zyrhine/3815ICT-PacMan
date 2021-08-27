using UnityEngine;
using UnityEngine.SceneManagement;

public class PMSceneManager : MonoBehaviour
{
    public static bool isRandomMaze = false;

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
