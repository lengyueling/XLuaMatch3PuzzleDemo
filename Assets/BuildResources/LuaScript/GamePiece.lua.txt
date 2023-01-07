local parent = nil
local x = nil
local y = nil

function Move(newX,newY)
    x = newX
    y = newY
end

function OnInit()
    parent = self.transform.parent
    x = string.sub(parent.name,1,1)
    y = string.sub(parent.name,3,3)
end

function OnOpen()
    print("onopen")
end

function Update()
    if(x ~= string.sub(parent.name,1,1) or y ~= string.sub(parent.name,3,3)) then
        self.transform:SetParent(GameObject.Find(x .. ":" .. y).transform)
        self.transform.anchoredPosition = Vector2(0,0)
    end
end
