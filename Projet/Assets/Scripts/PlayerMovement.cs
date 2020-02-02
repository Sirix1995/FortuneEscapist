using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    private Vector2 direction;

    void Start(){
    }
    // Update is called once per frame
    void Update()
    {
        TakeInput();
        Move();
    }

    private void Move(){
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void TakeInput(){
        direction = Vector2.zero;

        if(Input.GetKey(KeyCode.Z)){
            direction += Vector2.up;
        }
        if(Input.GetKey(KeyCode.S)){
            direction += Vector2.down;
        }
        if(Input.GetKey(KeyCode.Q)){
            direction += Vector2.left;
        }
        if(Input.GetKey(KeyCode.D)){
            direction += Vector2.right;
        }
    }


}
