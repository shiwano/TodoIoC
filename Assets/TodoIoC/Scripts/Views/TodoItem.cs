using UnityEngine;
using UnityEngine.UI;
using System;

public class TodoItem : MonoBehaviour
{
    public Text text;
    public Toggle toggle;
    public Button removeButton;

    public TodoModel.Todo Todo { get; private set; }
    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void SetTodo(TodoModel.Todo todo)
    {
        Todo = todo;
        text.text = todo.Comment;
        SetFinished(todo.IsFinished);
    }

    public void SetFinished(bool isFinished)
    {
        toggle.isOn = isFinished;
    }
}
