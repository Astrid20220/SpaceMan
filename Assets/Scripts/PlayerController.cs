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
    Vector3 startPosition;

    const string STATE_ALIVE = "EstaVivo";
    const string STATE_ON_THE_GROUND ="EstaEnElSuelo";

    private int healthPotions, manaPotions;

    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15,
    MAX_HEALTH = 200, MAX_MANA = 30,
    MIN_HEALTH = 10, MIN_MANA =0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE =1.5f;

    public LayerMask groundMask

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


    }
    
    // Start is called before the first frame update
    void Start()
    {
       
        startPosition = this.transform.position;
        
    }

    public void StartGame(){
        animator.SetBoll(STATE_ALIVE, true);
        animator.SetBoll(STATE_ON_THE_GROUND, true);

        healthPotions = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

        Invoke("RestartPosition", 0.2f);

    }


    void RestartPosition(){
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent <CameraFollow>().ResetCameraPosition();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")){
            Jump(false);
          }
          if(Input.GetButtonDown("Superjump")){
            Jump(true);
          }
        animator.SetBoll(STATE_ON_THE_GROUND, IsTouchingTheGround());
      
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


    void Jump(bool Superjump){

        float JumpForceFactor = jumpForce;
        if(Superjump&&manaPoints -= SUPERJUMP_COST){
            manaPoints-= SUPERJUMP_COST; 
            JumpForceFactor *= SUPERJUMP_FORCE;    
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
            
        }

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

    public void Die(){
        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxcore", 0f);
        if(travelledDistance > previousMaxDistance){
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
        this.animator.SetBoll(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();

    }

    public void CollectHealth(int points){
        this.healtPoints += points;
        if(this.healthPotions >= MAX_HEALTH){
            this.healthPotions = MAX_HEALTH;
        }
    }

    public void CollectMana(int points){

    }

    public int GetHealth(){
        return healtPoints;
    }

    public int GetMana(){
        return manaPoints;
    }

    public float GetTravelledDistance(){
        return this.transform.position.x - startPosition.x;
    }
}
