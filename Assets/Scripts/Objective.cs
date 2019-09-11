using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{    
    public int score;
    public bool respawning = false;
    public bool isBeingPickedUp = false;
    public float flyHeight = 3;
    public int respawnTime = 30;

    private float moveSpeed;
    private Renderer rend;
    private Color activeObjectiveColor = new Color(0.0046f, 0.4183f, 0.9905f);
    private Color usedObjectColor = new Color(0.9921f, 0.1218f, 0.00392f);

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool reverse;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startPosition = transform.position;
        endPosition =new Vector3(transform.position.x, transform.position.y + flyHeight, transform.position.z);
        reverse = false;
        moveSpeed = Random.Range(0.5f, 2f);
    }

    void Update()
    {
        handleMovement();
    }

    private void handleMovement()
    {
        float step = moveSpeed * Time.deltaTime;
        if (!reverse)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
        }
        if (transform.position == endPosition) reverse = true;
        else if (transform.position == startPosition) reverse = false;
    }


    public void startRespawning()
    {
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {       
        rend.material.SetColor("_Color", usedObjectColor);
        yield return new WaitForSeconds(respawnTime);
        respawning = false;
        rend.material.SetColor("_Color", activeObjectiveColor);
    }
}
