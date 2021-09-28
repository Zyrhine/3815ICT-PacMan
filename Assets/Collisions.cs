using UnityEngine;

public class Collisions : MonoBehaviour
{
    private PacManController pacManController;

    // Start is called before the first frame update
    void Start()
    {
        pacManController = GameObject.FindGameObjectWithTag("Controller").GetComponent<PacManController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            //if (ghost.IsVulernable)
            var anim = collision.gameObject.GetComponent<Animator>();
            if (anim.GetBool("IsVulnerable")) {
                pacManController.AddScore();
                anim.SetBool("Eaten", true);
            } else
            {
                pacManController.CapturePacMan();
            }
        }
    }
}
