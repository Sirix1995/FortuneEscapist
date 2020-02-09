using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 5F;

	public Rigidbody2D rigidbody;

	Vector2 movement;

    void Start()
    {	

    }

	void Update(){
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

	}

    void FixedUpdate()
    {	
        rigidbody.MovePosition(rigidbody.position + movement * speed * Time.fixedDeltaTime);
    }
}