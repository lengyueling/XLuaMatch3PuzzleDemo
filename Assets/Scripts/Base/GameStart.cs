using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameMode GameMode;
    void Start()
    {
        AppConst.GameMode = GameMode;
        Manager.Event.Subscribe(1, OnLuaInit);
        DontDestroyOnLoad(this);
        Manager.Lua.Init();
    }

    void OnLuaInit(object args)
    {
        Manager.Lua.DoLuaFile("Main");
    }

    void OnApplicationQuit()
    {
        Manager.Event.UnSubscribe(1, OnLuaInit);
    }
}