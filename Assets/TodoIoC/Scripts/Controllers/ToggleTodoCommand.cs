using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class ToggleTodoCommand : EventCommand
{
    [Inject]
    public TodoModel TodoModel { get; set; }

    public override void Execute()
    {
        var todo = evt.data as TodoModel.Todo;
        todo.Toggle();
        dispatcher.Dispatch(TodoIoCEvent.TOGGLED_TODO, todo);
    }
}
