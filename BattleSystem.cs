using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.XR.WSA.Input;


//This is a nightmare and a half to code...the Youtudetutorial I have is sucky.

public enum BattleState 
{
    Start, //Start of the battle
    PlayerTurn, //Player action
    EnemyTurn, //enemy turn
    FinishedTurn, //self-explanatory
    Won,        //1+2=420 360 no-scope
    Lost    //Boo-hoo
}

public class Turnhandle : MonoBehaviour
{
    public BattleState state;

    public EnemyProfile[] EnemiesInBattle;
    private bool enemyActed;
    private GameObject[] EnemyAtks;

    public GameObject PlayerUi;
    public Iconmovement Playericon;



    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        enemyActed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == BattleState.Start)
        {
            PlayerUi.SetActive(true);
            state = BattleState.PlayerTurn;

            //needs an animated sprite ig
        }
        else if (state == BattleState.PlayerTurn)
        {
            //wait for player to finish up quacking
        }
        else if (state == BattleState.EnemyTurn)
        {
            if (EnemiesInBattle.Length <= 0)
            {
                EnemyFinishedTurn();
            }
            else
            {
                if (!enemyActed)
                {
                    Playericon.gameObject.SetActive(true);
                    Playericon.SetHeart();

                    foreach (EnemyProfile emy in EnemiesInBattle)
                    {
                        int AtkNumb = Random.Range(0, emy.EnemiesAttacks.Length);

                        Instantiate(emy.EnemiesAttacks[AtkNumb], Vector3.zero, Quaternion.identity);
                    }
                    EnemyAtks = GameObject.FindGameObjectsWithTag("Enemy");

                    enemyActed = true;
                }
                else
                {
                    bool enemyfin = true;
                    foreach (GameObject emy in EnemyAtks)
                    {
                        if (!emy.GetComponent<EnemyTurnHandle>().FinishedTurn)
                        {
                            enemyfin = false;
                        }
                    }
                    if (enemyfin)
                    {
                        EnemyFinishedTurn();
                    }

                    else if (state == BattleState.FinishedTurn)
                    {
                        Playericon.gameObject.SetActive(false);

                        if (Playericon.GetComponent<Player>().HP < 0)
                        {
                            state = BattleState.Start;
                        }
                    }
                    else if (state == BattleState.Won)
                    {

                    }
                    public void PlayerAct()
                    {
                        playerfinishedTurn();
                    }
                    void playerfinishedTurn();
                    {
                        PlayerUi.SetActive(false);

                        state = BattleState.EnemyTurn;

                    }
                     void EnemyFinishedTurn()
                    {
                        foreach (GameObject obj in EnemyAtks)
                        {
                            Destroy(obj);
                        }
                    }
                    enemyActed = false;
                    state = BattleState.FinishedTurn;
                }

            }
        }
    }
}

    
