using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveMent : BaseComponent
{
    protected Rigidbody actorRigidbody;
    [SerializeField]
    float speed = 100.0f;
    [SerializeField]
    float jumpPower = 100.0f;

    protected Animator animator;

    private void Start()
    {
        actorRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    protected override void Init()
    {
        actorRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void CharacterMove(float horizontal, float vertical, float deltaTime)
    {
        animator.SetBool("Jump", false);
        actorRigidbody.velocity = new Vector3(horizontal * speed * deltaTime, actorRigidbody.velocity.y, vertical * speed * deltaTime);
        float animatorSpeed = actorRigidbody.velocity.normalized.magnitude;
        //Debug.Log(animatorSpeed);

        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", horizontal);
    }

    public void Jump(float power)
    {
       
       actorRigidbody.AddForce(new Vector3(0.0f, jumpPower * power, 0.0f));
       animator.SetBool("Jump",true);
    }
}
