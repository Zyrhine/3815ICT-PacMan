using UnityEngine;
using UnityEngine.AI;

///<summary>
/// The model containing the data for a single ghost.
///</summary>
[System.Serializable]
public class Ghost
{
    public GhostType GhostType;
    public bool IsSmart;
    public bool IsVulnerable = false;
    public float SearchSpeed = 1f;
    public float ChaseSpeed = 2f;
    public float ChaseRange = 3f;

    [HideInInspector] public GhostState State = GhostState.Search;
    [HideInInspector] public MoveDirection moveDirection = MoveDirection.Right;
    [HideInInspector] public bool Stop;
    [HideInInspector] public Vector3 Destination;
    [HideInInspector] public Animator Animator;
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public GameObject GameObject;
}

public enum GhostState
{
    Search,
    Chase,
    Return
}

public enum GhostType
{
    Blinky,
    Inky,
    Pinky,
    Clyde
}
