using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct worldObjectData
{
    short worldObjID;
    //Block coordinates of the object relative to it's chunk (0 - 63)
    int x;
    int y;
}

public class Chunk : MonoBehaviour
{
    Vector2 location;
    short biome;
    List<worldObjectData> blockList;
    List<worldObjectData> objectList;
    bool isAlive;

    // The index in the chunkList(Part of ChunkHandler.cs) of the Chunks on any given side of this chunk 
    short indxWest;
    short indxEast;
    short indxNorth;
    short indxSouth;

    public Chunk(Vector2 location, short biome, short left = -1, short right = -1, short up = -1, short down = -1)
    {
        this.location = location;
        this.biome = biome;
        
        blockList = new List<worldObjectData>();
        objectList = new List<worldObjectData>();

        indxWest = left;
        indxEast = right;
        indxNorth = up;
        indxSouth = down;

    }

    // Getters & Setters

    public Vector2 GetLocation()
    {
        return location;
    }
    // Get and Set chunkList Indices

    public short GetWest()
    {
        return indxWest;
    }

    public void SetWest (short left)
    {
        indxWest = left;
    }

    public short GetEast()
    {
        return indxEast;
    }

    public void SetEast (short right)
    {
        indxEast = right;
    }

    public short GetNorth()
    {
        return indxNorth;
    }

    public void SetNorth (short up)
    {
        indxNorth = up;
    }

    public short GetSouth()
    {
        return indxSouth;
    }

    public void SetSouth (short down)
    {
        indxSouth = down;
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
