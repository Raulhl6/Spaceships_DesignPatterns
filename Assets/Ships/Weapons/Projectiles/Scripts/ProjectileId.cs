using UnityEngine;

[CreateAssetMenu(menuName = "Create ProjetileId", fileName = "ProjetileId", order = 0)]
public class ProjectileId : ScriptableObject
{

    [SerializeField] private string _value;

    public string Value => _value;
}
