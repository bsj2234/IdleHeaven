using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    private float speed = 0.0f;
    private float direction = 0.0f;
    private NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    Vector3 prevDir = Vector3.zero;
    void Update()
    {
        float curSpeed = agent.velocity.magnitude;

        if (curSpeed > 0.1f)
        {
            Vector3 dirDiff = transform.forward - prevDir;
            dirDiff = transform.InverseTransformDirection(dirDiff);
            float DeltaDir = dirDiff.x;
            anim.SetFloat("MovingDirection", DeltaDir);

            anim.SetBool("isMoving", true);
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("IsRunning", false);
        }
        if (curSpeed > 15f)
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("IsRunning", true);
        }
        prevDir = transform.forward;
    }
}
