using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private LevelConfiguration _levelConfiguration;
    [SerializeField] private ShipsWharehouse _shipsWharehouse;
    
    private ShipsFactory _shipsFactory;
    private float _currentTimeInSeconds;
    private int _currentConfigurationIndex;
    private bool _canSpawn;
    private List<ShipMediator> _spawnedShips = new List<ShipMediator>();

    private void Awake()
    {
        _shipsFactory = new ShipsFactory(Instantiate(_shipsWharehouse));
    }

    private void Update()
    {
        if (!_canSpawn) return;
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
            var shipBuilder = _shipsFactory.Create(shipConfiguration.ShipId.Value);
            
            ShipMediator ship = shipBuilder
                .WithPosition(spawnPosition.position)
                .WithRotation(spawnPosition.rotation)
                .WithInputMode(ShipBuilder.EInputMode.AI)
                .WithCheckLimitsType(ShipBuilder.ECheckLimitsTypes.InitalPosition)
                .WithConfiguration(shipConfiguration)
                .Build();
            
            _spawnedShips.Add(ship);
            
        }
    }

    public void StartSpawn()
    {
        _canSpawn = true;
    }

    public void StopAndReset()
    {
        _canSpawn = false;
        _currentTimeInSeconds = 0;
        _currentConfigurationIndex = 0;
        
        _spawnedShips.ForEach(s => Destroy(s.gameObject));
        _spawnedShips.Clear();
    }
}
