using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogic : LuaBehaviour
{
    private Action m_LuaOnOpen;
    private Action m_LuaOnClose;

    /// <summary>
    /// 模拟awake
    /// 只执行一次
    /// </summary>
    /// <param name="luaName"></param>
    public override void Init(string luaName)
    {
        base.Init(luaName);
        //让lua OnOpen函数指定在 m_LuaOnOpen执行时候触发
        m_ScriptEnv.Get("OnOpen", out m_LuaOnOpen);
        m_ScriptEnv.Get("OnClose", out m_LuaOnClose);
    }

    /// <summary>
    /// 模拟start
    /// </summary>
    public void OnOpen()
    {
        m_LuaOnOpen?.Invoke();
    }

    public void OnClose()
    {
        m_LuaOnClose?.Invoke();
    }

    protected override void Clear()
    {
        base.Clear();
        m_LuaOnOpen = null;
        m_LuaOnClose = null;
    }
}