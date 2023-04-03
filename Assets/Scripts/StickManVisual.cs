using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StickManVisual : MonoBehaviour
{
    private const string PLAYER_RUN = "run";
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private GameObject _deathParticle;
    [SerializeField] private Animator _animator;
    [SerializeField] private StickMan _stickMan;
    [SerializeField] private Material _material;

    private void OnEnable() {
        _stickMan.OnRecruited.AddListener(StickMan_OnRecruited);
    }

    private void StickMan_OnRecruited() {
        _skinnedMeshRenderer.material = _material;
        StartCoroutine(AnimationUpdate());
    }

    private IEnumerator AnimationUpdate() {
        while (true)
        {

            if (PlayerManager.Instance.MoveByTouch)
            {
                _animator.SetFloat(PLAYER_RUN, 1f);
            }
            else
            {
                _animator.SetFloat(PLAYER_RUN, 0f);
            }
            yield return null;
        }
    }
}
