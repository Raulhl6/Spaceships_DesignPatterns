using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private LevelConfiguration _levelConfiguration;
    [SerializeField] private ShipsWharehouse _shipsWharehouse;
    
    private ShipsFactory _shipsFactory;
    private float _currentTimeInSeconds;
    private int _currentConfigurationIndex;

    private void Awake()
    {
        _shipsFactory = new ShipsFactory(Instantiate(_shipsWharehouse));
    }

    private void Update()
    {
        if (_currentConfigurationIndex >= _levelConfiguration.SpawnConfigurations.Length) return;
        
        _currentTimeInSeconds += Time.deltaTime;
        
        var spawnConfiguration = _levelConfiguration.SpawnConfigurations[_currentConfigurationIndex];
        if (spawnConfiguration.TimeToSpawn > _currentTimeInSeconds) return;
        
        SpawnShips(spawnConfiguration);
        _currentConfigurationIndex++;
    }

    private void SpawnShips(SpawnConfiguration spawnConfiguration)
    {
        for (int i = 0; i < spawnConfiguration.ShipToSpawnConfigurations.Length; i++)
        {
            var shipConfiguration = spawnConfiguration.ShipToSpawnConfigurations[i];
            var spawnPosition = _spawnPositions[i % _spawnPositions.Length];
            var shipBuilder = _shipsFactory.Create(shipConfiguration.ShipId.Value,
                spawnPosition.position,
                spawnPosition.rotation);
            
            shipBuilder.W
                .WithInput(new InputAIAdapter(shipBuilder.Build().transform))
                .WithCheckLimits(new InitialPositionCheckLimits(shipBuilder.Build().transform, 10f))
                .WithConfiguration(shipConfiguration)
                .Build();
            
        }
    }
}
