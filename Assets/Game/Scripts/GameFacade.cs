using UnityEngine;

public class GameFacade : MonoBehaviour, IGameFacade
{

    

    public void StopBattle()
    {
        ServiceLocator.Instance.GetService<EnemySpawner>().StopAndReset();
    }
    
}
