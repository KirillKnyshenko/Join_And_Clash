using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruitment : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        StickMan stickMan = other.transform.GetComponent<StickMan>();

        if (stickMan != null && !stickMan.IsRecruited)
        {
            stickMan.Recruite();

            stickMan.gameObject.AddComponent<Recruitment>();

            PlayerManager.Instance.RecruitmentsList.Add(stickMan.transform.GetComponent<Recruitment>());

            stickMan.transform.SetParent(PlayerManager.Instance.transform);
        }
    }
}
