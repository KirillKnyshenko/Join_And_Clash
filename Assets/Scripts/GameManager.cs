using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private List<StickMan> _stickmansList;

    private void Awake() {
        _gameInput.Initialize();
        _playerManager.Initialize();

        foreach (var stickman in _stickmansList)
        {
            stickman.Initialize();
        }
    }
}
