using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

/// <summary>
/// The controller for ghosts in a scene.
/// </summary>
public class GhostController : MonoBehaviour
{
    private GhostModel model;
    private GameObject target;
    private Tilemap maze;
    public GameObject homeWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<GhostModel>();
        target = GameObject.FindGameObjectWithTag("Player");
        maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<Tilemap>();

        SpawnGhosts();
    }

    // Spawn each ghost at the home waypoint
    void SpawnGhosts()
    {
        foreach (Ghost ghost in model.Ghosts)
        {
            ghost.GameObject = Instantiate(Resources.Load(ghost.GhostType.ToString()), homeWaypoint.transform) as GameObject;
            ghost.Animator = ghost.GameObject.GetComponent<Animator>();
            ghost.Agent = ghost.GameObject.GetComponent<NavMeshAgent>();
            ghost.Agent.speed = ghost.SearchSpeed;
            ghost.Agent.updateUpAxis = false;
            ghost.Agent.updateRotation = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Ghost ghost in model.Ghosts)
        {
            ghost.Destination = ghost.Agent.destination;

            switch (ghost.State)
            {
                case GhostState.Search:
                    UpdateSearch(ghost);
                    break;
                case GhostState.Chase:
                    UpdateChase(ghost);
                    break;
                case GhostState.Return:
                    UpdateReturn(ghost);
                    break;
            }

            UpdateDirection(ghost);
        }
    }

    // Update the movement direction of a ghost
    void UpdateDirection(Ghost ghost)
    {
        if (ghost.Agent.velocity.x >= 1)
        {
            ghost.moveDirection = MoveDirection.Right;
        }
        if (ghost.Agent.velocity.x <= -1)
        {
            ghost.moveDirection = MoveDirection.Left;
        }
        if (ghost.Agent.velocity.y >= 1)
        {
            ghost.moveDirection = MoveDirection.Up;
        }
        if (ghost.Agent.velocity.y <= -1)
        {
            ghost.moveDirection = MoveDirection.Down;
        }

        ghost.Animator.SetInteger("Direction", (int)ghost.moveDirection);
    }

    // Update the search state of a ghost
    void UpdateSearch(Ghost ghost)
    {
        if (ghost.Agent.remainingDistance < 1)
        {
            while (true)
            {
                Vector3Int cellPosition = new Vector3Int(Random.Range(1, 26), Random.Range(-29, -1), 0);
                if (!maze.HasTile(cellPosition))
                {
                    var worldPosition = maze.CellToWorld(cellPosition);
                    ghost.Agent.SetDestination(worldPosition);
                    break;
                }
            }
        }

        if (ghost.IsSmart)
        {
            if (Vector3.Distance(ghost.GameObject.transform.position, target.transform.position) < ghost.ChaseRange)
            {
                ghost.State = GhostState.Chase;
                ghost.Agent.speed = ghost.ChaseSpeed;
            }
        }
    }

    // Update the chase state of a ghost
    void UpdateChase(Ghost ghost)
    {
        if (Vector3.Distance(ghost.GameObject.transform.position, target.transform.position) < ghost.ChaseRange)
        {
            ghost.Agent.SetDestination(target.transform.position);
        }
        else
        {
            ghost.State = GhostState.Search;
            ghost.Agent.speed = ghost.SearchSpeed;
        }
    }

    // Update the return state of a ghost
    void UpdateReturn(Ghost ghost)
    {
        ghost.Agent.SetDestination(homeWaypoint.transform.position);
    }

    void OnDrawGizmos()
    {
        if (model)
        {
            foreach (Ghost ghost in model.Ghosts)
            {
                Gizmos.DrawCube(ghost.Destination, new Vector3(0.25f, 0.25f, 0.25f));
            }
        }
    }
}
