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

    private void StartBattle()
    {
        Debug.Log("Start Battle Button");
        ServiceLocator.Instance.GetService<CommandQueue>().AddCommand(new StartBattleCommand());
    }
    private void StopBattle() => ServiceLocator.Instance.GetService<IGameFacade>().StopBattle();
}
