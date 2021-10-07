using UnityEngine;

/// <summary>
/// The model for PacMan.
/// </summary>
public class PacManModel : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public MoveDirection queueDirection;
    public MoveDirection moveDirection;
    public Vector2 touchStartPos;
    public bool IsEnabled = false;
    public int Score = 0;
    public int Lives = 3;
}
