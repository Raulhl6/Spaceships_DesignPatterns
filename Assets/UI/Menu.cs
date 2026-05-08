using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;
    

    private void Awake()
    {
        _startButton.onClick.AddListener(StartBattle);
        _stopButton.onClick.AddListener(StopBattle);
    }

    private void StartBattle() => ServiceLocator.Instance.GetService<IGameFacade>().StartBattle();
    private void StopBattle() => ServiceLocator.Instance.GetService<IGameFacade>().StopBattle();
}
