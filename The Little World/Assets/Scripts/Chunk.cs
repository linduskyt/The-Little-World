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
        this.objPerChunk = 110;
        this.myIndex = myIndexInList;
        this.location = location;
        this.biome = biome;
        this.chunkSize = chunkSize;

        this.blockList = new List<WorldObjectData>();
        this.objectList = new List<WorldObjectData>();
        this.tileList = new List<WorldObjectData>();
        this.activeObjList = new List<GameObject>();

        if (Random.Range(0, 1) == 0)
        {
            short ID = (short)Random.Range(1, 6);
            ID += 400;
            sbyte x = (sbyte)Random.Range(0, 64);
            sbyte y = (sbyte)Random.Range(0, 64);
            sbyte z = 0;
            WorldObjectData worldObj = new WorldObjectData(ID, x, y, z);
            this.blockList.Add(worldObj);
        }

        switch (this.biome)
        {
            case 0:
                GenerateForest();
                break;
            case 1:
                GenerateAutumn();
                break;
            case 2:
                GenerateDesert();
                break;
            case 3:
                GenerateTeal();
                break;
            case 4:
                GeneratePurple();
                break;
            case 5:
                GenerateSwamp();
                break;
            case 6:
                GenerateTaint();
                break;
            case 7:
                GenerateDirt();
                break;
            case 8:
                GenerateWinter();
                break;
            default:
                break;
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

    public short GetBiome()
    {
        return this.biome;
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
    public void SetWest(short left)
    {
        this.indxWest = left;
    }

    public short GetEast()
    {
        return this.indxEast;
    }
    public void SetEast(short right)
    {
        this.indxEast = right;
    }

    public short GetNorth()
    {
        return this.indxNorth;
    }
    public void SetNorth(short up)
    {
        this.indxNorth = up;
    }

    public short GetSouth()
    {
        return this.indxSouth;
    }
    public void SetSouth(short down)
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
            switch (Random.Range(0, 11))
            {
                case 0:
                    ID = 001; // Grass Block
                    break;
                case 1:
                    ID = 003; // Stone Block
                    break;
                case 2:
                    ID = 201; // Grassy Rock
                    break;
                case 3:
                    ID = 202; // Dirt Rock
                    break;
                case 4:
                    ID = 204; // Tree
                    break;
                case 5:
                    ID = 205; // Fruit Tree
                    break;
                case 6:
                    ID = 206; // Gold Fruit Tree
                    break;
                case 7:
                    ID = 207; // Pine Tree
                    break;
                case 8:
                    ID = 208; // Bush
                    break;
                case 9:
                    ID = 209; // Flower Bush
                    break;
                case 10:
                    ID = (short)(Random.Range(0,3) + 406);
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
            for (sbyte k = 0; k < this.chunkSize; ++k)
            {
                short id = (short)Random.Range(501, 508);
                switch (id)
                {
                    case 507:
                        id = 532;
                        break;
                    case 508:
                        id = 533;
                        break;
                    default:
                        break;
                }
                this.tileList.Add(new WorldObjectData(id, i, k, 1));
            }
        }
    }


    private void GenerateAutumn()
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
            switch (Random.Range(0, 7))
            {
                case 0:
                    ID = 002; // Dirt Block
                    break;
                case 1:
                    ID = 003; // Stone Block
                    break;
                case 2:
                    ID = 210; // Fall Bush
                    break;
                case 3:
                    ID = 211; // Fall Rock
                    break;
                case 4:
                    ID = 212; // Fall Tree #1
                    break;
                case 5:
                    ID = 213; // Fall Tree #2
                    break;
                case 6:
                    ID = (short)(Random.Range(0, 3) + 406);
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
            for (sbyte k = 0; k < this.chunkSize; ++k)
            {
                this.tileList.Add(new WorldObjectData((short)Random.Range(507, 512), i, k, 1));
            }
        }
    }

    private void GenerateDesert()
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
            switch (Random.Range(0, 7))
            {
                case 0:
                    ID = 003; // Stone Block
                    break;
                case 1:
                    ID = 214; // Cactus #1
                    break;
                case 2:
                    ID = 215; // Cactus #2
                    break;
                case 3:
                    ID = 216; // Cactus #3
                    break;
                case 4:
                    ID = 217; // Dry Bush
                    break;
                case 5:
                    ID = 218; // Sand Rock
                    break;
                case 6:
                    ID = (short)(Random.Range(0, 3) + 406);
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
            for (sbyte k = 0; k < this.chunkSize; ++k)
            {
                this.tileList.Add(new WorldObjectData((short)Random.Range(536, 541), i, k, 1));
            }
        }
    }

    private void GenerateTeal()
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
            switch (Random.Range(0, 8))
            {
                case 0:
                    ID = 219; // Small Mush #1
                    break;
                case 1:
                    ID = 220; // Small Mush #2
                    break;
                case 2:
                    ID = 221; // Small Mush #3
                    break;
                case 3:
                    ID = 222; // Small Mush #4
                    break;
                case 4:
                    ID = 223; // Tall Mush #1
                    break;
                case 5:
                    ID = 224; // Tall Mush #2
                    break;
                case 6:
                    ID = 225; // Teal Rock
                    break;
                case 7:
                    ID = (short)(Random.Range(0, 3) + 406);
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
            for (sbyte k = 0; k < this.chunkSize; ++k)
            {
                this.tileList.Add(new WorldObjectData((short)Random.Range(519, 523), i, k, 1));
            }
        }
    }

    private void GeneratePurple()
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
            switch (Random.Range(0, 5))
            {
                case 0:
                    ID = 226; // Purple Small Mush
                    break;
                case 1:
                    ID = 227; // Purple Rock
                    break;
                case 2:
                    ID = 223; // Tall Mush #1
                    break;
                case 3:
                    ID = 224; // Tall Mush #2
                    break;
                case 4:
                    ID = (short)(Random.Range(0, 3) + 406);
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
            for (sbyte k = 0; k < this.chunkSize; ++k)
            {
                this.tileList.Add(new WorldObjectData((short)Random.Range(527, 531), i, k, 1));
            }
        }
    }

    private void GenerateSwamp()
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
            switch (Random.Range(0, 3))
            {
                case 0:
                    ID = 228; // Swamp Rock
                    break;
                case 1:
                    ID = 229; // Swamp Tree
                    break;
                case 2:
                    ID = (short)(Random.Range(0, 3) + 406);
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
            for (sbyte k = 0; k < this.chunkSize; ++k)
            {
                this.tileList.Add(new WorldObjectData((short)Random.Range(534, 536), i, k, 1));
            }
        }
    }

    private void GenerateTaint()
    {  
        // Fill tilemap
        for (sbyte i = 0; i < this.chunkSize; ++i)
        {
            for (sbyte k = 0; k < this.chunkSize; ++k)
            {
                this.tileList.Add(new WorldObjectData(541, i, k, 1));
            }
        }
    }

    private void GenerateDirt()
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
            switch (Random.Range(0, 5))
            {
                case 0:
                    ID = 202; // Dirt Rock
                    break;
                case 1:
                    ID = 217; // Dry Bush
                    break;
                case 2:
                    ID = 2; // Dirt Block
                    break;
                case 3:
                    ID = 3; // Stone Block
                    break;
                case 4:
                    ID = (short)(Random.Range(0, 3) + 406);
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
            for (sbyte k = 0; k < this.chunkSize; ++k)
            {
                short id = (short)Random.Range(506, 509);
                switch (id)
                {
                    case 507:
                        id = 512;
                        break;
                    case 508:
                        id = 518;
                        break;
                    default:
                        break;
                }
                this.tileList.Add(new WorldObjectData(id, i, k, 1));
            }
        }
    }

    private void GenerateWinter()
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
            switch (Random.Range(0, 4))
            {
                case 0:
                    ID = 230; // Winter Tree
                    break;
                case 1:
                    ID = 231; // Snow Bush
                    break;
                case 2:
                    ID = 232; // Snow Rock
                    break;
                case 3:
                    ID = (short)(Random.Range(0, 3) + 406);
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
            for (sbyte k = 0; k < this.chunkSize; ++k)
            {
                this.tileList.Add(new WorldObjectData((short)Random.Range(513, 518), i, k, 1));
            }
        }
    }
}


