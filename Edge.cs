public class Edge<T>
{
    protected Node<T> adj_node;
    protected decimal? weight;
    public Edge(Node<T> Adj_Node)
    {
        adj_node = Adj_Node;
    }
    public Edge(Node<T> Adj_Node, decimal Weight) : this(Adj_Node)
    {
        weight = Weight;
    }
    public Node<T> Adj_Node {get => adj_node;}
    public decimal? Weight {get => weight;}

    public override int GetHashCode() => Adj_Node.GetHashCode(); //Adj_Node can't be null

    public override bool Equals(object? other)
    {
        if(other == null) return false;
        if(!(other is Edge<T>)) return false;
        return ((Edge<T>) other).Adj_Node == this.Adj_Node;
    }
}
