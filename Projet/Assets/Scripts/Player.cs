using UnityEngine;
using System.Collections;

public class Player : MovingObject {

    public Animator animator;

    private Vector2 direction;

    public const int DETECTION = 5; //Définie la porté de vérouillage de la cible

    private GameObject enemyLock = null;

    public GameObject arrow;

    void Start()
    {	

    }

	void Update()
    {
        //Récupère l'entrée utilisateur 
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");


        //Lance l'animation adequat

        if( enemyLock != null ){
            animator.SetFloat("Horizontal",enemyLock.transform.position.x - this.transform.position.x);
            animator.SetFloat("Vertical",enemyLock.transform.position.y - this.transform.position.y);
            animator.SetFloat("speed",direction.sqrMagnitude);
        }else{
            animator.SetFloat("Horizontal",direction.x);
            animator.SetFloat("Vertical",direction.y);
            animator.SetFloat("speed",direction.sqrMagnitude);
        }

        if( Input.GetKeyDown(KeyCode.Z) && enemyLock != null ) {
            attack();
        }

        //Execute le mouvement
        base.move(direction.x,direction.y);

        NearEnemy();

	}


    private void NearEnemy()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;

        float tmp = 10000;
        float distance;

        foreach( GameObject enemie in enemies ){
 
            distance = Vector3.Distance(this.transform.position,enemie.transform.position);

            if( distance < tmp ){
                tmp = distance;
                if( distance < DETECTION ){
                    nearest = enemie;
                }
            }
        }

        if( nearest != null ){
            enemyLock = nearest;
        }else{
            enemyLock = null;
        }

    }


    private void attack()
    {   

        float decalage = 1f;
        float decalageX; 
        float decalageY;

        if( transform.position.x - enemyLock.transform.position.x < 0 ){
            decalageX = decalage * Vector3.Distance(this.transform.position,enemyLock.transform.position) / DETECTION;
        }else{
            decalageX = -decalage * Vector3.Distance(this.transform.position,enemyLock.transform.position) / DETECTION;
        }

        if( transform.position.y - enemyLock.transform.position.y < 0 ){
            decalageY = decalage * Vector3.Distance(this.transform.position,enemyLock.transform.position) / DETECTION;
        }else{
            decalageY = -decalage * Vector3.Distance(this.transform.position,enemyLock.transform.position) / DETECTION;
        }

        GameObject arrowSpawn = Instantiate(arrow,new Vector3(transform.position.x+decalageX,transform.position.y+decalageY,0f),Quaternion.identity);
        arrowSpawn.GetComponent<Arrow>().init(enemyLock.transform.position.x,enemyLock.transform.position.y);
    }
}