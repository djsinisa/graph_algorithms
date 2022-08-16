public class Tree<T> : Graph<T>
{
    private Node<T> root;
    public Node<T> Root{get{return root;}}
    public Tree(Node<T> root_node) : base(root_node) 
    {
        root = root_node;
        root.Parent = default;
        root.Children = new List<Node<T>>();
    }
    public override void add_edge(Node<T> parent, Node<T> child)
    {
        if(adj_list.ContainsKey(parent))
        {
            base.add_edge(parent, child);
            if(parent.Children != default)
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
    public override void remove_node(Node<T> node)
    {
        base.remove_node(node);
    }
    public override void remove_edge(Node<T> node1, Node<T> node2)
    {
        base.remove_edge(node1, node2);
        //To do: Remove parent/child ponters
    }
    public override void remove_edge(Node<T> node1, Node<T> node2, decimal weight)
    {
        base.remove_edge(node1, node2, weight);
        //To do: Remove parent/child ponters
    }
    public override void print()
    {
        //print root first
        if(!(Root.Children == default))
        {
           Console.WriteLine("{0} -> {1}", root.Data, take_data_from_nodes_to_print(Root.Children));
        }
        foreach(KeyValuePair<Node<T>, List<Edge<T>>> pair in adj_list)
        {
            if(pair.Key == Root) continue;
            if(!(pair.Key.Children == default))
            {
                Console.WriteLine("{0} -> {1}", pair.Key.Data, take_data_from_nodes_to_print(pair.Key.Children));
            }
        }
    }
    private string take_data_from_nodes_to_print(List<Node<T>> node_list)
    {
        string node_data_to_print = "";
        foreach(Node<T> node in node_list)
        {
            node_data_to_print += node.Data + " ";
        }
        return node_data_to_print;
    }
}