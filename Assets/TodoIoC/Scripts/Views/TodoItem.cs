using UnityEngine;
using UnityEngine.UI;

public class TodoItem : MonoBehaviour
{
    public Text text;
    public Image checkmark;

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
        checkmark.gameObject.SetActive(todo.IsFinished);
    }
}
