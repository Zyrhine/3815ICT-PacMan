using UnityEngine;

/// <summary>
/// The controller for PacMan.
/// </summary>
public class PacManController : MonoBehaviour
{
    private PacManModel model;
    private GameObject PacMan;
    private Rigidbody2D rb;
    private Animator anim;
    private GameManager manager;
    private HUDController HUD;
    private GhostController ghostController;
    private GameObject RespawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        HUD = GetComponent<HUDController>();
        ghostController = GetComponent<GhostController>();
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<PacManModel>();
        PacMan = GameObject.Find("view/PacMan");
        rb = PacMan.GetComponent<Rigidbody2D>();
        anim = PacMan.GetComponent<Animator>();
        RespawnPoint = GameObject.FindGameObjectWithTag("Respawn");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!model.IsEnabled) return;

        // Get directional input
        if (Application.platform == RuntimePlatform.Android)
        {
            GetTouchInput();
        }
        else
        {
            GetKeyboardInput();
        }

        // If the direction has been changed, change direction if PacMan is able to
        if (model.moveDirection != model.queueDirection)
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, DirectionToVector(model.queueDirection), 0.16f);

            if (!hit || !hit.collider.CompareTag("Maze"))
            {
                model.moveDirection = model.queueDirection;
                anim.SetInteger("Direction", (int)model.moveDirection);
            }
        }

        // Move in the direction
        Vector2 dir = DirectionToVector(model.moveDirection);
        Vector2 pos = rb.position + (dir * model.MoveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
    }

    // Gets the movement direction from input
    public void GetKeyboardInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            model.queueDirection = MoveDirection.Right;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            model.queueDirection = MoveDirection.Down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            model.queueDirection = MoveDirection.Left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            model.queueDirection = MoveDirection.Up;
        }
    }

    public void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record initial touch position.
                    model.touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 endPos = touch.position;
                    Vector2 direction = endPos - model.touchStartPos;
                    direction.Normalize();

                    if (direction.x > 0.5f)
                    {
                        model.queueDirection = MoveDirection.Right;
                    }
                    else if (direction.x < -0.5f)
                    {
                        model.queueDirection = MoveDirection.Left;
                    }
                    else if (direction.y > 0.5f)
                    {
                        model.queueDirection = MoveDirection.Up;
                    }
                    else if (direction.y < -0.5f)
                    {
                        model.queueDirection = MoveDirection.Down;
                    }
                    break;
            }
        }
    }

    // Add score from pellet
    public void AddScore()
    {
        model.Score += 10;
        HUD.UpdateScore(model.Score);
    }

    // Lose a life
    public void CapturePacMan()
    {
        if (model.Lives > 0)
        {
            model.Lives -= 1;
            HUD.UpdateLives(model.Lives);
            
            // Go back to spawn
            transform.position = RespawnPoint.transform.position;

            // Reset ghosts to home
            ghostController.ResetGhosts();
        } else
        {
            manager.LoseGame();
        }
    }

    // Convert movement direction to Vector2
    static Vector2 DirectionToVector(MoveDirection direction)
    {
        return direction switch
        {
            MoveDirection.Right => Vector2.right,
            MoveDirection.Down => Vector2.down,
            MoveDirection.Left => Vector2.left,
            MoveDirection.Up => Vector2.up,
            _ => Vector2.zero
        };
    }
}

public enum MoveDirection
{
    Right,
    Down,
    Left,
    Up
}