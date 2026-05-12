using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CommandQueue
{
    public static CommandQueue Instance => _instance ??= new CommandQueue();
    private static CommandQueue _instance;

    private readonly Queue<ICommand> _commandsToExecute;
    private bool _runningCommand;

    private CommandQueue()
    {
        _commandsToExecute = new Queue<ICommand>();
        _runningCommand = false;
    }

    public void AddCommand(ICommand commandToEnqueue)
    {
        Debug.Log($"Enqueuing command: {commandToEnqueue.GetType().Name}");
        _commandsToExecute.Enqueue(commandToEnqueue);
        RunNextCommand().WrapErrors();
    }

    private async Awaitable RunNextCommand()
    {
        if (_runningCommand) return;
    
        while (_commandsToExecute.Count > 0)
        {
            _runningCommand = true;
            var commandToExecute = _commandsToExecute.Dequeue();
            await commandToExecute.Execute();
        }

        _runningCommand = false;
    }
}

public static class AwaitableExtensions
{
    // Lest show expections sin the awaitables without stopping the execution
    public static async void WrapErrors(this Awaitable awaitable)
    {
        try
        {
            await awaitable;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error executing command: {ex}");
        }
    }
    
    public static async Awaitable TaskCompleted()
    {
        await Completed();

        async Awaitable Completed() {}
    }
    
}
