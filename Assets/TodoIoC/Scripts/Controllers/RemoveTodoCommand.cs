using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class RemoveTodoCommand : EventCommand
{
    [Inject]
    public TodoModel TodoModel { get; set; }

    public override void Execute()
    {
        var todo = evt.data as TodoModel.Todo;
        TodoModel.RemoveTodo(todo);
        dispatcher.Dispatch(TodoIoCEvent.REMOVED_TODO, todo);
    }
}
