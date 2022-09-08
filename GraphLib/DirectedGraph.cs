namespace GraphLib
{
    public class DirectedGraph<T> : Graph<T>
    {
        public DirectedGraph() : base() { }

        public DirectedGraph(T node) : base(node) { }

        public override void Add(T node1, T node2, double? weight = null) => Add(new Node(node1), new Node(node2), weight);

        private void Add(Node node1, Node node2, double? weight = null) {
            if (!CheckConnection(node1.Id, node2.Id)){
                Add(node1.Id);
                adjList[node1].Add(new Edge(node2.Id, weight, (int?)Edge.Directions.outgoing));
                Add(node2.Id);
                adjList[node2].Add(new Edge(node1.Id, weight, (int?)Edge.Directions.incoming));
            }
        }

        public override void Disconnect(T node1, T node2, double? weight = null) {
            Node firstNode = new (node1);
            Node secondNode = new(node2);
            if (adjList.ContainsKey(firstNode) && adjList.ContainsKey(secondNode)) adjList[firstNode].RemoveAll(edge => (edge.Node == secondNode) && edge.Weight == weight && edge.Direction == (int?)Edge.Directions.outgoing);
        }

        public override bool CheckConnection(T node1, T node2, double? weight = null) {
            Node firstNode = new(node1);
            Node secondNode = new(node2);
            if (adjList.ContainsKey(firstNode) && adjList.ContainsKey(secondNode))
                //Check if there is connection from node1 to node2 with particular weight and node1--->node2 direction
                return adjList[firstNode].Any(edge => (edge.Node == secondNode) && edge.Weight == weight && edge.Direction == (int?)Edge.Directions.outgoing);
            else return false;
        }
    }
}
