using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MovingObject
{   

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        base.move(direction.x,direction.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        
        if( other.tag == "Enemy"){
            Destroy(gameObject);
        }
        //Ici qu'il faudra gérer les dégats
        
    }

    public void init(float xDir,float yDir)
    {
        direction.x = xDir - transform.position.x;
        direction.y = yDir - transform.position.y;
    }

}
