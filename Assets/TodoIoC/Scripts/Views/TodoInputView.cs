using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

[RequireComponent(typeof(InputField))]
public class TodoInputView : View
{
    public const string END_EDIT = "END_EDIT";

    [Inject]
    public IEventDispatcher dispatcher { get; set; }

    private InputField inputField;

    public void Initialize()
    {
        inputField = GetComponent<InputField>();
        inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
    }

    private void OnInputFieldEndEdit(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            dispatcher.Dispatch(END_EDIT, text);
            inputField.text = string.Empty;
        }
    }
}
