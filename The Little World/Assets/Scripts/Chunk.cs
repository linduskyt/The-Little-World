using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WorldObjectData
{
    public short worldObjID;
    //Block coordinates of the object relative to it's chunk (0 - 63)
    public sbyte x;
    public sbyte y;
    public sbyte z;

    public WorldObjectData(short objID, sbyte x, sbyte y, sbyte z)
    {
        this.worldObjID = objID;
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class Chunk
{
    private Vector2 location;
    private short myIndex;
    private short biome;
    private short chunkSize;
    private List<WorldObjectData> blockList;
    private List<WorldObjectData> objectList;
    private List<WorldObjectData> tileList;
    private List<GameObject> activeObjList;
    private bool isAlive;
    private sbyte objPerChunk;

    // The index in the chunkList(Part of ChunkHandler.cs) of the Chunks on any given side of this chunk 
    short indxWest;
    short indxEast;
    short indxNorth;
    short indxSouth;
    // Diagonal links
    short indxNorthEast;
    short indxNorthWest;
    short indxSouthEast;
    short indxSouthWest;

    public Chunk(short myIndexInList, Vector2 location, short biome, short chunkSize = 64, short left = -1, short right = -1, short up = -1, short down = -1, short upAndRight = -1, short upAndLeft = -1, short downAndRight = -1, short downAndLeft = -1)
    {
        this.objPerChunk = 22;
        this.myIndex = myIndexInList;
        this.location = location;
        this.biome = biome;
        this.chunkSize = chunkSize;

        this.blockList = new List<WorldObjectData>();
        this.objectList = new List<WorldObjectData>();
        this.tileList = new List<WorldObjectData>();
        this.activeObjList = new List<GameObject>();

        WorldObjectData data = new WorldObjectData(004, 0, 0, 0);
        this.blockList.Add(data);
        data = new WorldObjectData(004, 63, 0, 0);
        this.blockList.Add(data);
        data = new WorldObjectData(004, 63, 63, 0);
        this.blockList.Add(data);
        data = new WorldObjectData(004, 0, 63, 0);
        this.blockList.Add(data);

        if (this.biome == 0)
        {
            GenerateForest();
        }

        this.indxWest = left;
        this.indxEast = right;
        this.indxNorth = up;
        this.indxSouth = down;

        this.indxNorthEast = upAndRight;
        this.indxNorthWest = upAndLeft;
        this.indxSouthEast = downAndRight;
        this.indxSouthWest = downAndLeft;

    }

    // Getters & Setters
    public List<WorldObjectData> GetBlockList()
    {
        return this.blockList;
    }

    public List<WorldObjectData> GetTileList()
    {
        return this.tileList;
    }

    public List<GameObject> GetObjectList()
    {
        return this.activeObjList;
    }

    public void AddToActiveList(GameObject obj)
    {
        this.activeObjList.Add(obj);
    }

    public void AddToBlockList(GameObject obj, Vector2 pos)
    {
        sbyte x = (sbyte)pos.x;
        sbyte y = (sbyte)pos.y;
        short obID = (short)obj.GetComponent<GroundBlock>().item.Id;
        WorldObjectData data = new WorldObjectData(obID, x, y, 0);
        this.blockList.Add(data);
    }

    public Vector2 GetLocation()
    {
        return this.location;
    }

    public short GetMyIndex()
    {
        return this.myIndex;
    }

    public bool IsAlive()
    {
        return this.isAlive;
    }

    public void Activate()
    {
        this.isAlive = true;
    }

    public void Deactivate()
    {
        this.isAlive = false;
    }
    // Get and Set chunkList Indices

    public short GetWest()
    {
        return this.indxWest;
    }
    public void SetWest (short left)
    {
        this.indxWest = left;
    }

    public short GetEast()
    {
        return this.indxEast;
    }
    public void SetEast (short right)
    {
        this.indxEast = right;
    }

    public short GetNorth()
    {
        return this.indxNorth;
    }
    public void SetNorth (short up)
    {
        this.indxNorth = up;
    }

    public short GetSouth()
    {
        return this.indxSouth;
    }
    public void SetSouth (short down)
    {
        this.indxSouth = down;
    }

    public short GetNorthEast()
    {
        return this.indxNorthEast;
    }
    public void SetNorthEast(short upAndRight)
    {
        this.indxNorthEast = upAndRight;
    }

    public short GetNorthWest()
    {
        return this.indxNorthWest;
    }
    public void SetNorthWest(short upAndLeft)
    {
        this.indxNorthWest = upAndLeft;
    }

    public short GetSouthEast()
    {
        return this.indxSouthEast;
    }
    public void SetSouthEast(short downAndRight)
    {
        this.indxSouthEast = downAndRight;
    }

    public short GetSouthWest()
    {
        return this.indxSouthWest;
    }
    public void SetSouthWest(short downAndLeft)
    {
        this.indxSouthWest = downAndLeft;
    }

    //Build Chunk
    private void GenerateForest()
    {
        // Fill with objects
        for (sbyte i = 0; i < this.objPerChunk; ++i)
        {
            bool dupe = true;
            sbyte x = 0, y = 0, z = 0;
            while (dupe)
            {
                x = (sbyte)Random.Range(0, 64);
                y = (sbyte)Random.Range(0, 64);
                z = 0;
                dupe = false;
                for (sbyte k = 0; k < this.blockList.Count; ++k)
                {
                    if (x == this.blockList[k].x && y == this.blockList[k].y)
                        dupe = true;
                }
            }
            short ID = 0;
            switch (Random.Range(0,2))
            {
                case 0:
                    ID = 004;
                    break;
                case 1:
                    ID = 204;
                    break;
                default:
                    break;
            }
            WorldObjectData worldObj = new WorldObjectData(ID, x, y, z);
            this.blockList.Add(worldObj);
        }
        // Fill tilemap
        for (sbyte i = 0; i < this.chunkSize; ++i)
        {
            for (sbyte k = 0; k < this.chunkSize; ++k) {
                this.tileList.Add(new WorldObjectData(501, i, k, 0));
            }
        }
    }
}

/*
	ID List:
		000 = TempBlock
		001 = Grass
		002 = Dirt
		003 = Stone
		004 = Wood Planks
        005 = Red Bricks
		...
		201 = Bolder #1
		202 = Bolder #2
		203 = Fallen Tree
		...
		301 = Cave Entrance
		302 = Dungeon Entrance
		303 = Crypt Entrance
		304 = Catacomb Entrance
		...
		401 = NPC #1
		402 = NPC #2
		...
		421 = Snowman
		422 = Cow
		423 = Chicken
		424 = Pig
		425 = Dog
		...
		491 = Slime
		492 = Zombie
		493 = Skelton
		...
        TILES
        501 = Grass #1
        502 = Grass #2
		999 = Player
	*/
