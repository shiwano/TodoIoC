using UnityEngine;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class TodoInputMediator : EventMediator
{
    [Inject]
    public TodoInputView view { get; set; }

    public override void OnRegister()
    {
        view.dispatcher.AddListener(TodoInputView.END_EDIT, OnEndEdit);
        view.Initialize();
    }

    public override void OnRemove()
    {
        view.dispatcher.RemoveListener(TodoInputView.END_EDIT, OnEndEdit);
    }

    private void OnEndEdit(IEvent evt)
    {
        var text = evt.data as string;
        dispatcher.Dispatch(TodoIoCEvent.CREATE_TODO, text);
    }
}
