using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStates : MonoBehaviour
{
    public bool active = true;
    public int level = 1;
    State currentState = State.waiting;
    public float minWait = 2f;
    public float maxWait = 5f;
    float timer;
    //Movement stages before attack
    public int stages = 3;
    int currentStage = 0;

    void Update()
    {
        //Test
        if (Input.GetButtonDown("Fire2"))
            Detected();

       switch (currentState)
        {
            case State.waiting:
                S_Waiting();
                break;
            case State.moving:
                S_Moving();
                break;
            case State.detected:
                S_Detected();
                break;
            case State.movingBack:
                S_MovingBack();
                break;
            case State.prepareAttack:
                S_PrepareAttack();
                break;
            case State.attack:
                S_Attack();
                break;
        }

    }
    //Public function that is called when player detects monster and interactes to push them back
    public void Detected()
    {
        //Could do a check to see if monster was preparing an attack
        //Could do bonus slow down of enemy for player
        currentState = State.detected;
    }

    //Waiting to move forward
    void S_Waiting()
    {
        float currentTime = Time.time;
        if (timer < currentTime)
        {
            currentState = State.moving;
        }
    }

    //Enemy moves closer
    //Can have a chance to move, or chance to stay, based off of level
    //Then waits again
    void S_Moving()
    {
        currentStage++;
        Debug.Log("Moving to " + currentStage);
        if(currentStage > stages)
        {
            timer = Time.time + Random.Range(minWait + 2, maxWait+2);
            currentState = State.prepareAttack;
        }
        else
        {
            timer = Time.time + Random.Range(minWait, maxWait);
            currentState = State.waiting;
        }

        //Position monster to location here

        //Play optional sounds to indicate change here

    }

    //How does monster deal with being detected
    void S_Detected()
    {
        Debug.Log("Detected!");
        //Can stand still until not detected anymore. or just react and go back

        currentState = State.movingBack;
    }

    //Player has done something to move enemy back
    void S_MovingBack()
    {
        currentStage = 0;
        Debug.Log("Back to " + currentStage);
        timer = Time.time + Random.Range(minWait, maxWait);
        currentState = State.waiting;
        //Position monster to location here
    }

    //Time to hold untill attack. 
    //last chance to hold enemy back
    //Good time for alert, or potentail jump scare
    void S_PrepareAttack()
    {
        Debug.Log("Preparing Attack");
        //Can check for conditions to attack here
        float currentTime = Time.time;
        if (timer < currentTime)
        {
            currentState = State.attack;
        }
    }

    //Game Over to player
    void S_Attack()
    {
        Debug.Log("Attack - Game Over");
    }

    enum State
    {
        waiting,
        moving,
        detected,
        movingBack,
        prepareAttack,
        attack
    }
}
