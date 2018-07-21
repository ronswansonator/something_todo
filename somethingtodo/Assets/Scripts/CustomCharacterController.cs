using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class CustomCharacterController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _dir;

    public float Speed = 10.0f;
    public FarmTile farmTile; 

	void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _dir = Vector2.left;
	}
	
	void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;
        dir.y += Input.GetAxis("Vertical");
        dir.x += Input.GetAxis("Horizontal");
        dir *= Speed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(_rigidbody.position + dir);
        if (dir.magnitude > .05f)
        {
            _dir = dir;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            Vector2 worldPos = _rigidbody.position + _dir.normalized;
            FarmManager.GetInstance().Plant(farmTile, worldPos);
        }
	}
}
