print("lua Main")
--面向对象相关
require("Object")
--字符串拆分
require("SplitTools")
--Json解析
Json = require("JsonUtility")

--Unity相关
GameObject = CS.UnityEngine.GameObject
Resources = CS.UnityEngine.Resources
Transform = CS.UnityEngine.Transform
RectTransform = CS.UnityEngine.RectTransform
TextAsset = CS.UnityEngine.TextAsset
Random = CS.UnityEngine.Random
--图集对象类
SpriteAtlas = CS.UnityEngine.U2D.SpriteAtlas

Vector3 = CS.UnityEngine.Vector3
Vector2 = CS.UnityEngine.Vector2
Quaternion = CS.UnityEngine.Quaternion

--UI相关
UI = CS.UnityEngine.UI
Image = UI.Image
Text = UI.Text
Button = UI.Button
Toggle = UI.Toggle
ScrollRect = UI.ScrollRect
UIBehaviour = CS.UnityEngine.EventSystems.UIBehaviour
Root = GameObject.Find("Root").transform

--C#脚本相关
Manager = CS.Manager
ResourceManager = Manager.Resource
UIManager = Manager.UI
EntityManager = Manager.Entity
SceneManager = Manager.Scene
PathUtil = CS.PathUtil

--层级管理相关
local ui_group = 
{
    "Grid",
    "UI",
}

local entity_group = 
{
    "Grid",
    "Player",
}
UIManager:SetUIGroup(ui_group)
EntityManager:SetEntityGroup(entity_group)
GridLayer = GameObject.Find("Group-Grid").transform

--初始化后逻辑
UIManager:OpenUI("Grid","UI","Grid")
--test
-- SceneManager:ChangeScene("Scene01","test")