using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// 缓存UI
    /// </summary>
    Dictionary<string, GameObject> m_UI = new Dictionary<string, GameObject>();

    /// <summary>
    /// UI分组
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <typeparam name="Transform"></typeparam>
    /// <returns></returns>
    Dictionary<string, Transform> m_UIGroups = new Dictionary<string, Transform>();
    /// <summary>
    /// UI根节点
    /// </summary>
    private Transform m_UIParent;

    private void Awake()
    {
        m_UIParent = this.transform.parent.Find("UI");
    }

    /// <summary>
    /// 设置UI层级分组
    /// UI分组也需要热更新
    /// 因此该在lua脚本中被调用
    /// </summary>
    /// <param name="group">lua代码中进行热更新传递分组</param>
    public void SetUIGroup(List<string> group)
    {
        for (int i = 0; i < group.Count; i++)
        {
            GameObject go = new GameObject("Group-" + group[i]);
            go.transform.SetParent(m_UIParent, false);
            m_UIGroups.Add(group[i], go.transform);
        }
    }

    /// <summary>
    /// 获取UI分组
    /// </summary>
    /// <returns>返回具体的UI分组，作为载入ui的父物体</returns>
    private Transform GetUIGroup(string group)
    {
        if (!m_UIGroups.ContainsKey(group))
        {
            Debug.LogError("分组不存在");
        }
        return m_UIGroups[group];
    }

    /// <summary>
    /// 打开UI
    /// 执行实例化对应UI
    /// lua代码中编写
    /// </summary>
    /// <param name="uiName">要打开UI资源的名字</param>
    /// <param name="luaName">Lua模拟MonoBehaviour使用的文件</param>
    public void OpenUI(string uiName, string group, string luaName)
    {
        GameObject ui = null;
        Transform parent = GetUIGroup(group);
        //如果之前已经缓存（使用）过了这个ui则直接调用，不需要重新加载资源，生命周期只执行一次（模拟start）
        if (m_UI.TryGetValue(uiName,out ui))
        {
            UILogic uILogic = ui.GetComponent<UILogic>();
            uILogic.OnOpen();
            return;
        }
        //资源加载并执行lambda表达式，初始化并执行OnOpen()函数（模拟awake）
        Manager.Resource.LoadUI(uiName, (Object obj) =>
         {
             ui = Instantiate(obj) as GameObject;
             ui.transform.SetParent(parent, false);
             m_UI.Add(uiName, ui);
             UILogic uiLogic = ui.AddComponent<UILogic>();
             uiLogic.Init(luaName);
             uiLogic.OnOpen();
         });
    }
}