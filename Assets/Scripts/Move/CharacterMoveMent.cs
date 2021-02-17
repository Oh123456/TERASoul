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
        Vector3 forward = transform.forward;
        Vector3 velocity_X = transform.forward * speed * vertical * deltaTime;
        Vector3 velocity_Z = Vector3.Cross(Vector3.up, forward) * speed * horizontal * deltaTime;
        Vector3 velocity = velocity_X + velocity_Z;
        velocity.y = actorRigidbody.velocity.y;
        actorRigidbody.velocity = velocity;


        float animatorSpeed = actorRigidbody.velocity.normalized.magnitude;

        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", horizontal);
    }

    public void CharacterMove(float horizontal, float vertical, float up, float deltaTime)
    {
        CharacterMove(horizontal, vertical,deltaTime);
        Vector3 v3 = actorRigidbody.velocity;
        v3.y = up * deltaTime;
        actorRigidbody.velocity = v3;
    }

    public void Jump(float power)
    {
       
       actorRigidbody.AddForce(new Vector3(0.0f, jumpPower * power, 0.0f));
       animator.SetBool("Jump",true);
    }

    public void ChangeSpeed(float value)
    {
        speed = value;
    }

    public void ChangeSpeed(float value, ref float orine)
    {
        orine = speed;
        speed = value;
    }
}
