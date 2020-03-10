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
        
        // Generate and link starting chunks
        // Start & Horizontal / Vertical Adjacents
        chunkList.Add(new Chunk(new Vector2(0, 0), 0)); // 0:Center
        chunkList.Add(new Chunk(new Vector2(1, 0), 0)); // 1:East
        LinkChunks(0, 1);   // Center & East

        chunkList.Add(new Chunk(new Vector2(-1, 0), 0));// 2:West
        LinkChunks(0, 2);   // Center & West

        chunkList.Add(new Chunk(new Vector2(0, 1), 0)); // 3:North
        LinkChunks(0, 3);   // Center & North

        chunkList.Add(new Chunk(new Vector2(0, -1), 0));// 4:South
        LinkChunks(0, 4);   // Center & South
        
        // Diagonal From Start
        chunkList.Add(new Chunk(new Vector2(1, 1), 0)); // 5:NorthEast
        LinkChunks(5, 1);   // NorthEast & East
        LinkChunks(5, 3);   // NorthEast & North

        chunkList.Add(new Chunk(new Vector2(-1, 1), 0));// 6:NorthWest
        LinkChunks(6, 2);   // NorthWest & West
        LinkChunks(6, 3);   // NorthWest & North

        chunkList.Add(new Chunk(new Vector2(1, -1), 0));// 7:SouthEast
        LinkChunks(7, 1);   // SouthEast & East
        LinkChunks(7, 4);   // SouthEast & South

        chunkList.Add(new Chunk(new Vector2(-1, -1), 0));// 8:SouthWest
        LinkChunks(8, 2);   // SouthWest & West
        LinkChunks(8, 4);   // SouthWest & South
        
    }

    // Update is called once per frame
    void Update()
    {
        // Runs different checks on a cycle of 9 frames
        switch (updateCycle)
        {
            case 0:
                if (activeChunk != playerChunk)
                    activeChunk = playerChunk;
                    changingChunks = true;
                break;
            case 1:
                // Check/Load North & NorthEast chunks
                Chunk northChunk;
                Chunk northEastChunk;
                // North
                if (activeChunk.GetNorth() != -1)
                {
                    northChunk = chunkList[activeChunk.GetNorth()];
                }

                // NorthEast
                if (chunkList[activeChunk.GetNorth()].GetEast() != -1)
                {

                }
                else if (chunkList[activeChunk.GetEast()].GetNorth() != -1)
                {

                }
                else
                {

                }
                    break;
            case 2:
                // Check/Load East & SouthEast chunks
                Chunk eastChunk;
                Chunk southEastChunk;
                break;
            case 3:
                // Check/Load South & SouthWest chunks
                Chunk southChunk;
                Chunk southWestChunk;
                break;
            case 4:
                // Check/Load West & NorthWest chunks
                Chunk westChunk;
                Chunk northWestChunk;
                updateCycle = -1;
                break;
        }
        ++updateCycle;
    }

    // Give two adjacent chunks each others index so they can reference each other
    void LinkChunks(short indA, short indB)
    {
        Vector2 linkVector = chunkList[indA].GetLocation() - chunkList[indB].GetLocation();
        if (linkVector.sqrMagnitude == 1 | linkVector.sqrMagnitude == Mathf.Sqrt(2)) // Validate that the two chunks are horizontally or vertically adjacent
        {
            switch (linkVector.x)
            {
                case -1: // B is to the RIGHT of A
                    switch (linkVector.y)
                    {
                        case -1:
                            break;
                        case 1:
                            break;
                        case 0:
                            break;
                        default:
                            break;
                    }
                    chunkList[indA].SetEast(indB);
                    chunkList[indB].SetWest(indA);
                    break;
                case 1:         // B is to the LEFT of A
                    chunkList[indA].SetWest(indB);
                    chunkList[indB].SetEast(indA);
                    break;
                case 0:
                    switch (linkVector.y)
                    {
                        case -1:    // B is above A
                            chunkList[indA].SetNorth(indB);
                            chunkList[indB].SetSouth(indA);
                            break;
                        case 1:     // B is below A
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
}
