using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class TodoScrollView : View
{
    public const string REMOVE_TODO = "REMOVE_TODO";
    public const string TOGGLE_TODO = "TOGGLE_TODO";
    public const string FILTER_BY_ALL = "FILTER_BY_ALL";
    public const string FILTER_BY_ACTIVE = "FILTER_BY_ACTIVE";
    public const string FILTER_BY_COMPLETED = "FILTER_BY_COMPLETED";

    [Inject]
    public IEventDispatcher dispatcher { get; set; }

    public GameObject todoPrefab;
    public GameObject content;
    public Text itemCountText;
    public Button allButton;
    public Button activeButton;
    public Button completedButton;
    public Color selectedButtonColor;

    private readonly List<TodoItem> todoItems = new List<TodoItem>();
    private Predicate<TodoModel.Todo> predicateForFilter;

    public void Initialize()
    {
        allButton.onClick.AddListener(() => dispatcher.Dispatch(FILTER_BY_ALL));
        activeButton.onClick.AddListener(() => dispatcher.Dispatch(FILTER_BY_ACTIVE));
        completedButton.onClick.AddListener(() => dispatcher.Dispatch(FILTER_BY_COMPLETED));

        ToggleFilterButtons(allButton);
    }

    public void SetTodos(List<TodoModel.Todo> todos)
    {
        Clear();

        foreach (var todo in todos)
        {
            var todoItem = CreateTodoItem(todo);
            FilterTodoItem(todoItem);
        }
    }

    public void RemoveTodo(TodoModel.Todo todo)
    {
        var todoItem = todoItems.Find(i => i.Todo == todo);

        if (todoItem != null)
        {
            RemoveTodoItem(todoItem);
        }
    }

    public void ToggleTodo(TodoModel.Todo todo)
    {
        var todoItem = todoItems.Find(i => i.Todo == todo);

        if (todoItem != null)
        {
            todoItem.SetFinished(todo.IsFinished);
        }
    }

    public void SetFilter(Predicate<TodoModel.Todo> predicate)
    {
        predicateForFilter = predicate;

        foreach (var todoItem in todoItems)
        {
            FilterTodoItem(todoItem);
        }
    }

    public void ToggleFilterButtons(Button button)
    {
        allButton.image.color = Color.white;
        activeButton.image.color = Color.white;
        completedButton.image.color = Color.white;

        button.image.color = selectedButtonColor;
    }

    private TodoItem CreateTodoItem(TodoModel.Todo todo)
    {
        var todoItem = (Instantiate(todoPrefab) as GameObject).GetComponent<TodoItem>();
        todoItem.SetTodo(todo);
        todoItem.RectTransform.SetParent(content.transform, false);
        todoItem.removeButton.onClick.AddListener(() => dispatcher.Dispatch(REMOVE_TODO, todo));
        todoItem.toggle.onValueChanged.AddListener(_ => dispatcher.Dispatch(TOGGLE_TODO, todo));
        todoItems.Add(todoItem);
        return todoItem;
    }

    private void Clear()
    {
        foreach (var todoItem in todoItems.ToArray())
        {
            RemoveTodoItem(todoItem);
        }
    }

    private void RemoveTodoItem(TodoItem todoItem)
    {
        todoItem.RectTransform.SetParent(null, false);
        Destroy(todoItem.gameObject);
        todoItems.Remove(todoItem);
    }

    private void FilterTodoItem(TodoItem todoItem)
    {
        var isActive = predicateForFilter == null || predicateForFilter.Invoke(todoItem.Todo);
        todoItem.gameObject.SetActive(isActive);
    }
}
