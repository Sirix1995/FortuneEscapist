using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MovingObject
{   

    private Vector2 direction;

    private Vector2 unit = new Vector2(0f,1f);


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
        float sens = 1f;

        if( transform.position.x > xDir ){
            sens = -1f;
        }

        transform.Rotate(0,0,sens*Vector2.Angle(unit,new Vector2((transform.position.x-xDir),(transform.position.y-yDir)))+180f);
        direction.x = xDir - transform.position.x;
        direction.y = yDir - transform.position.y;
    }

}
