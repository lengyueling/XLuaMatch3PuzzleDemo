using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLogic : LuaBehaviour
{
    private Action m_LuaOnShow;
    private Action m_LuaOnHide;
    public override void Init(string luaName)
    {
        base.Init(luaName);
        //让lua OnOpen函数指定在 m_LuaOnOpen执行时候触发
        m_ScriptEnv.Get("OnShow", out m_LuaOnShow);
        m_ScriptEnv.Get("OnHide", out m_LuaOnHide);
    }
    public void OnShow()
    {
        m_LuaOnShow?.Invoke();
    }
    public void OnHide()
    {
        m_LuaOnHide?.Invoke();
    }
    protected override void Clear()
    {
        base.Clear();
        m_LuaOnShow = null;
        m_LuaOnHide = null;
    }
}