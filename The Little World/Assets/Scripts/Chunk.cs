using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct worldObjectData
{
    short worldObjID;
    //Block coordinates of the object relative to it's chunk (0 - 63)
    sbyte x;
    sbyte y;
    sbyte z;
}

public class Chunk
{
    Vector2 location;
    short myIndex;
    short biome;
    List<worldObjectData> blockList;
    List<worldObjectData> objectList;
    bool isAlive;

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

    public Chunk(short myIndexInList, Vector2 location, short biome, short left = -1, short right = -1, short up = -1, short down = -1, short upAndRight = -1, short upAndLeft = -1, short downAndRight = -1, short downAndLeft = -1)
    {
        this.myIndex = myIndexInList;
        this.location = location;
        this.biome = biome;

        this.blockList = new List<worldObjectData>();
        this.objectList = new List<worldObjectData>();

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
		999 = Player
	*/
