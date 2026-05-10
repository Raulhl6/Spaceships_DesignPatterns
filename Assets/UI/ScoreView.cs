using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour, IEventObsever
{
    
    [SerializeField] private TextMeshProUGUI _text;
    
    private int _currentScore;
    

    private void Start()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EEventIds.ShipDestroyed, this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().UnSubscribe(EEventIds.ShipDestroyed, this);
    }

    private void AddScore(ETeams killedTeam, int scoreToAdd)
    {
        if (killedTeam != ETeams.Enemy) return;
        
        _currentScore += scoreToAdd;
        _text.SetText(_currentScore.ToString());
    }

    public void Reset()
    {
        _currentScore = 0;
        _text.SetText(_currentScore.ToString());
    }

    public int GetCurrentScore() => _currentScore;

    public void Process(EventData eventData)
    {
        if (eventData.EventId != EEventIds.ShipDestroyed) return;
        
        var shipData = (ShipDestroyedEventData) eventData;
        AddScore(shipData.Team, shipData.ScoreToAdd);
    }
}
