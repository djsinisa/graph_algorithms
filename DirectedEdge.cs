public class DirectedEdge<T> : Edge<T>
{
    private string direction; // one of the ['in', 'out']
    public DirectedEdge(Node<T> Adj_Node, string Direction) : base(Adj_Node)
    {
        //Validate Direction
        HashSet<string> possible_directions = new HashSet<string>() {"in", "out"};
        if(possible_directions.Contains(Direction))
            direction = Direction;
        else throw new ArgumentOutOfRangeException("Direction must be one of the [in, out]");
    }
    public DirectedEdge(Node<T> Adj_Node, decimal Weight, string Direction) : base(Adj_Node, Weight)
    {
        //Validate Direction
        direction = Direction;
    }
    public string Direction {get => direction;}
}