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
        View.dispatcher.AddListener(TodoScrollView.TOGGLE_TODO, OnTodoToggle);
        dispatcher.AddListener(TodoIoCEvent.CREATED_TODO, OnTodoCreated);
        dispatcher.AddListener(TodoIoCEvent.REMOVED_TODO, OnTodoRemoved);
        dispatcher.AddListener(TodoIoCEvent.TOGGLED_TODO, OnTodoToggled);
    }

    public override void OnRemove()
    {
        View.dispatcher.RemoveListener(TodoScrollView.REMOVE_TODO, OnTodoRemove);
        View.dispatcher.RemoveListener(TodoScrollView.TOGGLE_TODO, OnTodoToggle);
        dispatcher.RemoveListener(TodoIoCEvent.CREATED_TODO, OnTodoCreated);
        dispatcher.RemoveListener(TodoIoCEvent.REMOVED_TODO, OnTodoRemoved);
        dispatcher.RemoveListener(TodoIoCEvent.TOGGLED_TODO, OnTodoToggled);
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

    private  void OnTodoToggle(IEvent evt)
    {
        var todo = evt.data as TodoModel.Todo;
        dispatcher.Dispatch(TodoIoCEvent.TOGGLE_TODO, todo);
    }

    private  void OnTodoToggled(IEvent evt)
    {
        var todo = evt.data as TodoModel.Todo;
        View.ToggleTodo(todo);
    }
}
