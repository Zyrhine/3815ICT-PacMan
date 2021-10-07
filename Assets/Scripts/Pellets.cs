using UnityEngine;
using UnityEngine.Tilemaps;

public class Pellets : MonoBehaviour
{
    private Tilemap tilemap;
    private GameManager manager;
    private PacManController pacManController;
    private GhostController ghostController;
    private AudioSource sound;
    private int counter = 0;

    [Header("Sounds")]
    public AudioClip[] ClipMunch;
    public AudioClip ClipPowerPellet;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        tilemap = GetComponent<Tilemap>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        pacManController = GameObject.FindGameObjectWithTag("Controller").GetComponent<PacManController>();
        ghostController = GameObject.FindGameObjectWithTag("Controller").GetComponent<GhostController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var position = tilemap.WorldToCell(collision.transform.position);
            var tile = tilemap.GetTile(position);
            if (tile)
            {
                if (tile.name == "PelletS")
                {
                    // Regular pellet
                    counter = 1 - counter;
                    sound.PlayOneShot(ClipMunch[counter]);
                    pacManController.AddScore();

                }
                else if (tile.name == "PelletL")
                {
                    // Power pellet
                    sound.PlayOneShot(ClipPowerPellet);
                    ghostController.SetVulnerable();
                }

                // Remove the tile
                tilemap.SetTile(position, null);

                // Check if all pellets were collected
                if (tilemap.GetUsedTilesCount() == 0)
                {
                    manager.WinGame();
                }
            }
        }
    }
}
