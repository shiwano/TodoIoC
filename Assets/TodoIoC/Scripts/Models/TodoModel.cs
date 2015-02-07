using System.Collections.Generic;

public class TodoModel
{
    public class Todo
    {
        public string Comment { get; private set; }
        public bool IsFinished { get; private set; }

        public Todo(string comment)
        {
            Comment = comment;
            IsFinished = false;
        }

        public void Toggle()
        {
            IsFinished = !IsFinished;
        }
    }

    public List<Todo> Todos { get; private set; }

    public TodoModel()
    {
        Todos = new List<Todo>();
    }

    public Todo CreateTodo(string comment)
    {
        var todo = new Todo(comment);
        Todos.Add(todo);
        return todo;
    }

    public void ClearAllTodos()
    {
        Todos.Clear();
    }
}
