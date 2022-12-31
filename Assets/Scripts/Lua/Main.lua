print("lua Main")
local group = 
{
    "Main",
    "UI",
    "Box"
}
require("InitClass")
Manager.UI:SetUIGroup(group)

Manager.UI:OpenUI("TestUI","UI","Grid")