using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public emun CollectableType{
    healthPotion,
    manaPotion,
    money
}
public class Collectable : MonoBehaviour{
    public CollectableType type = CollectableType.money;

    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;

    bool hasBeenCollected = false;

    public int value = 1;

    GameObject player;

    private void Awake(){
        sprite = GetComponent <CircleCollider2D>();
        itemCollider = GetComponent<CircleCollider2D>();
    }
}
     
{
    private void Start(){
        player = GameObject.Find("Player");
    }
    void Show(){
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false;

    }

    void Hide(){
        sprite.enabled = false;
        itemCollider.enabled = false;

    }

    void Collect(){
        Hide();
        hasBeenCollected = true;

        switch(this.type){
            case CollectableType.money:
            GameManager.sharedInstance.CollectObject(this);
            break;

            case CollectableType.healthPotion:
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerController>().CollectHealth(this.value);
            break;

            case CollectableType.manaPotion:
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerController>().CollectMana(this.value);

            break;

        }

    }

    void OnTriggerEnter2D(Collider2D collision){

    }
    if(collision.tag == "Player"){
        Collect();

    }
    
}
