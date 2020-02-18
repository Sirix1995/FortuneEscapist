using UnityEngine;
using System.Collections;

public class Player : MovingObject {

    public Animator animator;

    private Vector2 direction;

    void Start()
    {	

    }

	void Update()
    {
        //Récupère l'entrée utilisateur 
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");


        //Lance l'animation adequat
        animator.SetFloat("Horizontal",direction.x);
        animator.SetFloat("Vertical",direction.y);
        animator.SetFloat("speed",direction.sqrMagnitude);


        //Execute le mouvement
        base.move(direction.x,direction.y);
	}
}