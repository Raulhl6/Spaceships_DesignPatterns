using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour, IEventObsever
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _backToMenuButton;
    

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _backToMenuButton.onClick.AddListener(BackToMenu);

        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EEventIds.GameOver, this);
        gameObject.SetActive(false);
    }

    private void BackToMenu()
    {
        ServiceLocator.Instance.GetService<CommandQueue>().AddCommand(new LoadSceneCommand("Menu"));
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().UnSubscribe(EEventIds.GameOver, this);
    }

    private void RestartGame()
    {
        ServiceLocator.Instance.GetService<CommandQueue>().AddCommand(new StartBattleCommand());
        gameObject.SetActive(false);
    }
    

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EEventIds.GameOver)
        {
            _scoreText.SetText(ServiceLocator.Instance.GetService<ScoreView>().GetCurrentScore().ToString());
            gameObject.SetActive(true);
        }
    }
}
