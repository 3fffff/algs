// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;

//Console.WriteLine("Hello, World!");
class Prog {
    private static void Main() {
        var tr = GenTree(3);
        var sum = TraverseTree(tr);
        Console.WriteLine(sum);
        var sum1= TravTree(tr);
        Console.WriteLine(sum1);
    }
    static void GenTreeRef(ref List<Tree>trees,int level) {
        Random random = new Random();
        for (int l = 0; l < trees.Count; l++)
        {
            List<Tree> leaf = new List<Tree>();
            for (int i = 0; i < random.Next(1,3); i++)
                leaf.Add(new Tree(level));
            trees[l].leafs = leaf;
            if(level>0)
                GenTreeRef(ref trees[l].leafs, level - 1);
            else return;
        }
    }
    static List<Tree> GenTree( int level)
    {
        List<Tree> leaf = new List<Tree>();
        Random random = new Random();
        for (int i = 0; i < random.Next(1, 5); i++) {
            leaf.Add(new Tree(level));
            if (level > 0)
                leaf[i].leafs = GenTree(level - 1);
        }            
        return leaf;
    }
    static int TraverseTree(List<Tree> tree) {
        int sum = 0;
        for (int i = 0; i < tree.Count; i++) {
            sum += tree[i].data;
            if(tree[i].leafs != null)
                sum+=TraverseTree(tree[i].leafs);
        }
        return sum;
    }
    static int TravTree(List<Tree> tree) {
        int sum = 0;
        List<Tree> tmpTree = tree;
        while (tmpTree.Count > 0) {
            Tree current = tmpTree[0];
            tmpTree.RemoveAt(0);
            sum+=current.data;
            if(current.leafs!= null)
                foreach(var leafs in current.leafs)
                    tmpTree.Add(leafs);
        }
        return sum;
    }
}
class Tree {
    public int data;
    public List<Tree> leafs;
    public Tree(int Data) { 
        data = Data;
    }
}
