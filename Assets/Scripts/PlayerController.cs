using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,walk,attack,interact,stagger
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody2d;
    private Vector3 change;

    public FloatValue currentHealth;
    public Signal playerHealthSignal;

    public PlayerState currentState;


    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rigidbody2d = GetComponent<Rigidbody2D>();

        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.interact && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.interact || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }   

    public void AttackWrapper()
    {
        if (currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.2f);
        currentState = PlayerState.walk;

    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        rigidbody2d.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    public void knock(float knockTime, float damege)
    {
        currentHealth.RunTimeValue -= damege;
        playerHealthSignal.Raise();
        if(currentHealth.RunTimeValue > 0)
        {
            StartCoroutine(knockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator knockCo(float knockTime)
    {
        if (rigidbody2d != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidbody2d.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rigidbody2d.velocity = Vector2.zero;
        }
    }
}
