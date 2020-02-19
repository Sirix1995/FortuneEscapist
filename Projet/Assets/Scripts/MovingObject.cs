using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    
    public Rigidbody2D rigidbody;

    public BoxCollider2D boxCollider;

    public float baseSpeed = 5F;

    private float speed;

    Vector2 movement;

    void Start()
    {	
        speed = baseSpeed;
    }

	void Update(){

	}

    public void move(float xdir, float ydir){
        movement.x = xdir;
		movement.y = ydir;

        if( movement.x != 0 && movement.y != 0 ){
            speed = (float) 0.75  * baseSpeed;
        }else{
            speed = baseSpeed;
        }
    }

    void FixedUpdate()
    {	
        rigidbody.MovePosition(rigidbody.position + movement * speed * Time.fixedDeltaTime);
    }
}
