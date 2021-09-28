using UnityEngine;
using UnityEngine.Tilemaps;

public class Pellets : MonoBehaviour
{
    private Tilemap tilemap;
    private GameManager manager;
    private PacManController pacManController;
    private GhostController ghostController;

    // Start is called before the first frame update
    void Start()
    {
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
                    pacManController.AddScore();

                }
                else if (tile.name == "PelletL")
                {
                    // Power pellet
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
