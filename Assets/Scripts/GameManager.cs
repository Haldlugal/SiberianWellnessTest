using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool paused;
    public GameObject player;
    
    public float collectionRadius;
    public float gettingObjectiveTime;
    public int gameTime;
    public int victoryCondition;
    public HUD ui; 
    public GameObject castingBar;

    private IEnumerator pickUpCoroutine;
    private float gettingProcessTime;
    private float countdown;    

    private int score;
    
    void Start()
    {
        paused = false;
        score = 0;
        ui.setScore(score);
        gettingProcessTime = 0;
        castingBar.SetActive(false);
        countdown = gameTime;
    }

    void Update()
    {
        if (!paused)
        {
            handleCountdown();
            handleInput();
            handleGameOver();
        }
        
    }

    private void handleGameOver()
    {
        if (countdown <= 0)
        {
            paused = true;
            bool isWon = false;
            ui.setGameOver(isWon);
        }
        else if (score >= victoryCondition)
        {
            paused = true;
            bool isWon = true;
            ui.setGameOver(isWon);
        }
    }

    private void handleInput()
    {             
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {            
            Collider[] foundColliders = Physics.OverlapSphere(player.transform.position, collectionRadius);
            int i = 0;
            while (i < foundColliders.Length)
            {
                if (foundColliders[i].tag == "Objective")
                {                    
                    Objective objective = foundColliders[i].GetComponent<Objective>();
                    if (!objective.respawning && !objective.isBeingPickedUp)
                    {
                        StartCoroutine(PickUpObjective(objective));
                    }
                    break;
                }
                i++;
            }                      
        }        
    }

    private void interruptGetting(Objective objective)
    {
        objective.isBeingPickedUp = false;
        castingBar.SetActive(false);
        gettingProcessTime = 0;
    }

    IEnumerator PickUpObjective(Objective objective)
    {
        castingBar.SetActive(true);
        objective.isBeingPickedUp = true;
        while (true)
        {
            gettingProcessTime += Time.deltaTime;
            ui.fillCastingBar(gettingProcessTime / gettingObjectiveTime);
            float distance = Vector3.Distance(objective.transform.position, player.transform.position);
            
            if (distance > collectionRadius)
            {
                interruptGetting(objective);
                yield break;
            }
            if (gettingProcessTime >= gettingObjectiveTime)
            {
                getObjective(objective);
                interruptGetting(objective);
                yield break;
            }
            yield return null;
        }
    }
    private void getObjective(Objective objective)
    {
        objective.respawning = true;
        score += objective.score;
        ui.setScore(score);
        objective.startRespawning();          
    }    
    
    private void handleCountdown()
    {
        countdown -= Time.deltaTime;
        ui.setTimer(countdown);        
    }

    public void reload()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void quit()
    {
        Application.Quit();
    }
}
