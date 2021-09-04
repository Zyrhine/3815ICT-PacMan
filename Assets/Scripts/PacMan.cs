using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private MoveDirection queueDirection;
    private MoveDirection _moveDirection;
    private MoveDirection moveDirection
    {
        get => _moveDirection;
        set
        {
            _moveDirection = value;
            anim.SetInteger("Direction", (int)value);
        }
    }
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get directional input
        if (Application.platform == RuntimePlatform.Android)
        {
            GetTouchInput();
        } else
        {
            GetKeyboardInput();
        }

        // If the direction has been changed, change direction if PacMan is able to
        if (moveDirection != queueDirection)
        {
            if (!Physics2D.Raycast(rb.position, DirectionToVector(queueDirection), 0.16f))
            {
                moveDirection = queueDirection;
            }
        }

        // Move in the direction
        Vector2 dir = DirectionToVector(moveDirection);
        Vector2 pos = rb.position + (dir * MoveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
    }

    // Gets the movement direction from input
    public void GetKeyboardInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            queueDirection = MoveDirection.Right;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            queueDirection = MoveDirection.Down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            queueDirection = MoveDirection.Left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            queueDirection = MoveDirection.Up;
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
                    startPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 endPos = touch.position;
                    Vector2 direction = endPos - startPos;
                    direction.Normalize();

                    if (direction.x > 0.5f)
                    {
                        queueDirection = MoveDirection.Right;
                    } else if (direction.x < -0.5f)
                    {
                        queueDirection = MoveDirection.Left;
                    } else if (direction.y > 0.5f)
                    {
                        queueDirection = MoveDirection.Up;
                    } else if (direction.y < -0.5f)
                    {
                        queueDirection = MoveDirection.Down;
                    }

                    break;
            }
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
