using UnityEngine;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class TodoInputMediator : EventMediator
{
    [Inject]
    public TodoInputView View { get; set; }

    public override void OnRegister()
    {
        View.dispatcher.AddListener(TodoInputView.END_EDIT, OnViewEndEdit);
        View.Initialize();
    }

    public override void OnRemove()
    {
        View.dispatcher.RemoveListener(TodoInputView.END_EDIT, OnViewEndEdit);
    }

    private void OnViewEndEdit(IEvent evt)
    {
        var text = evt.data as string;
        dispatcher.Dispatch(TodoIoCEvent.CREATE_TODO, text);
    }
}
