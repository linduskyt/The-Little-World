using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkHandler : MonoBehaviour
{
    //Declare class variables
    short updateCycle; // Counter to cycle through staggered pdate
    bool changingChunks; // If player crossed a chunk and a full cycle through update cases has yet to finish
    short chunkSize; // Number of blocks per chunk
    Chunk activeChunk; // Chunk Player is currently standing in
    List<Chunk> chunkList; // List of Chunks

    // Start is called before the first frame update
    void Start()
    {
        chunkSize = 64;
        List<Chunk> chunkList = new List<Chunk>();
        updateCycle = 0;
        changingChunks = false;

        /*
        // Hardcoded Generation and Linking of 3x3 cluster of starting chunks
        chunkList.Add(new Chunk(new Vector2(0, 0), 0)); // 0:Center
        chunkList.Add(new Chunk(new Vector2(1, 0), 0)); // 1:East
        chunkList.Add(new Chunk(new Vector2(-1, 0), 0));// 2:West
        chunkList.Add(new Chunk(new Vector2(0, 1), 0)); // 3:North
        chunkList.Add(new Chunk(new Vector2(0, -1), 0));// 4:South
        chunkList.Add(new Chunk(new Vector2(1, 1), 0)); // 5:NorthEast
        chunkList.Add(new Chunk(new Vector2(-1, 1), 0));// 6:NorthWest
        chunkList.Add(new Chunk(new Vector2(1, -1), 0));// 7:SouthEast
        chunkList.Add(new Chunk(new Vector2(-1, -1), 0));// 8:SouthWest

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
        */

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
        
    }

    // Update is called once per frame
    void Update()
    {
        // Runs different checks on a cycle of 9 frames
        switch (updateCycle)
        {
            case 0:
                //if (activeChunk != playerChunk)
                 //   activeChunk = playerChunk;
                  //  changingChunks = true;
                break;
            case 1:
                // Check/Load North & NorthEast chunks
                Chunk northChunk = GetChunkToThe(0, 1);
                Chunk northEastChunk = GetChunkToThe(1, 1);

                if (!northChunk.IsAlive())
                {
                    BuildChunk(northChunk);
                }
                if (!northEastChunk.IsAlive())
                {
                    BuildChunk(northEastChunk);
                }
                break;
            case 2:
                // Check/Load East & SouthEast chunks
                Chunk eastChunk = GetChunkToThe(1, 0);
                Chunk southEastChunk = GetChunkToThe(1, -1);
                break;
            case 3:
                // Check/Load South & SouthWest chunks
                Chunk southChunk = GetChunkToThe(0, -1);
                Chunk southWestChunk = GetChunkToThe(-1, -1);
                break;
            case 4:
                // Check/Load West & NorthWest chunks
                Chunk westChunk = GetChunkToThe(-1, 0);
                Chunk northWestChunk = GetChunkToThe(-1, 1);
                updateCycle = -1;
                break;
        }
        ++updateCycle;
    }

    void BuildChunk(Chunk buildingChunk)
    {

    }

    Chunk GetChunkToThe(short horizontalOffset, short verticalOffset)
    {
        Vector2 target = new Vector2(horizontalOffset, verticalOffset);
        short indexTemp = -1;

        switch (horizontalOffset)
        {
            case -1:
                switch (verticalOffset)
                {
                    case -1:
                        indexTemp = activeChunk.GetSouthWest();
                        break;
                    case 0:
                        indexTemp = activeChunk.GetWest();
                        break;
                    case 1:
                        indexTemp = activeChunk.GetNorthWest();
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
                        indexTemp = activeChunk.GetSouth();
                        break;
                    case 1:
                        indexTemp = activeChunk.GetNorth();
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
                        indexTemp = activeChunk.GetSouthEast();
                        break;
                    case 0:
                        indexTemp = activeChunk.GetEast();
                        break;
                    case 1:
                        indexTemp = activeChunk.GetNorthEast();
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
            target = target + activeChunk.GetLocation();
            indexTemp = (short)chunkList.FindIndex(a => a.GetLocation() == target);
            if (indexTemp != -1)
            {
                LinkChunks(indexTemp, activeChunk.GetMyIndex());
            }
            else
            {
                // If there is no chunk to the north of activeChunk, create one
                indexTemp = (short)chunkList.Count;
                chunkList.Add(new Chunk(indexTemp, new Vector2(target.x, target.y), 0));
            }
        }
        return chunkList[indexTemp];
    }

    // Give two adjacent chunks each others index so they can reference each other
    void LinkChunks(short indA, short indB)
    {
        Vector2 linkVector = chunkList[indA].GetLocation() - chunkList[indB].GetLocation();
        if (linkVector.sqrMagnitude == 1 | linkVector.sqrMagnitude == Mathf.Sqrt(2)) // Validate that the two chunks are horizontally or vertically adjacent
        {
            switch (linkVector.x)
            {
                case -1: // B is to the East of A
                    switch (linkVector.y)
                    {
                        case -1: // B is to the NorthEast of A
                            chunkList[indA].SetNorthEast(indB);
                            chunkList[indB].SetSouthWest(indA);
                            break;
                        case 1: // B is to the SouthEast of A
                            chunkList[indA].SetSouthEast(indB);
                            chunkList[indB].SetNorthWest(indA);
                            break;
                        case 0: // B is directly to the East of A
                            chunkList[indA].SetEast(indB);
                            chunkList[indB].SetWest(indA);
                            break;
                        default:
                            break;
                    }
                    break;
                case 1: // B is to the West of A
                    switch (linkVector.y)
                    {
                        case -1: // B is to the NorthWest of A
                            chunkList[indA].SetNorthWest(indB);
                            chunkList[indB].SetSouthEast(indA);
                            break;
                        case 1: // B is to the SouthWest of A
                            chunkList[indA].SetSouthWest(indB);
                            chunkList[indB].SetNorthEast(indA);
                            break;
                        case 0: // B is directly to the West of A
                            chunkList[indA].SetWest(indB);
                            chunkList[indB].SetEast(indA);
                            break;
                        default:
                            break;
                    }
                    break;
                case 0:
                    switch (linkVector.y)
                    {
                        case -1:    // B is to the North of A
                            chunkList[indA].SetNorth(indB);
                            chunkList[indB].SetSouth(indA);
                            break;
                        case 1:     // B is to the South of A
                            chunkList[indA].SetSouth(indB);
                            chunkList[indB].SetNorth(indA);
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
    void WorldToChunk()
    {

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

    void WorldToBlock()
    {

    }

    void BlockToWorld()
    {

    }

    void BuildChunk(short chunkIndex)
    {

    }
}
