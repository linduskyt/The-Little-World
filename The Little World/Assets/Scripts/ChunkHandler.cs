using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkHandler : MonoBehaviour
{
    //Declare class variables
    short chunkSize; // Number of blocks per chunk
    Chunk activeChunk; // Chunk Player is currently standing in
    List<Chunk> chunkList; // List of Chunks

    // Start is called before the first frame update
    void Start()
    {
        chunkSize = 64;
        chunkList = new List<Chunk>();
        
        // Generate and link starting chunks
        // Start & Horizontal / Vertical Adjacents
        chunkList.Add(new Chunk(new Vector2(0, 0), 0)); // 0:Center
        chunkList.Add(new Chunk(new Vector2(1, 0), 0)); // 1:Right
        LinkChunks(0, 1);   // Center & Right

        chunkList.Add(new Chunk(new Vector2(-1, 0), 0));// 2:Left
        LinkChunks(0, 2);   // Center & Left

        chunkList.Add(new Chunk(new Vector2(0, 1), 0)); // 3:Up
        LinkChunks(0, 3);   // Center & Up

        chunkList.Add(new Chunk(new Vector2(0, -1), 0));// 4:Down
        LinkChunks(0, 4);   // Center & Down
        
        // Diagonal From Start
        chunkList.Add(new Chunk(new Vector2(1, 1), 0)); // 5:Top Right
        LinkChunks(5, 1);   // Top Right & Right
        LinkChunks(5, 3);   // Top Right & Up

        chunkList.Add(new Chunk(new Vector2(-1, 1), 0));// 6:Top Left
        LinkChunks(6, 2);   // Top Left & Left
        LinkChunks(6, 3);   // Top Left & Up

        chunkList.Add(new Chunk(new Vector2(1, -1), 0));// 7:Bottom Right
        LinkChunks(7, 1);   // Bottom Right & Right
        LinkChunks(7, 4);   // Bottom Right & Down

        chunkList.Add(new Chunk(new Vector2(-1, -1), 0));// 8:Bottom Left
        LinkChunks(8, 2);   // Bottom Left & Left
        LinkChunks(8, 4);   // Bottom left & Down
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Give two adjacent chunks each others index so they can reference each other
    void LinkChunks(short indA, short indB)
    {
        Vector2 linkVector = chunkList[indA].GetLocation() - chunkList[indB].GetLocation();
        if (linkVector.sqrMagnitude == 1) // Validate that the two chunks are horizontally or vertically adjacent
        {
            switch (linkVector.x)
            {
                case -1:        // B is to the RIGHT of A
                    chunkList[indA].SetRight(indB);
                    chunkList[indB].SetLeft(indA);
                    break;
                case 1:         // B is to the LEFT of A
                    chunkList[indA].SetLeft(indB);
                    chunkList[indB].SetRight(indA);
                    break;
                case 0:
                    switch (linkVector.y)
                    {
                        case -1:    // B is above A
                            chunkList[indA].SetUp(indB);
                            chunkList[indB].SetDown(indA);
                            break;
                        case 1:     // B is below A
                            chunkList[indA].SetDown(indB);
                            chunkList[indB].SetUp(indA);
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
