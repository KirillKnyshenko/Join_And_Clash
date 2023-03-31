using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; 
    private const string PLAYER_RUN = "run";
    private bool _moveByTouch;
    private Vector3 _direction;
    public List<Recruitment> RecruitmentsList = new List<Recruitment>();
    [SerializeField] private float _runSpeed, _velocity, _swipeSpeed, _roadSpeed;
    [SerializeField] private Transform _roadTransform;

    private void Start() {
        Instance = this;

        RecruitmentsList.Add(transform.GetChild(0).GetComponent<Recruitment>());
    }

    private void Update() {
        Inputs();

        if (_moveByTouch)
        {
            _direction = new Vector3(Mathf.Lerp(_direction.x, Input.GetAxis("Mouse X"), _runSpeed * Time.deltaTime), 0f);
        
            _direction =  Vector3.ClampMagnitude(_direction, 1f);

            _roadTransform.position = new Vector3(0f, 0f, Mathf.SmoothStep(_roadTransform.position.z, -100f, _roadSpeed * Time.deltaTime));

            foreach (var stickMan in RecruitmentsList)
            {
                stickMan.Animator.SetFloat(PLAYER_RUN, 1f);
            }
        }
        else
        {
            foreach (var stickMan in RecruitmentsList)
            {
                stickMan.Animator.SetFloat(PLAYER_RUN, 1f);
            }
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
            Vector3 Displacement = new Vector3(_direction.x, 0f, 0f) * Time.fixedDeltaTime;

            foreach (var stickMan in RecruitmentsList)
            {
                stickMan.rb.velocity = new Vector3(_direction.x * Time.fixedDeltaTime * _swipeSpeed, 0f, 0f) + Displacement;
            }
        }
        else
        {
            foreach (var stickMan in RecruitmentsList)
            {
                stickMan.rb.velocity = Vector3.zero;
            }
        }
    }

    private void StickmanRotation() {
        foreach (var stickMan in RecruitmentsList)
        {
            if (stickMan.rb.velocity.magnitude > .5f)
            {
                stickMan.rb.rotation = Quaternion.Slerp(stickMan.rb.rotation, Quaternion.LookRotation(stickMan.rb.velocity, Vector3.up), Time.deltaTime * _velocity);
            }
            else
            {
                stickMan.rb.rotation = Quaternion.Slerp(stickMan.rb.rotation, Quaternion.identity, Time.deltaTime * _velocity);
            }
        }
    }
}
