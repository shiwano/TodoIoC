using UnityEngine;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class TodoIocContext : MVCSContext
{
    public TodoIocContext(MonoBehaviour view) : base(view)
    {
    }

    public TodoIocContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
        injectionBinder.Bind<TodoModel>().ToSingleton();

        mediationBinder.Bind<TodoInputView>().To<TodoInputMediator>();
        mediationBinder.Bind<TodoScrollView>().To<TodoScrollMediator>();

        commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
        commandBinder.Bind(TodoIoCEvent.CREATE_TODO).To<CreateTodoCommand>();
        commandBinder.Bind(TodoIoCEvent.FINISH_TODO).To<FinishTodoCommand>();
        commandBinder.Bind(TodoIoCEvent.REMOVE_TODO).To<RemoveTodoCommand>();
    }
}
