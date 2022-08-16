public class DirectedEdge<T> : Edge<T>
{
    private int direction; // one of the [-1, 1']
    public DirectedEdge(Node<T> Adj_Node, int Direction) : base(Adj_Node)
    {
        //Validate Direction
        HashSet<int> possible_directions = new HashSet<int>() {-1, 1};
        if(possible_directions.Contains(Direction))
            direction = Direction;
        else throw new ArgumentOutOfRangeException("Direction must be one of the [1, ou-1t]");
    }
    public DirectedEdge(Node<T> Adj_Node, decimal Weight, int Direction) : base(Adj_Node, Weight)
    {
        //Validate Direction
        direction = Direction;
    }
    public int Direction {get => direction;}
}