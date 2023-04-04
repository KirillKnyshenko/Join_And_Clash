using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; 
    private const float endPoint = -100f;
    private bool _moveByTouch;
    public bool MoveByTouch => _moveByTouch;
    private Vector3 _direction;
    public List<Recruitment> RecruitmentsList = new List<Recruitment>();
    [SerializeField] private float _runSpeed, _velocity, _swipeSpeed, _roadSpeed;
    [SerializeField] private Transform _roadTransform;

    public void Initialize() {
        Instance = this;

        GameInput.Instance.OnMove.AddListener(GameInput_OnMove);
        GameInput.Instance.OnStopMove.AddListener(GameInput_OnStopMove);

        RecruitmentsList.Add(transform.GetChild(0).GetComponent<Recruitment>());
    }

    private void Update() {
        if (_moveByTouch)
        {
            _direction = new Vector3(Mathf.Lerp(_direction.x, Input.GetAxis("Mouse X"), _runSpeed * Time.deltaTime), 0f);

            _direction = Vector3.ClampMagnitude(_direction, 1f);

            _roadTransform.position = new Vector3(0f, 0f, Mathf.SmoothStep(_roadTransform.position.z, endPoint, _roadSpeed * Time.deltaTime));
        }

        StickmanRotation();
    }

    private void FixedUpdate() {
        StickmanMovement();
    }

    private void GameInput_OnMove() {
        _moveByTouch = true;
    }

    private void GameInput_OnStopMove() {
        _moveByTouch = false;
    }

    private void StickmanMovement() {
        if (_moveByTouch)
        {
            foreach (var recruitment in RecruitmentsList)
            {
                recruitment.rb.velocity = new Vector3(_direction.x * Time.fixedDeltaTime * _swipeSpeed, 0f, 0f) ;
            }
        }
        else
        {
            foreach (var recruitment in RecruitmentsList)
            {
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
}
