class Run
{
    public static void Main()
    {
        Node<int> node1 = new Node<int>(1);
        Node<int> node2 = new Node<int>(2);
        Node<int> node3 = new Node<int>(3);
        Node<int> node4 = new Node<int>(4);
        Node<int> node5 = new Node<int>(5);
        Node<int> node6 = new Node<int>(6);
        Node<int> node7 = new Node<int>(7);
        Node<int> node8 = new Node<int>(8);
        Graph<int> g = new Graph<int>(node1, node2);
        //DirectedGraph<int> dg = new DirectedGraph<int>(node1);
        //g.add_node(node1);
        g.add_edge(node1, node3);
        g.add_edge(node2, node3);
        g.add_edge(node2, node4);
        g.add_edge(node2, node5);
        g.add_edge(node3, node5);
        g.add_edge(node3, node7);
        g.add_edge(node3, node8);
        g.add_edge(node4, node5);
        g.add_edge(node5, node6);
        g.add_edge(node7, node8);
        Console.WriteLine("------------------------");
        List<Node<int>> nodes_to_remove = new List<Node<int>>() {node1, node2, node3, node4, node5, node6, node7, node8};
        g.remove(nodes_to_remove);
        g.print();
        Console.WriteLine(g.is_biparite);
    }
}
