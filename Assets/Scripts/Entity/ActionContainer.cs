using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionContainer : MonoBehaviour
{
    public Thrower Thrower {  get; private set; }

    private void Awake()
    {
        Thrower = GetComponent<Thrower>();
    }
}
