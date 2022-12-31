using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaBehaviour : MonoBehaviour
{
    /// <summary>
    /// 复用全局唯一的虚拟机
    /// </summary>
    private LuaEnv m_LuaEnv = Manager.Lua.luaEnv;
    protected LuaTable m_ScriptEnv;
    private Action m_LuaInit;
    private Action m_LuaUpdate;
    private Action m_LuaOnDestroy;

    private void Awake()
    {
        m_ScriptEnv = m_LuaEnv.NewTable();
        //为每个继承的脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        LuaTable meta = m_LuaEnv.NewTable();
        meta.Set("__index", m_LuaEnv.Global);
        m_ScriptEnv.SetMetaTable(meta);
        meta.Dispose();
        m_ScriptEnv.Set("self", this);
    }

    /// <summary>
    /// 初始化资源时候执行
    /// </summary>
    /// <param name="luaName"></param>
    public virtual void Init(string luaName)
    {
        m_LuaEnv.DoString(Manager.Lua.GetLuaScript(luaName),luaName,m_ScriptEnv);
        //初始化完成后执行，开始
        m_ScriptEnv.Get("Update", out m_LuaUpdate);
        m_ScriptEnv.Get("OnInit", out m_LuaInit);
        m_LuaInit?.Invoke();
    }

    void Update()
    {
        m_LuaUpdate?.Invoke();
    }

    /// <summary>
    /// 释放资源
    /// 虚方法允许子类重写
    /// 关闭游戏或者资源销毁时执行
    /// </summary>
    protected virtual void Clear()
    {
        m_LuaOnDestroy = null;
        m_ScriptEnv?.Dispose();
        m_ScriptEnv = null;
        m_LuaInit = null;
        m_LuaUpdate = null;
    }

    private void OnDestroy()
    {
        m_LuaOnDestroy?.Invoke();
        Clear();
    }

    private void OnApplicationQuit()
    {
        Clear();
    }
}