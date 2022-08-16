public class DirectedGraph<T> : Graph<T>
{
    public DirectedGraph(Node<T> node) : base(node) {}
    public DirectedGraph(Node<T> node1, Node<T> node2) : base(node1)
    {
        add_edge(node1, node2);
    }
    public override void add_edge(Node<T> node1, Node<T> node2)
    {
        if(!check_connection(node1, node2))
        {
            DirectedEdge<T> edge_out = new DirectedEdge<T>(node2, "out");
            DirectedEdge<T> edge_in = new DirectedEdge<T>(node1, "in");
            add_node(node1);
            add_node(node2);
            adj_list[node1].Add(edge_out);
            adj_list[node2].Add(edge_in);
        }
    }
    public override void add_edge(Node<T> node1, Node<T> node2, decimal weight)
    {
        if(!check_connection(node1, node2))
        {
            DirectedEdge<T> edge_out = new DirectedEdge<T>(node2, weight, "out");
            DirectedEdge<T> edge_in = new DirectedEdge<T>(node1, weight, "in");
            add_node(node1);
            add_node(node2);
            adj_list[node1].Add(edge_out);
            adj_list[node2].Add(edge_in);
        }
    }

    public override void print()
    {
        if(this.Is_Empty)
            Console.WriteLine("Graph is empty!");
        foreach(KeyValuePair<Node<T>, List<Edge<T>>> pair in adj_list)
        {
            string list = "";
            foreach(DirectedEdge<T> edge in pair.Value)
            {
                list += "(" + edge.Direction + ")" + "(" + edge.Weight + ")" + edge.Adj_Node.Data  + " ";
            }
            Console.WriteLine("{0} -> {1}", pair.Key.Data, list);
        }
    }

    public override bool check_connection(Node<T> node1, Node<T> node2)
    {
        if (adj_list.ContainsKey(node1))
        {
            return adj_list[node1].Contains(new Edge<T>(node2));
        }
        else return false;
    }
} 