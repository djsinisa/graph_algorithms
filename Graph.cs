public class Graph<T>
{
    //adj_list : node -> (weight1, dest_node1), (weight2, dest_node2), ...
    protected Dictionary<Node<T>, List<(double weight, Node<T> dest_node)>> adj_list;
    public bool is_biparite {
                                get
                                { 
                                    Node<T>? any_node = adj_list.Keys.FirstOrDefault();
                                    if(any_node != default)
                                    { 
                                        return bfs(any_node).is_piparite;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Graph is empty!");
                                        return false;
                                    }
                                }
                            }
    public Graph(Node<T> node)
    {
        adj_list = new Dictionary<Node<T>, List<(double weight, Node<T> dest_node)>>();
        add_node(node);
    }
    public Graph(Node<T> node1, Node<T> node2)
    {
        adj_list = new Dictionary<Node<T>, List<(double weight, Node<T> dest_node)>>();
        add_edge(node1, node2);
    }
    public void add_node(Node<T> Node)
    {
        if(!(adj_list.ContainsKey(Node)))
        {
            // Node is not present in graph and will be inserted as not connected to any node
            List<(double weight, Node<T> dest_node)> empty_list = new List<(double weight, Node<T> dest_node)>();
            adj_list.Add(Node, empty_list);
        }
    }
    public virtual void add_edge(Node<T> node1, Node<T> node2, double weight=0)
    {
        //check if edge already exist
        if(!check_connection(node1, node2, weight))
            {
            add_node(node1);
            adj_list[node1].Add((weight, node2));
            add_node(node2);
            adj_list[node2].Add((weight, node1));
            }
    }
    public virtual void remove(List<Node<T>> nodes)
    {
        //Removes list of nodes from adj_list
        foreach(Node<T> node in nodes)
        {
            if(adj_list.ContainsKey(node))
            {
                foreach((double weight, Node<T> dest_node) edge in adj_list[node])
                {   
                    //remove edge from all adjecent nodes to node
                    adj_list[edge.dest_node].Remove((edge.weight, node));
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
    public void print()
    {
        if(!adj_list.Any())
            Console.WriteLine("Graph is empty!");
        foreach(KeyValuePair<Node<T>, List<(double weight, Node<T> dest_node)>> pair in adj_list)
        {
            string list = "";
            foreach(var edge in pair.Value)
            {
                list += "(" + edge.weight + ")";
                list += edge.dest_node.Data  + " ";
            }
            Console.WriteLine("{0} -> {1}", pair.Key.Data, list);
        }
    }
    public virtual bool check_connection(Node<T> node1, Node<T> node2, double weight)
    {
        if (adj_list.ContainsKey(node1) & adj_list.ContainsKey(node2))
        {
            return adj_list[node1].Contains(((weight, node2))) & adj_list[node2].Contains((weight, node1));
        }
        else return false;
    }
    private (Graph<T> bfs_tree, bool is_piparite) bfs(Node<T> start_node)
    {
        //set starting node as discovered
        //set all other nodes as not discovered
        Dictionary<Node<T>, (bool discovered, int color)> discovery_dict = undiscover_nodes(start_node);
        // list of layers
        List<List<Node<T>>> layers = new List<List<Node<T>>>();
        // starting layer consisting of start_node only
        List<Node<T>> layer0 = new List<Node<T>>(); 
        bool biparite = true;
        layer0.Add(start_node);
        layers.Add(layer0);
        //Initialization of bfs tree
        DirectedGraph<T> bfs_tree = new DirectedGraph<T>(start_node);
        int layer_counter = 0;
        while(layers[layer_counter].Any())
        {
            List<Node<T>> current_list = new List<Node<T>>();
            foreach(Node<T> node in layers[layer_counter])
            {
                //consider each edge incident to node
                foreach((double weight, Node<T> dest_node) incident_node in adj_list[node])
                {
                    if(discovery_dict[incident_node.dest_node].discovered == false)
                    {
                        int new_color = 1 - discovery_dict[node].color;
                        discovery_dict[incident_node.dest_node] = (true, new_color);
                        current_list.Add(incident_node.dest_node);
                        bfs_tree.add_edge(node, incident_node.dest_node, incident_node.weight);
                    }
                    else
                    {
                        // if two adjecent nodes in graph have same color, graph is not biparite
                        if(discovery_dict[incident_node.dest_node].color == discovery_dict[node].color)
                            biparite = false;
                    }
                }
            }
            layers.Add(current_list);
            layer_counter ++;
        }
            return (bfs_tree, biparite);
    }
    private void dfs(Node<T> start_node)
    {
        Dictionary<Node<T>, (bool explored, int color)> explored_dict = undiscover_nodes(start_node);
        Stack<Node<T>> s = new Stack<Node<T>>();
        s.Push(start_node);
        while(s.Any())
        {
            Node<T> processing_node = s.Pop();
            if(explored_dict[processing_node].explored == false)
            {
                Tuple<bool, int> temp = new Tuple<bool, int>(true, -1);
                explored_dict[processing_node] = (true, -1);
                foreach((double weight, Node<T> dest_node) node in adj_list[processing_node])
                {
                    s.Push(node.dest_node);
                }
            }
        }
    } 
    private Dictionary<Node<T>, (bool, int)> undiscover_nodes(Node<T> start_node)
    {
        //Set all nodes other than start_node to undiscovered/unexplored for bfs/dfs
        Dictionary<Node<T>, (bool discovered, int color)> discovery_dict = new Dictionary<Node<T>, (bool discovered, int color)>();
        foreach(KeyValuePair<Node<T>, List<(double weight, Node<T> dest_node)>> pair in adj_list)
        {
            discovery_dict.Add(pair.Key, (false, -1));
            if(pair.Key == start_node)
            {
                discovery_dict[pair.Key] = (true, 0);
            }
        }
        return discovery_dict;
    }
}