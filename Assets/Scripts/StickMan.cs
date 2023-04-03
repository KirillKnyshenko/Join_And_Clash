using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickMan : MonoBehaviour
{
    public UnityEvent OnRecruited;
    private int _health;
    public bool IsRecruited;

    private void Start() {
        if (IsRecruited)
        {
            Recruite();
        }
    }
    
    public void Recruite() {
        IsRecruited = true;
        OnRecruited?.Invoke();
    }
}
