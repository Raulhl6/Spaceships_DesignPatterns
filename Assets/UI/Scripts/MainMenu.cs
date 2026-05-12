using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonPressed);
    }

    private void OnStartButtonPressed()
    {
        ServiceLocator.Instance.GetService<CommandQueue>().AddCommand(new LoadGameScene());
    }
}
