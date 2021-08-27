using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    public enum MoveDirection
    {
        Right,
        Down,
        Left,
        Up
    }

    public float MoveSpeed = 5f;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        Vector2 dir = Vector2.zero;

        switch(moveDirection)
        {
            case MoveDirection.Right:
                dir = Vector2.right;
                break;
            case MoveDirection.Down:
                dir = Vector2.down;
                break;
            case MoveDirection.Left:
                dir = Vector2.left;
                break;
            case MoveDirection.Up:
                dir = Vector2.up;
                break;
        }

        Vector2 pos = rb.position + (dir * MoveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
    }

    public void GetInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = MoveDirection.Right;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection = MoveDirection.Down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = MoveDirection.Left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection = MoveDirection.Up;
        }
    }
}
