using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Settings")]
    public float turnSpeed;
    private Rigidbody rb;
    private Animator animator;
    private Vector3 movementDirection;
    private Quaternion rotation = Quaternion.identity;
    FiniteStateMachine.State currentState = FiniteStateMachine.State.Idle;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (animator == null || rb == null)
        {
            Debug.LogError("Animator or Rigidbody is not assigned in PlayerBehaviour.");
        }
        animator.applyRootMotion = false;
    }
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool isMoving = !Mathf.Approximately(horizontalInput, 0) || !Mathf.Approximately(verticalInput, 0);
        animator.SetBool("IsWalking", isMoving);

        movementDirection.Set(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movementDirection, turnSpeed * Time.deltaTime, 0);
        rotation = Quaternion.LookRotation(desiredForward);

        if (Input.GetMouseButtonDown(0))
        {
            currentState = FiniteStateMachine.State.Attacking;
        }

        else if (isMoving)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                currentState = FiniteStateMachine.State.Running;
            }
            else
            {
                currentState = FiniteStateMachine.State.Walking;
            }
        }
        else
        {
            currentState = FiniteStateMachine.State.Idle;
        }

        switch (currentState)
        {
            case FiniteStateMachine.State.Idle:
                idle();
                break;
            case FiniteStateMachine.State.Walking:
                walk();
                break;
            case FiniteStateMachine.State.Running:
                run();
                break;
            case FiniteStateMachine.State.Attacking:
                attack(); 
                break;

        }
    }
    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movementDirection * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
    private void idle()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRuning", false);
    }
    private void walk()
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsRuning", false);
    }
    private void run()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRuning", true);
    }
    private void attack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("Attack triggered");
    }  
}
