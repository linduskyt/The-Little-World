using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkHandler : MonoBehaviour
{
    public static GameObject placedBlock;
    public static bool placedABlock;
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
    public GameObject ID205; // Tree_RedFruit
    public GameObject ID206; // Tree_GoldenFruit
    public GameObject ID207; // Tree_Pine
    public GameObject ID208; // Bush
    public GameObject ID209; // Bush_Flower

    /* Tiles */
    public GameObject ID501; // Grass Tile #1
    public GameObject ID502; // Grass Tile #2
    public GameObject ID503; // Grass Tile #3
    public GameObject ID504; // Grass Tile #4
    public GameObject ID505; // Grass Tile #5
    public GameObject ID506; // Dirt
    public GameObject ID507; // Fall Grass #1
    public GameObject ID508; // Fall Grass #2
    public GameObject ID509; // Fall Grass #3
    public GameObject ID510; // Fall Grass #4
    public GameObject ID511; // Fall Grass #5
    public GameObject ID512; // Fall Dirt
    public GameObject ID513; // Winter Grass #1
    public GameObject ID514; // Winter Grass #2
    public GameObject ID515; // Winter Grass #3
    public GameObject ID516; // Winter Grass #4
    public GameObject ID517; // Winter Grass #5
    public GameObject ID518; // Winter Dirt
    public GameObject ID519; // Mushroom Grass #1
    public GameObject ID520; // Mushroom Grass #2
    public GameObject ID521; // Mushroom Grass #3
    public GameObject ID522; // Mushroom Grass #4
    public GameObject ID523; // Small Mushroom #1
    public GameObject ID524; // Small Mushroom #2
    public GameObject ID525; // Small Mushroom #3
    public GameObject ID526; // Small Mushroom #4
    public GameObject ID527; // Purple Mushroom Grass #1
    public GameObject ID528; // Purple Mushroom Grass #2
    public GameObject ID529; // Purple Mushroom Grass #3
    public GameObject ID530; // Purple Mushroom Grass #4
    public GameObject ID531; // Purple Small Mushroom
    public GameObject ID532; // Grass Puddle #1
    public GameObject ID533; // Grass Puddle #2
    public GameObject ID534; // Swamp Water #1
    public GameObject ID535; // Swamp Water #2
    public GameObject ID536;

    // Current object
    public GameObject currObject;

    //Declare class variables
    private short updateCycle; // Counter to cycle through staggered pdate
    private Vector2 changingChunks; // If player crossed a chunk and a full cycle through update cases has yet to finish 
    private sbyte chunkSize; // Number of blocks per chunk
    private Chunk activeChunk; // Chunk Player is currently standing in
    private List<Chunk> chunkList; // List of Chunks
    private GameObject playerObj; // Reference to player
    private Chunk prevChunk; // Chunk player just left

    // Start is called before the first frame update
    void Start()
    {
        placedBlock = null;
        placedABlock = false;
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
        this.chunkList.Add(new Chunk(0, new Vector2(0, 0), 0, this.chunkSize)); // 0:Center
        this.chunkList.Add(new Chunk(1, new Vector2(1, 0), 0, this.chunkSize)); // 1:East
        this.chunkList.Add(new Chunk(2, new Vector2(-1, 0), 0, this.chunkSize));// 2:West
        this.chunkList.Add(new Chunk(3, new Vector2(0, 1), 0, this.chunkSize)); // 3:North
        this.chunkList.Add(new Chunk(4, new Vector2(0, -1), 0, this.chunkSize));// 4:South
        this.chunkList.Add(new Chunk(5, new Vector2(1, 1), 0, this.chunkSize)); // 5:NorthEast
        this.chunkList.Add(new Chunk(6, new Vector2(-1, 1), 0, this.chunkSize));// 6:NorthWest
        this.chunkList.Add(new Chunk(7, new Vector2(1, -1), 0, this.chunkSize));// 7:SouthEast
        this.chunkList.Add(new Chunk(8, new Vector2(-1, -1), 0, this.chunkSize));// 8:SouthWest

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
        
        this.activeChunk = this.chunkList[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        // Link any placed block to it's chunk
        if (placedABlock)
        {
            Vector2 chunkLocPlaced = WorldToChunk(placedBlock.transform.position.x, placedBlock.transform.position.y);
            placedABlock = false;
            if (chunkLocPlaced == this.activeChunk.GetLocation())
            {
                this.activeChunk.AddToBlockList(placedBlock, WorldToBlock(placedBlock.transform.position.x, placedBlock.transform.position.y));
                this.activeChunk.AddToActiveList(placedBlock);
            }
            else if (chunkLocPlaced != this.activeChunk.GetLocation())
            {
                Vector2 chunkOffsetPlaced = chunkLocPlaced - this.activeChunk.GetLocation();
                Chunk placedIn = this.activeChunk;
                for (sbyte i = 0; i < chunkLocPlaced.x; ++i)
                {
                    placedIn = this.chunkList[placedIn.GetNorth()];
                }
                for (sbyte i = 0; i > chunkLocPlaced.x; --i)
                {
                    placedIn = this.chunkList[placedIn.GetSouth()];
                }
                for (sbyte i = 0; i < chunkLocPlaced.y; ++i)
                {
                    placedIn = this.chunkList[placedIn.GetEast()];
                }
                for (sbyte i = 0; i > chunkLocPlaced.y; --i)
                {
                    placedIn = this.chunkList[placedIn.GetWest()];
                }
                placedIn.AddToBlockList(placedBlock, WorldToBlock(placedBlock.transform.position.x, placedBlock.transform.position.y));
                placedIn.AddToActiveList(placedBlock);
            }
            else
            {
                Debug.Log("Error: Unable to find chunk block placed in. @ ChunkHandler.Update()"); // Should never happen
            }
        }

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

        List<WorldObjectData> tileList = chunkToLoad.GetTileList();
        for (short i = 0; i < tileList.Count; ++i)
        {
            Vector3 objCoords = new Vector3((float)(tileList[i].x * 0.32), (float)(tileList[i].y * 0.32), tileList[i].z); // Temporary, PLEASE CREATE A FUNCTION TO SIMPLIFY LATER
            currObject = IDToObj(tileList[i].worldObjID);
            GameObject tile = Instantiate(currObject, chunkTranslation + objCoords, Quaternion.identity);
            chunkToLoad.AddToActiveList(tile);
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
                this.chunkList.Add(new Chunk(indexTemp, new Vector2(target.x, target.y), 0, this.chunkSize));
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
            case 1:
                return ID001;
                break;
            case 2:
                return ID002;
                break;
            case 3:
                return ID003;
                break;
            case 4:
                return ID004;
                break;
            case 5:
                return ID005;
                break;
            case 201:
                return ID201;
                break;
            case 202:
                return ID202;
                break;
            case 203:
                return ID203;
                break;
            case 204:
                return ID204;
                break;
            case 205:
                return ID205;
                break;
            case 206:
                return ID206;
                break;
            case 207:
                return ID207;
                break;
            case 208:
                return ID208;
                break;
            case 209:
                return ID209;
                break;
            case 501:
                return ID501;
                break;
            case 502:
                return ID502;
                break;
            case 503:
                return ID503;
                break;
            case 504:
                return ID504;
                break;
            case 505:
                return ID505;
                break;
            case 506:
                return ID506;
                break;
            case 507:
                return ID507;
                break;
            case 508:
                return ID508;
                break;
            case 509:
                return ID509;
                break;
            case 510:
                return ID510;
                break;
            case 511:
                return ID511;
                break;
            case 512:
                return ID512;
                break;
            case 513:
                return ID513;
                break;
            case 514:
                return ID514;
                break;
            case 515:
                return ID515;
                break;
            case 516:
                return ID516;
                break;
            case 517:
                return ID517;
                break;
            case 518:
                return ID518;
                break;
            case 519:
                return ID519;
                break;
            case 520:
                return ID520;
                break;
            case 521:
                return ID521;
                break;
            case 522:
                return ID522;
                break;
            case 523:
                return ID523;
                break;
            case 524:
                return ID524;
                break;
            case 525:
                return ID525;
                break;
            case 526:
                return ID526;
                break;
            case 527:
                return ID527;
                break;
            case 528:
                return ID528;
                break;
            case 529:
                return ID529;
                break;
            case 530:
                return ID530;
                break;
            case 531:
                return ID531;
                break;
            case 532:
                return ID532;
                break;
            case 533:
                return ID533;
                break;
            case 534:
                return ID534;
                break;
            case 535:
                return ID535;
                break;
        }

        return ID000;
    }
}
