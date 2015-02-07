using System;
using UnityEngine;
using strange.extensions.signal.impl;

public class TodoIoCEvent
{
    public const string CREATE_TODO = "CREATE_TODO";
    public const string CREATED_TODO = "CREATED_TODO";
    public const string FINISH_TODO = "FINISH_TODO";
    public const string REMOVE_ALL_TODOS = "REMOVE_ALL_TODOS";
    public const string REMOVED_ALL_TODOS = "REMOVE_ALL_TODOS";
}