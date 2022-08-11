public class Tree<T> : Graph<T>
{
    public Tree(Node<T> root) : base(root) 
    {
        root.Parent = null;
        root.Children = new List<Node<T>>();
    }
    public override void add_edge(Node<T> parent, Node<T> child, double weight = 0)
    {
        if(adj_list.ContainsKey(parent))
        {
            base.add_edge(parent, child, weight);
            if(parent.Children != null)
            {
                parent.Children.Add(child);
            }
            else
            { 
                parent.Children = new List<Node<T>>();
                parent.Children.Add(child);
            }
        }
        else Console.WriteLine("{0} is not in the Tree!", parent);
    }
}