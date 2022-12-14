using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    menu,
    inGame,
    gameOver
    
}

public class GameManager : MonoBehaviour{

    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstance;

    private PlayerController controller;

    public int collectedObject = 0;

    void Awake(){

        if(sharedInstance == null){

             sharedInstance = this;

        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
       controller = GameObject.Find("Player").GetComponent<PlayerController>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") 
        && currentGameState != GameState.inGame){
            StartGame();
        }
    }

    public void StartGame(){
        SetGameState(GameState.inGame);
    
    }
    public void GameOver(){
        SetGameState(GameState.gameOver);

    }
    public void BackToMenu(){
        SetGameState(GameState.menu);

    }

    private void SetGameState(GameState newGameState){
        if(newGameState == GameState.menu){
            MenuManager.sharedInstance.ShowMainMenu();

        }else if(newGameState == GameState.inGame){
            //TODO hay que preparar la escena para jugar 
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            LevelManager.sharedInstance.GenerateInitialBlocks();
            controller.StartGame();
            MenuManager.sharedInstance.HideMainMenu();

        }else if(newGameState == GameState.gameOver){
            //TODO: preparar el juego para el Game Over
            MenuManager.sharedInstance.HideMainMenu();
        }

        this.currentGameState = newGameState;
    }

    void ReloadLevel(){
        
    }

    public void CollectObject(Collectable collectable){
        collectedObject += collectable.value;
    }

}
