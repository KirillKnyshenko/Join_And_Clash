using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    public UnityEvent OnMove;
    public UnityEvent OnStopMove;

    private bool _moveByTouch;

    private void Start() {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _moveByTouch = true;    
            OnMove?.Invoke(); 
        }

        if (Input.GetMouseButtonUp(0))
        {
            _moveByTouch = false;
            OnStopMove?.Invoke(); 
        }
    }
}
