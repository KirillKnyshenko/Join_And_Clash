using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruitment : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] public Animator Animator;

    private void OnCollisionEnter(Collision other) {
        StickMan stickMan = other.transform.GetComponent<StickMan>();
;
        if (stickMan != null)
        {
            stickMan.gameObject.AddComponent<Recruitment>();

            PlayerManager.Instance.RecruitmentsList.Add(stickMan.transform.GetComponent<Recruitment>());

            stickMan.transform.parent = null;

            stickMan.transform.parent = PlayerManager.Instance.transform;
        }
    }
}
