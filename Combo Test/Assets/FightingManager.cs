using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightingManager : MonoBehaviour
{
    [Header("Inputs")]
    public KeyCode heavyKey;
    public KeyCode lightKey;
    public KeyCode kickKey;

    [Header("Attacks")]
    public Attack heavyAttack;
    public Attack lightAttack;
    public Attack kickAttack;
    ComboInput lastInput;

    public List<ComboTypes> combos; //List of combos
    List<int> currentCombosInChain = new List<int>(); //List of combos still in combo chain. storing current increase in chain

    public float comboButtonGap = 0.2f;
    float inputTimer;

    Attack readiedAttack = null; // The attack ready to go when input time is up
    Attack curAttack = null; // The attack currently active

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        PrimeCombos();
    }

    void PrimeCombos()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            ComboTypes c = combos[i];
            c.onInputted.AddListener(() =>
            {
                //The combo has matched. 
                //Put at top of list for next output.
                readiedAttack = c.comboAttack;
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Attacking() == false)
        {
            CheckInputs();
        }
    }

    bool Attacking()
    {
        if (curAttack != null)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
                curAttack = null;
            return true;
        }
        return false;
    }

    void CheckInputs()
    {   
        if (currentCombosInChain.Count > 0)
        {
            inputTimer += Time.deltaTime;
            if(inputTimer >= comboButtonGap)
            {
           
                //Time is up, do stored attack
                if (readiedAttack != null)
                {
                    Attack(readiedAttack);
                    
                }
                else if(lastInput != null)
                {
                    Attack(GetAttackFromType(lastInput.type));
                }
                ResetCombos();
                readiedAttack = null;
                lastInput = null;
            }

        }
        else
        {
            //No combo chain to check. keep inputTimer at 0 till we add inputs
            inputTimer = 0;
            //We may have pressed an attack that does not start a combo however, so we should check for last input
            if (lastInput != null)
            {
                Attack(GetAttackFromType(lastInput.type));
            }
        }

        //Get Inputs
        ComboInput input = null;
        if (Input.GetKeyDown(heavyKey))
            input = new ComboInput(AttackType.heavy);
        if (Input.GetKeyDown(lightKey))
            input = new ComboInput(AttackType.light);
        if (Input.GetKeyDown(kickKey))
            input = new ComboInput(AttackType.kick);

        if (input == null)
            return;
        //Store our last input for the attack if no combos matched
        lastInput = input;

        //If we overdid our combo chain for a combo attack. Kick it off our readied attack
        if(readiedAttack != null)
        {
            readiedAttack = null;
        }

        //Create list of items to remove if they don't match the combo
        List<int> remove = new List<int>();

        for (int i = 0; i < currentCombosInChain.Count; i++)
        {
            ComboTypes c = combos[currentCombosInChain[i]];
            if (c.continueCombo(input))
            {
                inputTimer = 0;
            }
            else
            {
                remove.Add(i);
            }
        }
        foreach (int i in remove)
        {
            currentCombosInChain.RemoveAt(i);
        }

        for (int i = 0; i < combos.Count; i++)
        {
            if (currentCombosInChain.Contains(i)) continue;
            if (combos[i].continueCombo(input))
            {
                currentCombosInChain.Add(i);
                inputTimer = 0;
            }
        }
        
    }

    void Attack(Attack att)
    {
        curAttack = att;
        timer = att.length;
        Debug.Log(att.name);
    }

    Attack GetAttackFromType(AttackType t)
    {
        if (t == AttackType.heavy)
            return heavyAttack;
        if (t == AttackType.light)
            return lightAttack;
        if (t == AttackType.kick)
            return kickAttack;
        return null;
    }

    void ResetCombos()
    {
        //Reset combo counts
        for (int i = 0; i < combos.Count; i++)
        {
            ComboTypes c = combos[i];
            c.ResetCombo();
        }
        //Clear combo chain
        currentCombosInChain.Clear();
    }
}

public enum AttackType { heavy, light, kick };

[System.Serializable]
public class Attack
{
    public string name;
    public float length;
}

[System.Serializable]
public class ComboInput
{
    public AttackType type;
    //Movement input goes here too for more precise combos
    public ComboInput(AttackType t)
    {
        type = t;
    }

    public bool isSameAs(ComboInput test)
    {
        return (type == test.type);// Add && movement == test.movement
    }

}

//The Combo information
[System.Serializable]
public class ComboTypes
{
    public List<ComboInput> inputs; // List of inputs requried
    public Attack comboAttack; // The attack itself
    public UnityEvent onInputted; //The function that calls this attack
    int curInput = 0;

    //Checks to see if combo is still able to be done
    //Counts up each button input, and checks if it matches that position
    //If it does it stays as a potential combo
    public bool continueCombo(ComboInput i)
    {
        if (inputs[curInput].isSameAs(i))
        {
            curInput++;
            if (curInput >= inputs.Count) // Finished the inputs and we should do the attack
            {
                onInputted.Invoke(); //Combo has been matched fully. call function
                curInput = 0;
            }
            return true;
        }
        else
        {
            curInput = 0;
            return false;
        }
    }

    public ComboInput currentComboInput()
    {
        if (curInput >= inputs.Count) return null;
        return inputs[curInput];
    }

    public void ResetCombo()
    {
        curInput = 0;
    }
}
