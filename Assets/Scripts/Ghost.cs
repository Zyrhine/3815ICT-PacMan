using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    private enum State
    {
        Search,
        Chase,
        Return
    }

    private State curState = State.Search;
    public float SearchSpeed = 1f;
    public float ChaseSpeed = 2f;
    private float chaseRange = 3f;
    private MoveDirection _moveDirection = MoveDirection.Right;
    private MoveDirection moveDirection
    {
        get => _moveDirection;
        set
        {
            _moveDirection = value;
            anim.SetInteger("Direction", (int)value);
        }
    }
    private bool _isVulnerable = false;
    private bool isVulnerable
    {
        get => _isVulnerable;
        set
        {
            _isVulnerable = value;
            anim.SetBool("isVulnerable", value);
        }
    }
    private Animator anim;

    private GameObject target;
    private NavMeshAgent agent;
    private bool stop;
    public bool isSmart;

    public GameObject homeWaypoint;
    private Tilemap maze;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<Tilemap>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = SearchSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        destination = agent.destination;
        switch (curState)
        {
            case State.Search:
                UpdateSearch();
                break;
            case State.Chase:
                UpdateChase();
                break;
            case State.Return:
                UpdateReturn();
                break;
        }

        UpdateDirection();
    }

    void UpdateDirection()
    {
        if (agent.velocity.x >= 1)
        {
            moveDirection = MoveDirection.Right;
        }
        if (agent.velocity.x <= -1)
        {
            moveDirection = MoveDirection.Left;
        }
        if (agent.velocity.y >= 1)
        {
            moveDirection = MoveDirection.Up;
        }
        if (agent.velocity.y <= -1)
        {
            moveDirection = MoveDirection.Down;
        }
    }

    void UpdateSearch()
    {
        if (agent.remainingDistance < 1)
        {
            while (true)
            {
                Vector3Int cellPosition = new Vector3Int(Random.Range(1, 26), Random.Range(-29, -1), 0);
                if (!maze.HasTile(cellPosition))
                {
                    var worldPosition = maze.CellToWorld(cellPosition);
                    agent.SetDestination(worldPosition);
                    break;
                }
            }
        }

        if (isSmart)
        {
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) < chaseRange)
            {
                curState = State.Chase;
                agent.speed = ChaseSpeed;
            }
        }
    }

    void UpdateChase()
    {
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) < chaseRange)
        {
            agent.SetDestination(target.transform.position);
        } else
        {
            curState = State.Search;
            agent.speed = SearchSpeed;
        }
    }

    void UpdateReturn()
    {
        agent.SetDestination(homeWaypoint.transform.position);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(destination, new Vector3(0.25f, 0.25f, 0.25f));
    }
}
