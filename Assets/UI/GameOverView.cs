using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour, IEventObsever
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameFacade _gameFacade;
    

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        EventQueue.Instance.Subscribe(EEventIds.ShipDestroyed, this);
        EventQueue.Instance.Subscribe(EEventIds.GameOver, this);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventQueue.Instance.UnSubscribe(EEventIds.ShipDestroyed, this);
        EventQueue.Instance.UnSubscribe(EEventIds.GameOver, this);
    }

    private void RestartGame()
    {
        _gameFacade.StartBattle();
         gameObject.SetActive(false);
    }
    

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EEventIds.ShipDestroyed)
        {
            var shipData = (ShipDestroyedEventData) eventData;
            if (shipData.Team != ETeams.Ally) return;
            _gameFacade.StopBattle();
            EventQueue.Instance.EnqueueEvent(new EventData(EEventIds.GameOver));
        }
        else if (eventData.EventId == EEventIds.GameOver)
        {
            _scoreText.SetText(ScoreView.Instance.GetCurrentScore().ToString());
            gameObject.SetActive(true);
        }
        
        
    }
}
