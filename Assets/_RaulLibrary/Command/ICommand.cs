using UnityEngine;

public interface ICommand
{
    Awaitable Execute();
}
