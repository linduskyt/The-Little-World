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

public class Chunk
{
    Vector2 location;
    short biome;
    List<worldObjectData> blockList;
    List<worldObjectData> objectList;

    // The index in the chunkList(Part of ChunkHandler.cs) of the Chunks on any given side of this chunk 
    short indxLeft;
    short indxRight;
    short indxUp;
    short indxDown;

    public Chunk(Vector2 location, short biome, short left = -1, short right = -1, short up = -1, short down = -1)
    {
        this.location = location;
        this.biome = biome;
        
        blockList = new List<worldObjectData>();
        objectList = new List<worldObjectData>();

        indxRight = right;
        indxLeft = left;
        indxUp = up;
        indxDown = down;

    }

    // Getters & Setters

    public Vector2 GetLocation()
    {
        return location;
    }
    // Get and Set chunkList Indices

    public short GetLeft()
    {
        return indxLeft;
    }

    public void SetLeft (short left)
    {
        indxLeft = left;
    }

    public short GetRight()
    {
        return indxRight;
    }

    public void SetRight (short right)
    {
        indxRight = right;
    }

    public short GetUp()
    {
        return indxUp;
    }

    public void SetUp (short up)
    {
        indxUp = up;
    }

    public short GetDown()
    {
        return indxDown;
    }

    public void SetDown (short down)
    {
        indxDown = down;
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
