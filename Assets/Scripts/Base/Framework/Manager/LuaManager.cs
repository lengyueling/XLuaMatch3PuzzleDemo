using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class LuaManager : MonoBehaviour
{
    /// <summary>
    /// 所有需要加载的Lua文件文件名
    /// </summary>
    public List<string> LuaNames = new List<string>();

    /// <summary>
    /// lua脚本的内容的缓存
    /// </summary>
    private Dictionary<string, byte[]> m_LuaScripts;

    /// <summary>
    /// 全局唯一
    /// 为了方便引用改成public
    /// </summary>
    public LuaEnv luaEnv;
    
    /// <summary>
    /// luaEnv中的大G表,保存了所有的lua对象，提供给C#调用
    /// </summary>
    public LuaTable Global
    {
        get
        {
            return luaEnv.Global;
        }
    }

    public void Init()
    {
        if (luaEnv != null)
        {
            return;
        }
        m_LuaScripts = new Dictionary<string, byte[]>();
        luaEnv = new LuaEnv();
        if(AppConst.GameMode == GameMode.EditorMode)
        {
            luaEnv.AddLoader(Loader);
#if UNITY_EDITOR
            EditorLoadLuaScript();
#endif
            Manager.Event.Fire(1);
        }
        else
        {
            luaEnv.AddLoader(ABLoader);
            
            LoadLuaScript();
        }

    }

    #if UNITY_EDITOR
    /// <summary>
    /// 编辑器模式加载lua
    /// 编辑器模式下直接在assets资源目录找对应的lua脚本
    /// </summary>
    void EditorLoadLuaScript()
    {
        string[] luaFiles = Directory.GetFiles(Application.dataPath + "/Scripts/Lua/", "*.lua", SearchOption.AllDirectories);
        for (int i = 0; i < luaFiles.Length; i++)
        {
            string fileName = PathUtil.GetStandardPath(luaFiles[i]);
            byte[] file = File.ReadAllBytes(fileName);
            AddLuaScript(PathUtil.GetUnityPath(fileName), file);
        }
    }
#endif

    private byte[] Loader(ref string filePath)
    {
        string path = Application.dataPath + "/Scripts/Lua/" + filePath + ".lua";
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
        {
            Debug.Log("普通重定向失败,文件名为:" + filePath);
            return null;
        }
    }

    //使用热更新时，我们会通过加载AB包中的Lua脚本资源
    //AB包中.lua还是无法识别
    //所以打包时需要将lua脚本后缀加入.txt
    private byte[] ABLoader(ref string filePath)
    {
        return GetABLuaScript(filePath);
    }

    public byte[] GetABLuaScript(string name)
    {
        string fileName = PathUtil.GetLuaPath(name);
        //获取lua脚本的数据
        byte[] luaScript = null;
        //如果之前加载过这个数据，直接在缓存里取即可
        if (!m_LuaScripts.TryGetValue(fileName,out luaScript))
        {
            Debug.LogError("AB重定向失败,文件名为:" + fileName);
        }
        return luaScript;
    }

    public byte[] GetLuaScript(string name)
    {
        string fileName;
        if(AppConst.GameMode == GameMode.EditorMode)
        {
            fileName = "Assets/Scripts/Lua/" + name + ".lua";
        }
        else
        {
            fileName = PathUtil.GetLuaPath(name);
        }
        //获取lua脚本的数据
        byte[] luaScript = null;
        //如果之前加载过这个数据，直接在缓存里取即可
        if (!m_LuaScripts.TryGetValue(fileName,out luaScript))
        {
            Debug.LogError("未找到该文件,文件名为:" + fileName);
        }
        return luaScript;
    }

    void LoadLuaScript()
    {
        foreach (string name in LuaNames)
        {
            Manager.Resource.LoadLua(name, (UnityEngine.Object obj) =>
             {
                 AddLuaScript(name, (obj as TextAsset).bytes);
                 //如果缓存中文件的数量>=需要加载的数量
                 //就说明加载完成了
                 if (m_LuaScripts.Count >= LuaNames.Count)
                 {
                    Manager.Event.Fire(1);
                     //重置需要加载文件的列表
                     LuaNames.Clear();
                     LuaNames = null;
                 }
             });
        }
    }

    /// <summary>
    /// 添加lua脚本到缓存中
    /// </summary>
    /// <param name="assetNmae"></param>
    /// <param name="luaScript"></param>
    public void AddLuaScript(string assetsName,byte[] luaScript)
    {
        m_LuaScripts[assetsName] = luaScript;
    }


    public void DoLuaFile(string fileName)
    {
        string str = string.Format("require'{0}'", fileName);
        DoString(str);
    }

    public void DoString(string str)
    {
        if (luaEnv == null)
        {
            return;
        }
        luaEnv.DoString(str);
    }

    public void Tick()
    {
        if (luaEnv == null)
        {
            return;
        }
        luaEnv.Tick();
    }

    public void Dispose()
    {
        luaEnv.Dispose();
        luaEnv = null;
    }
}
