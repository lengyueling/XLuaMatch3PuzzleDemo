function OnInit()
    xDim = 8
    yDim = 8
    piecePrefabs = 
    {
        ["Normal"] = "prefab1"
    }
    for k,v in pairs(piecePrefabs) do
        print(k,v)
    end

end

function OnOpen()
    print("onopen")
end

function OnClose()
    print("onclose")
end

function Update()
    print("onupdate")
end