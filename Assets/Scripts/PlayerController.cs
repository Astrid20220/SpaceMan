using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Variables del movimiento del personaje
    public float SaltoForce = 6f;
    public float runningSpeed = 2f;
    Rigidbody2D RigidBody;
    Animator animator;

    private const string STATE_ALIVE = "EstaVivo";
    private const string STATE_ON_THE_GROUND ="EstaEnElSuelo";


    public LayerMask groundMask

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


    }
    
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBoll(STATE_ALIVE, true);
        animator.SetBoll(STATE_ON_THE_GROUND, true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")){
            Jump();
        
        animator.SetBoll(STATE_ON_THE_GROUND, IsTouchingTheGround());
        }
        Debug.DrawRay(this.transform.position, Vector2.down*1.5f, Color.red);
    }


        void FixedUpdate(){
            if(GameManager.sharedInstance.currentGameState == GameState.inGame)
            {
                if(rigidBody.velocity.x < runningSpeed)
                {
                   rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            }
        }else{// Si no estamos dentro de la partida
            rigidBody.velocity = Vector2(0, rigidBody.velocity.y);
        }


    void Salto(){
        if(IsTouchingTheGround()){

        }
        rigidBody.AddForce(Vector2.up*SaltoForce, ForceMode2D.Impulse);

    }
    
    //Nos indica si el personaje esta o no tovando el suelo
    bool IsTouchingTheGround(){
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 1.5f, groundMask)){

            return true;
        }else{
            return false;
        }

    }
}
