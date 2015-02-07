using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class TodoScrollView : View
{
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
            var todoItem = (Instantiate(todoPrefab) as GameObject).GetComponent<TodoItem>();
            todoItem.SetTodo(todo);
            todoItem.RectTransform.SetParent(content.transform, false);
            todoItems.Add(todoItem);
        }
    }

    public void Clear()
    {
        foreach (var todoItem in todoItems)
        {
            todoItem.RectTransform.SetParent(null, false);
            Destroy(todoItem.gameObject);
        }
        todoItems.Clear();
    }
}
