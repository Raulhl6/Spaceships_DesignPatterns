using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour, IEventObsever
{

    
    [SerializeField] private TextMeshProUGUI _text;


    private int _currentScore;

    public static ScoreView Instance { get; private set; }
    private ScoreView _instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    private void Start()
    {
        EventQueue.Instance.Subscribe(EEventIds.ShipDestroyed, this);
    }

    private void OnDestroy()
    {
        EventQueue.Instance.UnSubscribe(EEventIds.ShipDestroyed, this);
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
