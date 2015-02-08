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
        dispatcher.AddListener(TodoIoCEvent.REMOVED_TODO, OnTodoRemoved);
        dispatcher.AddListener(TodoIoCEvent.TOGGLED_TODO, OnTodoToggled);

        View.dispatcher.AddListener(TodoScrollView.REMOVE_TODO, OnViewRemoveTodo);
        View.dispatcher.AddListener(TodoScrollView.TOGGLE_TODO, OnViewToggleTodo);
        View.dispatcher.AddListener(TodoScrollView.FILTER_BY_ALL, OnViewFilterByAll);
        View.dispatcher.AddListener(TodoScrollView.FILTER_BY_ACTIVE, OnViewFilterByActive);
        View.dispatcher.AddListener(TodoScrollView.FILTER_BY_COMPLETED, OnViewFilterByCompleted);

        View.Initialize();
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(TodoIoCEvent.CREATED_TODO, OnTodoCreated);
        dispatcher.RemoveListener(TodoIoCEvent.REMOVED_TODO, OnTodoRemoved);
        dispatcher.RemoveListener(TodoIoCEvent.TOGGLED_TODO, OnTodoToggled);

        View.dispatcher.RemoveListener(TodoScrollView.REMOVE_TODO, OnViewRemoveTodo);
        View.dispatcher.RemoveListener(TodoScrollView.TOGGLE_TODO, OnViewToggleTodo);
        View.dispatcher.RemoveListener(TodoScrollView.FILTER_BY_ALL, OnViewFilterByAll);
        View.dispatcher.RemoveListener(TodoScrollView.FILTER_BY_ACTIVE, OnViewFilterByActive);
        View.dispatcher.RemoveListener(TodoScrollView.FILTER_BY_COMPLETED, OnViewFilterByCompleted);
    }

    private void OnTodoCreated()
    {
        View.SetTodos(TodoModel.Todos);
    }

    private  void OnTodoRemoved(IEvent evt)
    {
        var todo = evt.data as TodoModel.Todo;
        View.RemoveTodo(todo);
    }

    private  void OnTodoToggled(IEvent evt)
    {
        var todo = evt.data as TodoModel.Todo;
        View.ToggleTodo(todo);
    }

    private void OnViewRemoveTodo(IEvent evt)
    {
        var todo = evt.data as TodoModel.Todo;
        dispatcher.Dispatch(TodoIoCEvent.REMOVE_TODO, todo);
    }

    private  void OnViewToggleTodo(IEvent evt)
    {
        var todo = evt.data as TodoModel.Todo;
        dispatcher.Dispatch(TodoIoCEvent.TOGGLE_TODO, todo);
    }

    private void OnViewFilterByAll()
    {
        View.SetFilter(null);
        View.ToggleFilterButtons(View.allButton);
    }

    private void OnViewFilterByActive()
    {
        View.SetFilter((todo) => !todo.IsFinished);
        View.ToggleFilterButtons(View.activeButton);
    }

    private void OnViewFilterByCompleted()
    {
        View.SetFilter((todo) => todo.IsFinished);
        View.ToggleFilterButtons(View.completedButton);
    }
}
