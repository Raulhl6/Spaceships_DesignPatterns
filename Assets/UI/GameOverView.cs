using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour, IEventObsever
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _restartButton;
    

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);

        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EEventIds.GameOver, this);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().UnSubscribe(EEventIds.GameOver, this);
    }

    private void RestartGame()
    {
        ServiceLocator.Instance.GetService<IGameFacade>().StartBattle();
        gameObject.SetActive(false);
    }
    

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EEventIds.GameOver)
        {
            _scoreText.SetText(ScoreView.Instance.GetCurrentScore().ToString());
            gameObject.SetActive(true);
        }
    }
}
