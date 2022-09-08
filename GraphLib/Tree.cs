namespace GraphLib
{
    public class Tree<T> : Graph<T>
    {
        private readonly Dictionary<TreeNode, List<Edge>> treeAdjList;

        private TreeNode? _root;

        public T? Root { get { if (_root != null) return _root.Id;
                                else return default; } } 

        private class TreeNode : Node
        {
            public Node? Parent { get; set; }

            public List<TreeNode> Children { get; set; }

            public TreeNode(T node) : base(node) {
                Parent = null;
                Children = new List<TreeNode>();
            }
        }

        public Tree() {
            treeAdjList = new Dictionary<TreeNode, List<Edge>>();
            _root = null;
        }

        public Tree(T node) : this() => Add(new TreeNode(node));

        public override void Add(T node) => Add(new TreeNode(node));

        private void Add(TreeNode node) {
            if (_root == null) { //Add one node only to emty tree
                treeAdjList.Add(node, new List<Edge>());
                _root = node;
            } 
        }

        public override void Add(T parent, T child, double? weight = null)
        {
            TreeNode? p = null;
            foreach (KeyValuePair<TreeNode, List<Edge>> pair in treeAdjList) {
                if(EqualityComparer<T>.Default.Equals(pair.Key.Id, parent)) {
                   p = pair.Key;
                }
            }
            Add(p, new TreeNode(child), weight);
        }

        private void Add(TreeNode parent, TreeNode child, double? weight = null) {
            if (treeAdjList.ContainsKey(parent) && !treeAdjList.ContainsKey(child)) {
                /* Specified parent must be already in tree and child must not be in tree.
                 If child is already part of tree, that means that child have different parent
                and it is not allowed to any node to have more than one parent in tree. */
                treeAdjList![parent].Add(new Edge(child.Id, weight));
                treeAdjList.Add(child, new List<Edge>());
                treeAdjList[child].Add(new Edge(parent.Id, weight));
                //remove than add modified parent to dict
                foreach (KeyValuePair<TreeNode, List<Edge>> kvp in treeAdjList) {
                    if (kvp.Key == parent) {
                        parent.Children.Add(child);
                        treeAdjList.Remove(kvp.Key);
                        treeAdjList.Add(parent, kvp.Value);
                        break;
                    }
                }
            }
        }

        public T? GetParent(T node){
            TreeNode nodeToProcess = new (node);
            if (treeAdjList.ContainsKey(nodeToProcess) && (nodeToProcess != _root)) {
                return new KeyValuePair<TreeNode, List<Edge>>(nodeToProcess, treeAdjList[nodeToProcess]).Key.Parent!.Id;
            }
            else return default;
        }

        public List<T>? GetChildren(T node){
            TreeNode nodeToProcess = new(node);
            if (treeAdjList.ContainsKey(nodeToProcess)) {
                KeyValuePair<TreeNode, List<Edge>> pair = new(nodeToProcess, treeAdjList[nodeToProcess]);
                List<TreeNode> children = pair.Key.Children;
                List<T> childrenIds = children.Select(child => child.Id).ToList();
                return childrenIds;
                //return  new KeyValuePair<TreeNode, List<Edge>>(new TreeNode(node), treeAdjList[new TreeNode(node)]).Key.Children.Select(child => child.Id).ToList();
            }
            else return null;
        }
    }
}
