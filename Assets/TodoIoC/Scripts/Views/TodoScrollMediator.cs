using UnityEngine;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class TodoScrollMediator : EventMediator
{
    [Inject]
    public TodoScrollView View { get; set; }
    [Inject]
    public TodoModel TodoModel { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(TodoIoCEvent.CREATED_TODO, OnTodoCreated);
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(TodoIoCEvent.CREATED_TODO, OnTodoCreated);
    }

    private void OnTodoCreated()
    {
        View.SetTodos(TodoModel.Todos);
    }
}
