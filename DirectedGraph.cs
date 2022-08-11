public class DirectedGraph<T> : Graph<T>
{
    public DirectedGraph(Node<T> node) : base(node) {}
    public DirectedGraph(Node<T> node1, Node<T> node2) : base(node1)
    {
        add_edge(node1, node2);
    }
    public override void add_edge(Node<T> node1, Node<T> node2, double weight=0)
    {
        if(!check_connection(node1, node2, weight))
        {
            add_node(node1);
            adj_list[node1].Add((weight, node2));
        }
    }
        public override void remove(List<Node<T>> nodes)
    {
        //Removes list of nodes from adj_list
        foreach(Node<T> node in nodes)
        {
            if(adj_list.ContainsKey(node))
            {
                foreach((double weight, Node<T> dest_node) edge in adj_list[node])
                {   
                    //remove edges to node
                    if(adj_list[edge.dest_node].Contains((edge.weight, node)))
                    {
                        adj_list[edge.dest_node].Remove((edge.weight, node));
                    }
                }
                //Remove node at the end
                adj_list.Remove(node);
            }
            else
            {
                Console.WriteLine("{0} is not present in Graph.", node);
            }
        }
    }
    public override bool check_connection(Node<T> node1, Node<T> node2, double weight)
    {
        if (adj_list.ContainsKey(node1))
        {
            return adj_list[node1].Contains((weight, node2));
        }
        else return false;
    }
} 