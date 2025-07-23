using UnityEngine;

public class TaiNguyen : MonoBehaviour
{
    public int Tree;
    public int Rock;
    public int Leaf;
    public void AddLeaf(int countLeaf)
    {
        Leaf += countLeaf;
    }

    public void AddTree(int treecount)
    {
        Tree += treecount;
    }

    public void AddRock(int rockcount)
    {
        Rock += rockcount;
    }

    public int GetRock()
    {
        return Rock;
    }

    public int GetTree()
    {
        return Tree;
    }
    public int GetLeaf()
    {
        return Leaf;
    }
}
