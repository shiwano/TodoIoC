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
        View.dispatcher.AddListener(TodoScrollView.REMOVE_TODO, OnTodoRemove);
        dispatcher.AddListener(TodoIoCEvent.CREATED_TODO, OnTodoCreated);
        dispatcher.AddListener(TodoIoCEvent.REMOVED_TODO, OnTodoRemoved);
    }

    public override void OnRemove()
    {
        View.dispatcher.RemoveListener(TodoScrollView.REMOVE_TODO, OnTodoRemove);
        dispatcher.RemoveListener(TodoIoCEvent.CREATED_TODO, OnTodoCreated);
        dispatcher.RemoveListener(TodoIoCEvent.REMOVED_TODO, OnTodoRemoved);
    }

    private void OnTodoCreated()
    {
        View.SetTodos(TodoModel.Todos);
    }

    private void OnTodoRemove(IEvent evt)
    {
        var todo = evt.data as TodoModel.Todo;
        dispatcher.Dispatch(TodoIoCEvent.REMOVE_TODO, todo);
    }

    private  void OnTodoRemoved(IEvent evt)
    {
        var todo = evt.data as TodoModel.Todo;
        View.RemoveTodo(todo);
    }
}
