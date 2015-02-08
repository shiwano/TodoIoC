using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class TodoScrollView : View
{
    public const string REMOVE_TODO = "REMOVE_TODO";

    [Inject]
    public IEventDispatcher dispatcher { get; set; }

    public GameObject todoPrefab;
    public GameObject content;

    private List<TodoItem> todoItems = new List<TodoItem>();

    public void SetTodos(List<TodoModel.Todo> todos)
    {
        Clear();

        foreach (var todo in todos)
        {
            CreateTodoItem(todo);
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

    private void CreateTodoItem(TodoModel.Todo todo)
    {
        var todoItem = (Instantiate(todoPrefab) as GameObject).GetComponent<TodoItem>();
        todoItem.SetTodo(todo);
        todoItem.RectTransform.SetParent(content.transform, false);
        todoItem.removeButton.onClick.AddListener(() => dispatcher.Dispatch(REMOVE_TODO, todo));
        todoItems.Add(todoItem);
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
}
