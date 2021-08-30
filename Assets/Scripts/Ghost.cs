using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private bool stop;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
