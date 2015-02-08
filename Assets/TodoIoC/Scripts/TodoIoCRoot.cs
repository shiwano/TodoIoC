using strange.extensions.context.impl;

public class TodoIoCRoot : ContextView
{
    void Awake()
    {
        context = new TodoIoCContext(this);
    }
}
