namespace GraphLib
{
    public class Graph<T>
    {
        protected readonly Dictionary<Node, List<Edge>> adjList;

        protected class Node : IEquatable<Graph<T>.Node?>
        {
            public T Id { get; set; }

            public Node(T id)
            {
                Id = id;
            }

            public override bool Equals(object? obj) { return Equals(obj as Graph<T>.Node); }

            public bool Equals(Graph<T>.Node? other) { return other is not null && EqualityComparer<T>.Default.Equals(Id, other.Id); }

            public override int GetHashCode() { return HashCode.Combine(Id); }

            public static bool operator == (Graph<T>.Node? left, Graph<T>.Node? right) { return EqualityComparer<Graph<T>.Node>.Default.Equals(left, right); }

            public static bool operator != (Graph<T>.Node? left, Graph<T>.Node? right) { return !(left == right); }
        }

        protected class Edge {
            public Node Node { get; set; }

            public double? Weight { get; set; }

            public int? Direction { get; set; }

            public enum Directions
            {
                outgoing = -1,
                incoming = 1
            }

            public Edge(T node, double? weight = null, int? direction = null) {
                Node = new Node(node);
                Weight = weight;
                Direction = direction;
            }
        }

        public Graph() { adjList = new Dictionary<Node, List<Edge>>(); }

        public Graph(T node) : this() => Add(node);

        public virtual void Add(T node) => Add(new Node(node));

        private void Add(Node node) { if (!adjList!.ContainsKey(node)) adjList.Add(node, new List<Edge>()); }

        public virtual void Add(T node1, T node2, double? weight = null) => Add(new Node(node1), new Node(node2), weight);

        private void Add(Node node1, Node node2, double? weight = null) {
            if (!CheckConnection(node1.Id, node2.Id)) {
                Add(node1);
                adjList![node1].Add(new Edge(node2.Id, weight));
                Add(node2);
                adjList[node2].Add(new Edge(node1.Id, weight));
            }
        }

        public void Remove(T node) {
            //Removes node from graph
            Node nodeToRemove = new (node);
            if (adjList!.ContainsKey(nodeToRemove)) {
                foreach (Edge edge in adjList[nodeToRemove]) {
                    //remove edge from all adjecent edges to/from node
                    adjList[edge.Node].RemoveAll(edge => edge.Node == nodeToRemove);
                }
                //Remove node at the end
                adjList.Remove(nodeToRemove);
            }
        }

        public virtual void Disconnect(T node1, T node2, double? weight = null) {
            Node firstNode = new(node1);
            Node secondNode = new(node2);
            if (adjList!.ContainsKey(firstNode) && adjList.ContainsKey(secondNode)) {
                adjList[firstNode].RemoveAll(edge => (edge.Node == secondNode) && (edge.Weight == weight));
                adjList[secondNode].RemoveAll(edge => (edge.Node == firstNode) && (edge.Weight == weight));
            }
        }

        public virtual bool CheckConnection(T node1, T node2, double? weight = null) {
            Node firstNode = new(node1);
            Node secondNode = new(node2);
            if (adjList!.ContainsKey(firstNode) && adjList.ContainsKey(secondNode))
                return adjList[firstNode].Any(edge => (edge.Node == secondNode) && (edge.Weight == weight)) && adjList[secondNode].Any(edge => (edge.Node == firstNode) && (edge.Weight == weight));
            else return false;
        }

        protected Node? GetNodeFromAdjList(Node node)
        {
            foreach (KeyValuePair<Node, List<Edge>> pair in adjList!)
            {
                if (pair.Key == node) return pair.Key;
            }
            return null;
        }
    }
}