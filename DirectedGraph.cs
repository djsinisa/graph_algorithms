
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
    public override (Tree<T>? bfs_tree, bool? is_piparite) bfs(Node<T> start_node)
    {
        if(start_node == default) return (default, default);
        //set starting node as discovered
        //set all other nodes as not discovered
        Dictionary<Node<T>, (bool discovered, int color)> discovery_dict = undiscover_nodes(start_node);
        // list of layers
        List<List<Node<T>>> layers = new List<List<Node<T>>>();
        // starting layer consisting of start_node only
        List<Node<T>> layer0 = new List<Node<T>>(); 
        bool? biparite = true;
        layer0.Add(start_node);
        layers.Add(layer0);
        //Initialization of bfs tree
        Tree<T> bfs_tree = new Tree<T>(start_node);
        int layer_counter = 0;
        while(layers[layer_counter].Any())
        {
            List<Node<T>> current_list = new List<Node<T>>();
            foreach(Node<T> node in layers[layer_counter])
            {
                //consider each edge incident to node which goes out of node
                foreach(DirectedEdge<T> incident_edge in this.adj_list[node])
                {
                    if(discovery_dict[incident_edge.Adj_Node].discovered == false && incident_edge.Direction == "out")
                    {
                        int new_color = 1 - discovery_dict[node].color;
                        discovery_dict[incident_edge.Adj_Node] = (true, new_color);
                        current_list.Add(incident_edge.Adj_Node);
                        bfs_tree.add_edge(node, incident_edge.Adj_Node);
                    }
                    else
                    {
                        // if two adjecent nodes in graph have same color, graph is not biparite
                        if(discovery_dict[incident_edge.Adj_Node].color == discovery_dict[node].color)
                            biparite = false;
                    }
                }
            }
            layers.Add(current_list);
            layer_counter ++;
        }
            return (bfs_tree, biparite);
    }
} 