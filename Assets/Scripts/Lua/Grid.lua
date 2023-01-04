function OnInit()
    Grid = {}
    Grid.xDim = 8
    Grid.yDim = 8
    Grid.backGround = nil
    Grid.red = nil
    Grid.green = nil
    Grid.blue = nil
    Grid.yellow = nil
    Grid.purple = nil
    ResourceManager:LoadUI("BG", function(obj)
        Grid.backGround = obj
    end)
    ResourceManager:LoadUI("Red", function(obj)
        Grid.red = obj
    end)
    ResourceManager:LoadUI("Green", function(obj)
        Grid.green = obj
    end)
    ResourceManager:LoadUI("Blue", function(obj)
        Grid.blue = obj
    end)
    ResourceManager:LoadUI("Yellow", function(obj)
        Grid.yellow = obj
    end)
    ResourceManager:LoadUI("Purple", function(obj)
        Grid.purple = obj
    end)
    Grid.piecePrefabs = 
    {
        ["BackGround"] = Grid.backGround,
        ["Red"] = Grid.red,
        ["Green"] = Grid.green,
        ["Blue"] = Grid.blue,
        ["Yellow"] = Grid.yellow,
        ["Purple"] = Grid.purple,
    }

    for x=1,Grid.xDim do
        for y=1,Grid.yDim do 
            Grid.backGround = GameObject.Instantiate(Grid.piecePrefabs.BackGround, Vector3(x*103-462,y*103-462,0), Quaternion.identity)
            Grid.backGround.transform:SetParent(GridLayer,false)
            Grid.backGround.name = x..":"..y
            rad = Random.Range(1,6)
            if rad < 2 then
                UIManager:InsUI(Grid.piecePrefabs.Red.name,"Grid","test", function(obj)
                    obj.transform:SetParent(Grid.backGround.transform,false)
                end)
            elseif rad < 3 then
                UIManager:InsUI(Grid.piecePrefabs.Green.name,"Grid","test", function(obj)
                    obj.transform:SetParent(Grid.backGround.transform,false)
                end)
            elseif rad < 4 then
                UIManager:InsUI(Grid.piecePrefabs.Blue.name,"Grid","test", function(obj)
                    obj.transform:SetParent(Grid.backGround.transform,false)
                end)
            elseif rad < 5 then
                UIManager:InsUI(Grid.piecePrefabs.Yellow.name,"Grid","test", function(obj)
                    obj.transform:SetParent(Grid.backGround.transform,false)
                end)
            elseif rad < 6 then
                UIManager:InsUI(Grid.piecePrefabs.Purple.name,"Grid","test", function(obj)
                    obj.transform:SetParent(Grid.backGround.transform,false)
                end)
            end
            
        end
    end

end

function Update()
    print("onupdate")
end