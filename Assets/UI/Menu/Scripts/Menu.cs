using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;
    [SerializeField] private GameFacade _gameFacade;
    

    private void Awake()
    {
        _startButton.onClick.AddListener(StartBattle);
        _stopButton.onClick.AddListener(StopBattle);
    }

    private void StartBattle() => _gameFacade.StartBattle();
    private void StopBattle() => _gameFacade.StopBattle();
}
