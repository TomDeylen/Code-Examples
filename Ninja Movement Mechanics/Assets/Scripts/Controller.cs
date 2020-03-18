using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller: MonoBehaviour
{

    public abstract void Tick(float d, Inputs inp);
    public abstract void FixedTick(float d);
}
