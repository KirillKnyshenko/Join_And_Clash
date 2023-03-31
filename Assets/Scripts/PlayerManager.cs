using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; 
    private const float endPoint =  -100f;
    private bool _moveByTouch;
    public bool MoveByTouch => _moveByTouch;
    private Vector3 _direction;
    public List<Recruitment> RecruitmentsList = new List<Recruitment>();
    [SerializeField] private float _runSpeed, _velocity, _swipeSpeed, _roadSpeed;
    [SerializeField] private Transform _roadTransform;

    private void Start() {
        Instance = this;

        RecruitmentsList.Add(transform.GetChild(0).GetComponent<Recruitment>());
    }

    public Vector3 DebugRawDirection;
    public Vector3 DebugDirection;
    public Vector3 DebugVelocity;

    private void Update() {
        Inputs();

        if (_moveByTouch)
        {
            _direction = new Vector3(Mathf.Lerp(_direction.x, Input.GetAxis("Mouse X"), _runSpeed * Time.deltaTime), 0f);
            DebugRawDirection = _direction;
            _direction = Vector3.ClampMagnitude(_direction, 1f);
            DebugDirection = _direction;
            _roadTransform.position = new Vector3(0f, 0f, Mathf.SmoothStep(_roadTransform.position.z, endPoint, _roadSpeed * Time.deltaTime));
        }

        StickmanRotation();
    }

    private void FixedUpdate() {
        StickmanMovement();
    }

    private void Inputs() {
        if (Input.GetMouseButtonDown(0))
        {
            _moveByTouch = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _moveByTouch = false;
        }
    }

    private void StickmanMovement() {
        if (_moveByTouch)
        {
            DebugVelocity = new Vector3(_direction.x * Time.fixedDeltaTime * _swipeSpeed, 0f, 0f);
            foreach (var recruitment in RecruitmentsList)
            {
                recruitment.rb.velocity = new Vector3(_direction.x * Time.fixedDeltaTime * _swipeSpeed, 0f, 0f) ;
            }
        }
        else
        {
            foreach (var recruitment in RecruitmentsList)
            {
                DebugVelocity = Vector3.zero;
                recruitment.rb.velocity = Vector3.zero;
            }
        }
    }

    private void StickmanRotation() {
        foreach (var recruitment in RecruitmentsList)
        {
            if (recruitment.rb.velocity.magnitude > .5f)
            {
                recruitment.rb.rotation = Quaternion.Slerp(recruitment.rb.rotation, Quaternion.LookRotation(recruitment.rb.velocity, Vector3.up), Time.deltaTime * _velocity);
            }
            else
            {
                recruitment.rb.rotation = Quaternion.Slerp(recruitment.rb.rotation, Quaternion.identity, Time.deltaTime * _velocity);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + (Vector3.up * 0.5f), transform.position + (Vector3.up * 0.5f) + DebugRawDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + DebugDirection);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + DebugVelocity);
    }
}
