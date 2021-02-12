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

    protected override void Init()
    {
        actorRigidbody = GetComponent<Rigidbody>();
    }

    protected void CharacterMove(float horizontal, float vertical, float deltaTime)
    {
        actorRigidbody.velocity = new Vector3(horizontal * speed * deltaTime, actorRigidbody.velocity.y, vertical * speed * deltaTime);
    }

    protected void Jump(float power)
    {
        actorRigidbody.AddForce(new Vector3(0.0f, jumpPower * power, 0.0f));
    }
}
