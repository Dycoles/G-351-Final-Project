// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public class BattleSystem : MonoBehaviour
// {

//     public BattleState state;
//     public Character player1; //player
//     public Character player2; // enemy

//     public void Start(){
//         state = BattleState.START;
//         StartCoroutine(setUp(player1, player2));
//     }
    
//     public IEnumerator setUp(Character p1, Character p2){
//         Debug.Log("Setting up Battle");
//         player1 = p1;
//         player2 = p2;

//         yield return LoadResourcesAsync();

//         PlayerTurn();
        
//     }

//     IEnumerator LoadResourcesAsync(){
//         yield return Resources.LoadAsync("PlayerData");
//         yield return Resources.LoadAsync("EnemyData");
//     }

//     public void PlayerTurn(){
//         Debug.Log("players turn");
//         bool awaitingInput = true;
//         while(awaitingInput){
//             if(Input.GetKeyDown(KeyCode.Alpha1)){
//                 Debug.Log("players 1");
//                 awaitingInput = false;
//                 player2.damagePlayer(3);
//             }
//             if(Input.GetKeyDown(KeyCode.Alpha2)){
//                 Debug.Log("players 2");
//                 awaitingInput = false;
//                 player2.damagePlayer(5);

//             }
//             if(Input.GetKeyDown(KeyCode.Alpha3)){
//                 Debug.Log("players 3");
//                 awaitingInput = false;
//                 player2.damagePlayer(7);

//             }
//             if(Input.GetKeyDown(KeyCode.Alpha4)){
//                 Debug.Log("players 4");
//                 awaitingInput = false;
//                 endBattle();
//             }
//         }
//         validate();
//         EnemyTurn();
//     }

//     public void EnemyTurn(){
//         int randomNumber = Random.Range(0,4);
//         player1.damagePlayer(randomNumber);
//         validate();
//         PlayerTurn();
//     }

//     public void validate(){
//         if(player1.health() <= 0){
//             state = BattleState.LOST;
//             endBattle();
//         }
//         if(player2.health() <= 0){
//             state = BattleState.WON;
//             endBattle();
//         }
//     }
    
//     public void endBattle(){
//         MovementDragon p1a = player1.GetComponent<MovementDragon>();
//         p1a.isInCombat(false);
//         EnemyAI p2a = player2.GetComponent<EnemyAI>();
//         p2a.isInCombat(false);
//     }

// }
