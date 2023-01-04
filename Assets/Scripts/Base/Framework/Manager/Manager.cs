using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作为所有manager的入口
/// </summary>
public class Manager : MonoBehaviour
{
    private static ResourceManager _resource;
    public static ResourceManager Resource
    {
        get { return _resource; }
    }

    private static LuaManager _lua;
    public static LuaManager Lua
    {
        get { return _lua; }
    }

    private static UIManager _ui;
    public static UIManager UI
    {
        get { return _ui; }
    }

    private static SceneManager _scene;
    public static SceneManager Scene
    {
        get { return _scene; }
    }
    private static EventManager _event;
    public static EventManager Event
    {
        get { return _event; }
    }

    
    private static EntityManager _entity;
    public static EntityManager Entity
    {
        get { return _entity; }
    }

    private void Awake()
    {
        _resource = this.gameObject.AddComponent<ResourceManager>();
        _lua = this.gameObject.AddComponent<LuaManager>();
        _ui = this.gameObject.AddComponent<UIManager>();
        _scene = this.gameObject.AddComponent<SceneManager>();
        _event = this.gameObject.AddComponent<EventManager>();
        _entity = this.gameObject.AddComponent<EntityManager>();
    }

}