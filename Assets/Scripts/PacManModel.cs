using UnityEngine;

/// <summary>
/// The model for PacMan.
/// </summary>
public class PacManModel : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public MoveDirection queueDirection;
    public MoveDirection moveDirection;
    public Vector2 touchStartPos;
}
