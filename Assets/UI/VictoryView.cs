using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryView : MonoBehaviour, IEventObsever
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameFacade _gameFacade;
    

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);

        EventQueue.Instance.Subscribe(EEventIds.Victory, this);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventQueue.Instance.UnSubscribe(EEventIds.Victory, this);
    }

    private void RestartGame()
    {
        _gameFacade.StartBattle();
        gameObject.SetActive(false);
    }
    

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EEventIds.Victory)
        {
            _scoreText.SetText(ScoreView.Instance.GetCurrentScore().ToString());
            gameObject.SetActive(true);
        }
    }
}
