using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST, OVERWORLD}

public class Character : MonoBehaviour
{

    private List<Action> availableActions;
    private int currentHealth;
    public GameObject HUDElement;
    public BattleState state = BattleState.OVERWORLD;
    public Character opp;
    public GameObject me;
    public HealthBar healthBar;
    
    // added by Jennifer
    AudioManager audioManager;
    
    private bool inCombat = false;
    
    // added by Jennifer
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 10;
        HUDElement.SetActive(false);
        
        healthBar.SetMaxHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            if(state == BattleState.PLAYERTURN){
                //change JGG
                audioManager.PlaySFX(audioManager.corgiScratch);

                Debug.Log("SCRATCH");
                Debug.Log(opp.health());
                opp.damagePlayer(3);
                Debug.Log(opp.health());
                if(opp.health() <= 0){
                    Destroy(opp.me);
                    endBattle();
                }
                state = BattleState.ENEMYTURN;
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            if(state == BattleState.PLAYERTURN){
                //change JGG
                audioManager.PlaySFX(audioManager.corgiBite);
                Debug.Log("BITE");
                Debug.Log(opp.health());
                opp.damagePlayer(5);
                Debug.Log(opp.health());
                if(opp.health() <= 0){
                    Destroy(opp.me);
                    endBattle();
                }
                state = BattleState.ENEMYTURN;
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            if(state == BattleState.PLAYERTURN){
                //change JGG
                audioManager.PlaySFX(audioManager.corgiHeal);
                Debug.Log("HEAL");
                Debug.Log(opp.health());
                this.healPlayer(2);
                Debug.Log(opp.health());
                if(opp.health() <= 0){
                    Destroy(opp.me);
                    endBattle();
                }
                state = BattleState.ENEMYTURN;
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            if(state == BattleState.PLAYERTURN){
                //change JGG
                //audioManager.PlaySFX(audioManager.corgiRun);
                Debug.Log("RUN");
                Debug.Log(opp.health());
                opp.GetComponent<EnemyAI>().isInCombat(false);
                Invoke("ranAway", 2f);
                endBattle();
            }
        }
        if(state == BattleState.ENEMYTURN){
            Debug.Log("Enemys turn");
            Debug.Log(this.health());
            int randomNumber = Random.Range(0,4);
            this.damagePlayer(randomNumber);
            Debug.Log(this.health());
            if(this.health() <= 0){
                Destroy(this.me);
                    endBattle();
            }
            state = BattleState.PLAYERTURN;
        }
    }


    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Enemy"){
            if(inCombat == false){
                state = BattleState.START;
                EnemyAI enemy = other.GetComponent<EnemyAI>();
                MovementDragon myMovement = this.GetComponent<MovementDragon>();
                opp = other.GetComponent<Character>();
                inCombat = true;
                myMovement.isInCombat(true);
                enemy.isInCombat(true);
                Debug.Log("Hit an enemy!!! FIGHT!");
                this.GetComponent<MovementDragon>().moveBack();
                startBattle(opp);
            }
        }
    }

    public void damagePlayer(int damage){
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    public void healPlayer(int damage){
        currentHealth += damage;

        healthBar.SetHealth(currentHealth);
    }

    public int health(){
        return currentHealth;
    }

    public void startBattle(Character opponent){
        Debug.Log("Starting battle");

        state = BattleState.PLAYERTURN;

        HUDElement.SetActive(true);
        return;
    }

    public void endBattle(){
        Debug.Log("Returning to Overworld");
        MovementDragon myMovement = this.GetComponent<MovementDragon>();
        myMovement.isInCombat(false);
        state = BattleState.OVERWORLD;
        HUDElement.SetActive(false);
    }
    public void ranAway(){
        inCombat = false;
    }

    // public void myTurn(){
    //     if(this.tag == "Enemy"){
    //     int randomNumber = Random.Range(0,4);
    //     player1.damagePlayer(randomNumber);
    //     }else{
    //         while()
    //     }
    // }
    /*
while(state != BattleState.OVERWORLD){
            if(state == BattleState.PLAYERTURN){
                Debug.Log("Players Turn");
                if(Input.GetKeyDown(KeyCode.Alpha1)){
                    Debug.Log("players 1");
                    opponent.damagePlayer(5);
                    if(opponent.health() <= 0){
                        state = BattleState.OVERWORLD;
                        continue;
                    }
                    state = BattleState.ENEMYTURN;
                }
                if(Input.GetKeyDown(KeyCode.Alpha2)){
                    Debug.Log("players 2");
                    opponent.damagePlayer(5);
                    if(opponent.health() <= 0){
                        state = BattleState.OVERWORLD;
                        continue;
                    }
                    state = BattleState.ENEMYTURN;
                }
                if(Input.GetKeyDown(KeyCode.Alpha3)){
                    Debug.Log("players 3");
                    opponent.damagePlayer(7);
                    if(opponent.health() <= 0){
                        state = BattleState.OVERWORLD;
                        continue;
                    }
                    state = BattleState.ENEMYTURN;
                }
                if(Input.GetKeyDown(KeyCode.Alpha4)){
                    Debug.Log("players 4");
                    state = BattleState.OVERWORLD;
                }
            }
            if(state == BattleState.ENEMYTURN){
                int randomNumber = Random.Range(0,4);
                this.damagePlayer(randomNumber);
                if(this.health() <= 0){
                    state = BattleState.OVERWORLD;
                }
            }

        }

    */

}
