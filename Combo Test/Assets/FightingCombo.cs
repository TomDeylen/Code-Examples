using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace testOne
{
    public enum AttackType { heavy, light, kick };
    public class FightingCombo : MonoBehaviour
    {
        [Header("Inputs")]
        public KeyCode heavyKey;
        public KeyCode lightKey;
        public KeyCode kickKey;

        [Header("Attacks")]
        public Attack heavyAttack;
        public Attack lightAttack;
        public Attack kickAttack;
        public List<ComboTypes> combos;
        public float comboLeeway = 0.2f;

        Attack curAttack = null;



        [Header("Transforms")]
        Animator ani;

        
        float timer = 0;
        float leeway = 0;
        ComboInput lastInput;
        List<int> currentCombos = new List<int>();
        bool skip = false;


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
                    //Call attack functions with the combo's attack
                    curAttack = c.comboAttack;
                    //Attack(c.comboAttack);
                });
            }
        }

        void Update()
        {
            if (curAttack != null)
            {
                if (timer > 0)
                    timer -= Time.deltaTime;
                else
                    curAttack = null;
                return;
            }

            if (currentCombos.Count > 0)
            {
                leeway += Time.deltaTime;
                if (leeway >= comboLeeway)
                {
                    if (lastInput != null)
                    {
                        Attack(GetAttackFromType(lastInput.type));
                        lastInput = null;
                    }
                    ResetCombos();
                }
            }
            else
            {
                leeway = 0;
            }

            ComboInput input = null;
            if (Input.GetKeyDown(heavyKey))
                input = new ComboInput(AttackType.heavy);
            if (Input.GetKeyDown(lightKey))
                input = new ComboInput(AttackType.light);
            if (Input.GetKeyDown(kickKey))
                input = new ComboInput(AttackType.kick);

            if (input == null)
                return;

            lastInput = input;

            List<int> remove = new List<int>();
            for (int i = 0; i < currentCombos.Count; i++)
            {
                ComboTypes c = combos[currentCombos[i]];
                if (c.continueCombo(input))
                {
                    leeway = 0;
                }
                else
                {
                    remove.Add(i);
                }
            }

            foreach (int i in remove)
            {
                currentCombos.RemoveAt(i);
            }


            for (int i = 0; i < combos.Count; i++)
            {
                if (currentCombos.Contains(i)) continue;
                if (combos[i].continueCombo(input))
                {
                    currentCombos.Add(i);
                    leeway = 0;
                }
            }

            if (currentCombos.Count <= 0)
            {
                Attack(GetAttackFromType(input.type));
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
            for (int i = 0; i < currentCombos.Count; i++)
            {
                ComboTypes c = combos[currentCombos[i]];
                c.ResetCombo();
            }
            currentCombos.Clear();
        }

    }


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

    [System.Serializable]
    public class ComboTypes
    {
        public List<ComboInput> inputs;
        public Attack comboAttack;
        public UnityEvent onInputted;
        int curInput = 0;

        //Checks combo to see if it matches a combination
        public bool continueCombo(ComboInput i)
        {
            if (inputs[curInput].isSameAs(i))
            {
                curInput++;
                if (curInput >= inputs.Count) // Finished the inputs and we should do the attack
                {
                    onInputted.Invoke();
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


}