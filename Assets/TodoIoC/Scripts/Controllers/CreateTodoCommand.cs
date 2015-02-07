using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class CreateTodoCommand : EventCommand
{
    [Inject]
    public TodoModel TodoModel { get; set; }

    public override void Execute()
    {
        var comment = evt.data as string;
        var todo = TodoModel.CreateTodo(comment);
        dispatcher.Dispatch(TodoIoCEvent.CREATED_TODO, todo);
    }
}
