using System;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGenerator
{
    RoomNode rootNode;
    List<RoomNode> allSpaceNodes = new List<RoomNode>();

    private int arenaWidth;
    private int arenaLength;

    public ArenaGenerator(int arenaWidth, int arenaLength)
    {
        this.arenaWidth = arenaWidth;
        this.arenaLength = arenaLength;
    }

    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLenghtMin)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(arenaWidth, arenaLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLenghtMin);
        List<Node> roomSpaces = StructureHelper.TraversGraphToExtractLowestLeafes(bsp.RootNode);

        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLenghtMin, roomWidthMin);
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpaces(roomSpaces);
        return new List<Node>(roomList);
    }
}