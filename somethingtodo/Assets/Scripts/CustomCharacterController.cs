using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CustomCharacterController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public float Speed = 10.0f;

	void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;
        dir.y += Input.GetAxis("Vertical");
        dir.x += Input.GetAxis("Horizontal");
        dir *= Speed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(_rigidbody.position + dir);
	}
}
