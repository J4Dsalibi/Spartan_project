using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    private List<Node> childrenNodelist;

    public List<Node> ChildrenNodelist { get => childrenNodelist;}

    public bool Visited { get; set; }
    public Vector2Int BottomLeftAreaCorner { get; set; }
    public Vector2Int BottomRightAreaCorner { get; set; }
    public Vector2Int TopRightAreaCorner { get; set; }
    public Vector2Int TopLeftAreaCorner { get; set; }

    public Node Parent { get; set; }
    public int TreeLayerIndex { get; set; }

    public Node(Node parentNode)
    {
        childrenNodelist = new List<Node>();
        this.Parent = parentNode;
        if(parentNode != null)
        {
            parentNode.AddChild(this);
        }
    }

    public void AddChild(Node node)
    {
        childrenNodelist.Add(node);
    }

    public void RemoveChild(Node node)
    {
        childrenNodelist.Remove(node);
    }
}