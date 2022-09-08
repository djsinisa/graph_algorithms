namespace GraphLibraryTest
{
    using GraphLib;
    [TestClass]
    public class UnitTests
    {
        class GraphNode
        {
            public int Id;

            public GraphNode(int id) => Id = id;
        }

        readonly GraphNode node1 = new(1);
        readonly GraphNode node2 = new(2);

        [TestMethod]
        public void AddEdgeAndCheckConnection()
        {
            Graph<GraphNode> graph = new ();
            graph.Add(node1, node2);
            Assert.IsTrue(graph.CheckConnection(node1, node2));
        }
        [TestMethod]
        public void AddTwoNodesAndCheckNotConnected()
        {
            Graph<GraphNode> graph = new ();
            graph.Add(node1);
            graph.Add(node2);
            Assert.IsFalse(graph.CheckConnection(node1, node2));
        }
        [TestMethod]
        public void DisconectParticularWeight()
        {
            Graph<GraphNode> graph = new ();
            graph.Add(node1, node2, 1);
            graph.Add(node1, node2, 2);
            graph.Disconnect(node1, node2, 2);
            Assert.IsTrue(graph.CheckConnection(node1, node2, 1));
            Assert.IsFalse(graph.CheckConnection(node1, node2, 2));
        }
        [TestMethod]
        public void DisconectParticularDirection()
        {
            DirectedGraph<GraphNode> graph = new();
            graph.Add(node1, node2, 1);
            graph.Add(node2, node1, 2);
            graph.Disconnect(node1, node2, 1);
            Assert.IsFalse(graph.CheckConnection(node1, node2, 1));
            Assert.IsTrue(graph.CheckConnection(node2, node1, 2));
        }
        [TestMethod]
        public void CheckRootofEmtyTree()
        {
            Tree<GraphNode> tree = new ();
            Assert.IsTrue(tree.Root == null);
        }
        [TestMethod]
        public void CheckRoot()
        {
            Tree<GraphNode> tree = new(node1);
            Assert.IsTrue(tree.Root == node1);
        }
    }
}