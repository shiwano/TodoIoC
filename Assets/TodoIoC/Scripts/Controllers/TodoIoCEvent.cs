using System;
using UnityEngine;
using strange.extensions.signal.impl;

public class TodoIoCEvent
{
    public const string CREATE_TODO = "CREATE_TODO";
    public const string CREATED_TODO = "CREATED_TODO";
    public const string FINISH_TODO = "FINISH_TODO";
    public const string REMOVE_TODO = "REMOVE_TODO";
    public const string REMOVED_TODO = "REMOVED_TODO";
}