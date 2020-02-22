using UnityEngine;
using System.Collections;

public class Player : MovingObject {

    public Animator animator;

    private Vector2 direction;

    public const int DETECTION = 5; //Définie la porté de vérouillage de la cible

    private GameObject enemyLock = null;

    public GameObject[] projectiles = new GameObject[2];
    private int currentProjectile = 0;

    private float time = 10f;

    public int life;

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

        if( Input.GetKeyDown(KeyCode.Space) && enemyLock != null ) {
            attack();
        }

        if( Input.GetKeyDown(KeyCode.RightAlt) ) { // Changement d'arme
            currentProjectile = (currentProjectile + 1) % 2;
        }

        //Execute le mouvement
        base.move(direction.x,direction.y);

        NearEnemy();
        time += Time.deltaTime;

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

        if( time > projectiles[currentProjectile].GetComponent<Projectile>().timeInterval ){  
            GameObject projectilSpawn = Instantiate(projectiles[currentProjectile],new Vector3(transform.position.x,transform.position.y,2f),Quaternion.identity);
            projectilSpawn.GetComponent<Projectile>().init(enemyLock.transform.position.x,enemyLock.transform.position.y);
            time = 0f;
        }
    }

    public void changeWeapon(GameObject object){
        float changed = false;
        for( int i = 0; i < projectiles.lenght; i++ ){
            if( projectiles[i] == null ){
                projectiles[i] = object;
                changed = true;
            }
        }

        if ( !changed ){
            projectiles[currentProjectile] = object;
        }
    }
}