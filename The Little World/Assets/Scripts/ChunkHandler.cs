using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkHandler : MonoBehaviour
{
    //List of objects linked to IDs
    /* Place Blocks */
    public GameObject ID000; // Tempblock
    public GameObject ID001; // Grass
    public GameObject ID002; // Dirt
    public GameObject ID003; // Stone
    public GameObject ID004; // Wood Planks
    public GameObject ID005; // Red Bricks

    /* Crops */
    public GameObject ID101; // Fruit Tree
    public GameObject ID102; // Carrot

    /* World Blocks */
    public GameObject ID201; // Boulder #1
    public GameObject ID202; // Boulder #2
    public GameObject ID203; // Tree_Fallen
    public GameObject ID204; // Tree_Normal

    // Current object
    public GameObject currObject;

    //Declare class variables
    private short updateCycle; // Counter to cycle through staggered pdate
    private Vector2 changingChunks; // If player crossed a chunk and a full cycle through update cases has yet to finish 
    private short chunkSize; // Number of blocks per chunk
    private Chunk activeChunk; // Chunk Player is currently standing in
    private List<Chunk> chunkList; // List of Chunks
    private GameObject playerObj; // Reference to player
    private Chunk prevChunk; // Chunk player just left

    // Start is called before the first frame update
    void Start()
    {
        //TEMP
        this.currObject = ID004;
        //
        this.playerObj = GameObject.Find("Player");
        this.chunkSize = 64;
        this.chunkList = new List<Chunk>();
        // The origin chunk (0, 0) goes from block at world coord (0, 0) to (chunkSize - 1, chunkSize -1)
        this.updateCycle = 0;
        this.changingChunks = new Vector2(0, 0);

        
        // Hardcoded Generation and Linking of 3x3 cluster of starting chunks
        this.chunkList.Add(new Chunk(0, new Vector2(0, 0), 0)); // 0:Center
        this.chunkList.Add(new Chunk(1, new Vector2(1, 0), 0)); // 1:East
        this.chunkList.Add(new Chunk(2, new Vector2(-1, 0), 0));// 2:West
        this.chunkList.Add(new Chunk(3, new Vector2(0, 1), 0)); // 3:North
        this.chunkList.Add(new Chunk(4, new Vector2(0, -1), 0));// 4:South
        this.chunkList.Add(new Chunk(5, new Vector2(1, 1), 0)); // 5:NorthEast
        this.chunkList.Add(new Chunk(6, new Vector2(-1, 1), 0));// 6:NorthWest
        this.chunkList.Add(new Chunk(7, new Vector2(1, -1), 0));// 7:SouthEast
        this.chunkList.Add(new Chunk(8, new Vector2(-1, -1), 0));// 8:SouthWest

        LinkChunks(0, 1);   // Center & East
        LinkChunks(0, 2);   // Center & West
        LinkChunks(0, 3);   // Center & North
        LinkChunks(0, 4);   // Center & South
        LinkChunks(5, 1);   // NorthEast & East
        LinkChunks(5, 3);   // NorthEast & North
        LinkChunks(6, 2);   // NorthWest & West
        LinkChunks(6, 3);   // NorthWest & North
        LinkChunks(7, 1);   // SouthEast & East
        LinkChunks(7, 4);   // SouthEast & South
        LinkChunks(8, 2);   // SouthWest & West
        LinkChunks(8, 4);   // SouthWest & South

        for (sbyte i = 0; i < this.chunkList.Count; ++i)
        {
            LoadChunk(this.chunkList[i]);
        }
        /*

        //Generates 3x3 grid of spawn chunks
        for (short i = -1; i < 2; ++i)
        {
            for (short k = -1; k < 2; ++k)
            {
                chunkList.Add(new Chunk((short)chunkList.Count, new Vector2(i, k), 0));
            }
        }
        //Iterative linker: (has nlog(n) time)
        for (short i = 0; i < chunkList.Count; ++i)
        {
            for (short k = i; k < chunkList.Count; ++k)
            {
                LinkChunks(i, k);
            }
        }
        */

        this.activeChunk = this.chunkList[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        // Runs different checks on a cycle of 5 frames
        switch (this.updateCycle)
        {
            case 0: // Check if player is in the active chunk, if not change the active chunk and set changingChunk to tell following cycles what direction to load/unload
                Vector2 playerChunkLoc = WorldToChunk(playerObj.transform.position.x, playerObj.transform.position.y);
                if (playerChunkLoc != this.activeChunk.GetLocation())
                {
                    Vector2 playerMoved = this.activeChunk.GetLocation() - playerChunkLoc;
                    if (playerMoved.x >= 1)
                        playerMoved.x = 1;
                    else if (playerMoved.x <= -1)
                        playerMoved.x = -1;
                    else if (playerMoved.x == 0) { }
                    //No Change, just checking
                    else { }
                    //This Should NEVER happen

                    if (playerMoved.y >= 1)
                        playerMoved.y = 1;
                    else if (playerMoved.y <= -1)
                        playerMoved.y = -1;
                    else if (playerMoved.y == 0) { }
                    //No Change, just checking
                    else { }
                    //This Should NEVER happen

                    this.prevChunk = this.activeChunk;
                    this.activeChunk = GetChunkToThe((short)-playerMoved.x, (short)-playerMoved.y, this.activeChunk);
                    this.changingChunks = playerMoved;
                }
                else
                {
                    this.changingChunks.x = 0;
                    this.changingChunks.y = 0;
                }
                break;
            case 1:
                if (this.changingChunks.x != 0 || this.changingChunks.y != 0) { 
                    // Check/Load North & NorthEast chunks
                    Chunk northChunk = GetChunkToThe(0, 1, this.activeChunk);
                    Chunk northEastChunk = GetChunkToThe(1, 1, this.activeChunk);

                    if (!northChunk.IsAlive())
                    {
                        LoadChunk(northChunk);
                    }
                    if (!northEastChunk.IsAlive())
                    {
                        LoadChunk(northEastChunk);
                    }

                    // Check/Unload North & NorthEast chunks
                    if (this.changingChunks.y == 1)
                    {
                        northChunk = GetChunkToThe(0, 1, this.prevChunk);
                        northEastChunk = GetChunkToThe(1, 1, this.prevChunk);

                        if (northChunk.IsAlive())
                        {
                            UnloadChunk(northChunk);
                        }
                        if (northEastChunk.IsAlive())
                        {
                            UnloadChunk(northEastChunk);
                        }
                    }
                }
                break;
            case 2:
                if (this.changingChunks.x != 0 || this.changingChunks.y != 0)
                {
                    // Check/Load East & SouthEast chunks
                    Chunk eastChunk = GetChunkToThe(1, 0, this.activeChunk);
                    Chunk southEastChunk = GetChunkToThe(1, -1, this.activeChunk);

                    if (!eastChunk.IsAlive())
                    {
                        LoadChunk(eastChunk);
                    }
                    if (!southEastChunk.IsAlive())
                    {
                        LoadChunk(southEastChunk);
                    }

                    // Check/Unload East & SouthEast chunks
                    if (this.changingChunks.x == 1)
                    {
                        eastChunk = GetChunkToThe(0, 1, this.prevChunk);
                        southEastChunk = GetChunkToThe(1, 1, this.prevChunk);

                        if (eastChunk.IsAlive())
                        {
                            UnloadChunk(eastChunk);
                        }
                        if (southEastChunk.IsAlive())
                        {
                            UnloadChunk(southEastChunk);
                        }
                    }
                }
                break;
            case 3:
                if (this.changingChunks.x != 0 || this.changingChunks.y != 0)
                {
                    // Check/Load South & SouthWest chunks
                    Chunk southChunk = GetChunkToThe(0, -1, this.activeChunk);
                    Chunk southWestChunk = GetChunkToThe(-1, -1, this.activeChunk);

                    if (!southChunk.IsAlive())
                    {
                        LoadChunk(southChunk);
                    }
                    if (!southWestChunk.IsAlive())
                    {
                        LoadChunk(southWestChunk);
                    }

                    // Check/Unload South & SouthWest chunks
                    if (this.changingChunks.y == -1)
                    {
                        southChunk = GetChunkToThe(0, 1, this.prevChunk);
                        southWestChunk = GetChunkToThe(1, 1, this.prevChunk);

                        if (southChunk.IsAlive())
                        {
                            UnloadChunk(southChunk);
                        }
                        if (southWestChunk.IsAlive())
                        {
                            UnloadChunk(southWestChunk);
                        }
                    }
                }
                break;
            case 4:
                if (this.changingChunks.x != 0 || this.changingChunks.y != 0)
                {
                    // Check/Load West & NorthWest chunks
                    Chunk westChunk = GetChunkToThe(-1, 0, this.activeChunk);
                    Chunk northWestChunk = GetChunkToThe(-1, 1, this.activeChunk);

                    if (!westChunk.IsAlive())
                    {
                        LoadChunk(westChunk);
                    }
                    if (!northWestChunk.IsAlive())
                    {
                        LoadChunk(northWestChunk);
                    }

                    // Check/Unload West & NorthWest chunks
                    if (this.changingChunks.x == -1)
                    {
                        westChunk = GetChunkToThe(0, 1, this.prevChunk);
                        northWestChunk = GetChunkToThe(1, 1, this.prevChunk);

                        if (westChunk.IsAlive())
                        {
                            UnloadChunk(westChunk);
                        }
                        if (northWestChunk.IsAlive())
                        {
                            UnloadChunk(northWestChunk);
                        }
                    }
                }
                this.updateCycle = -1;
                break;
        }
        ++this.updateCycle;
    }

    void LoadChunk(Chunk chunkToLoad)
    {
        chunkToLoad.Activate();
        List<WorldObjectData> blockList = chunkToLoad.GetBlockList();
        Vector3 chunkTranslation = new Vector3((float)(chunkToLoad.GetLocation().x * 64 * 0.32), (float)(chunkToLoad.GetLocation().y * 64 * 0.32), 0);

        for (short i = 0; i < blockList.Count; ++i)
        {
            Vector3 objCoords = new Vector3((float)(blockList[i].x * 0.32), (float)(blockList[i].y * 0.32), blockList[i].z); // Temporary, PLEASE CREATE A FUNCTION TO SIMPLIFY LATER
            currObject = IDToObj(blockList[i].worldObjID);
            GameObject block = Instantiate(currObject, chunkTranslation + objCoords, Quaternion.identity);
            chunkToLoad.AddToActiveList(block);
        }
    }

    void UnloadChunk(Chunk chunkToUnload)
    {
        /*
        chunkToUnload.Deactivate();
        List<GameObject> blockList = chunkToUnload.GetObjectList();
        for (short i = (short)(blockList.Count - 1); i >= 0; --i)
        {
            Destroy(blockList[i]);
        }
        Debug.Log(blockList.Count);
        Debug.Log(chunkToUnload.GetObjectList().Count);
        */
    }

    Chunk GetChunkToThe(short horizontalOffset, short verticalOffset, Chunk fromChunk)
    {
        Vector2 target = new Vector2(horizontalOffset, verticalOffset);
        short indexTemp = -1;

        switch (horizontalOffset)
        {
            case -1:
                switch (verticalOffset)
                {
                    case -1:
                        indexTemp = fromChunk.GetSouthWest();
                        break;
                    case 0:
                        indexTemp = fromChunk.GetWest();
                        break;
                    case 1:
                        indexTemp = fromChunk.GetNorthWest();
                        break;
                    default:
                        // Should Never Happen
                        break;
                }
                break;
            case 0:
                switch (verticalOffset)
                {
                    case -1:
                        indexTemp = fromChunk.GetSouth();
                        break;
                    case 1:
                        indexTemp = fromChunk.GetNorth();
                        break;
                    case 0:
                    default:
                        // Should Never Happen
                        break;
                }
                break;
            case 1:
                switch (verticalOffset)
                {
                    case -1:
                        indexTemp = fromChunk.GetSouthEast();
                        break;
                    case 0:
                        indexTemp = fromChunk.GetEast();
                        break;
                    case 1:
                        indexTemp = fromChunk.GetNorthEast();
                        break;
                    default:
                        // Should Never Happen
                        break;
                }
                break;
            default:
                // Should Never Happen
                break;
        }

        if (indexTemp == -1)
        {
            target = target + this.activeChunk.GetLocation();
            indexTemp = (short)this.chunkList.FindIndex(a => a.GetLocation() == target);
            if (indexTemp != -1)
            {
                LinkChunks(indexTemp, this.activeChunk.GetMyIndex());
            }
            else
            {
                // If there is no chunk to the north of activeChunk, create one
                indexTemp = (short)chunkList.Count;
                this.chunkList.Add(new Chunk(indexTemp, new Vector2(target.x, target.y), 0));
            }
        }
        return this.chunkList[indexTemp];
    }

    // Give two adjacent chunks each others index so they can reference each other
    void LinkChunks(short indA, short indB)
    {
        Vector2 linkVector = this.chunkList[indA].GetLocation() - this.chunkList[indB].GetLocation();
        if (linkVector.sqrMagnitude == 1 | linkVector.sqrMagnitude == Mathf.Sqrt(2)) // Validate that the two chunks are horizontally or vertically adjacent
        {
            switch (linkVector.x)
            {
                case -1: // B is to the East of A
                    switch (linkVector.y)
                    {
                        case -1: // B is to the NorthEast of A
                            this.chunkList[indA].SetNorthEast(indB);
                            this.chunkList[indB].SetSouthWest(indA);
                            break;
                        case 1: // B is to the SouthEast of A
                            this.chunkList[indA].SetSouthEast(indB);
                            this.chunkList[indB].SetNorthWest(indA);
                            break;
                        case 0: // B is directly to the East of A
                            this.chunkList[indA].SetEast(indB);
                            this.chunkList[indB].SetWest(indA);
                            break;
                        default:
                            break;
                    }
                    break;
                case 1: // B is to the West of A
                    switch (linkVector.y)
                    {
                        case -1: // B is to the NorthWest of A
                            this.chunkList[indA].SetNorthWest(indB);
                            this.chunkList[indB].SetSouthEast(indA);
                            break;
                        case 1: // B is to the SouthWest of A
                            this.chunkList[indA].SetSouthWest(indB);
                            this.chunkList[indB].SetNorthEast(indA);
                            break;
                        case 0: // B is directly to the West of A
                            this.chunkList[indA].SetWest(indB);
                            this.chunkList[indB].SetEast(indA);
                            break;
                        default:
                            break;
                    }
                    break;
                case 0:
                    switch (linkVector.y)
                    {
                        case -1:    // B is to the North of A
                            this.chunkList[indA].SetNorth(indB);
                            this.chunkList[indB].SetSouth(indA);
                            break;
                        case 1:     // B is to the South of A
                            this.chunkList[indA].SetSouth(indB);
                            this.chunkList[indB].SetNorth(indA);
                            break;
                        case 0:
                        default:
                            // THESE SHOULD NEVER HAPPEN
                            // TODO: Add Error code, should not be attempting to link chunks that are not adjacent
                            break;
                    }
                    break;
                default:
                    // THIS SHOULD NEVER HAPPEN
                    // TODO: Add Error code, should not be attempting to link chunks that are not adjacent
                    break;
            }
        }
        else
        {
            // TODO: Add Error code, should not be attempting to link chunks that are not adjacent
        }
    }

    // Coordinate conversions
    Vector2 WorldToChunk(Vector2 loc)
    {
        return WorldToChunk(loc.x, loc.y);
    }
    Vector2 WorldToChunk(float x, float y)
    {
        if (x % 0.32 != 0 || y % 0.32 != 0)
        {
            //take any coordinate in world and align it to block grid.
            if (x > 0.16F)
                x += 0.16F;
            else if (x < -0.16F)
                x -= 0.16F;

            if (y > 0.16F)
                y += 0.16F;
            else if (y < -0.16F)
                y -= 0.16F;

            x = x - (x % 0.32F);
            y = y - (y % 0.32F);
        }
        //Take alligned coord and convert to block in world
        x = (float)(x / 0.32);
        y = (float)(y / 0.32);
        //Reduce to the bottom left block in chunk, then divide by the number of blocks in a chunk to get chunk coords.
        if (x >= 0)
            x = (x - (x % this.chunkSize)) / this.chunkSize;
        else
            x = -((-x - (-x % this.chunkSize)) / this.chunkSize) - 1;
        if (y >= 0)
            y = (y - (y % this.chunkSize)) / this.chunkSize;
        else
            y = -((-y - (-y % this.chunkSize)) / this.chunkSize) - 1;

        return new Vector2(x, y);
    }

    void ChunkToWorld()
    {

    }

    void BlockToChunk()
    {

    }

    void ChunkToBlock()
    {

    }

    Vector2 WorldToBlock(Vector2 loc)
    {
        return WorldToBlock(loc.x, loc.y);
    }
    Vector2 WorldToBlock(float x, float y)
    {
        if (x%0.32 != 0 || y%0.32 != 0) { 
            //take any coordinate in world and align it to block grid.
            if (x > 0.16F)
                x += 0.16F;
            else if (x < -0.16F)
                x -= 0.16F;

            if (y > 0.16F)
                y += 0.16F;
            else if (y < -0.16F)
                y -= 0.16F;

            x = x - (x % 0.32F);
            y = y - (y % 0.32F);
        }
        //Take alligned coord and convert to block in world
        x = (float)(x / 0.32);
        y = (float)(y / 0.32);
        //Convert to block in chunk
        x = x % this.chunkSize;
        y = y % this.chunkSize;
        if (x < 0)
            x = 64 - x;
        if (y < 0)
            y = 64 - y;

        return new Vector2(x, y);
    }

    void BlockToWorld()
    {

    }

    void BuildChunk(short chunkIndex)
    {

    }

    GameObject IDToObj(short ID)
    {
        switch (ID)
        {
            case 4:
                return ID004;
                break;
            case 204:
                return ID204;
                break;
        }

        return ID000;
    }
}
