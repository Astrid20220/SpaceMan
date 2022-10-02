using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Variables del movimiento del personaje
    public float SaltoForce = 6f;
    Rigidbody2D RigidBody;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();


    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Salto();

        }
    }

    void Salto(){
        rigidBody.AddForce(Vector2.up*SaltoForce, ForceMode2D.Impulse);

    }
}
