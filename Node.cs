public class Node<T>
{
    public T Data;
    private Node<T>? parent;
    private List<Node<T>>? children;
    public Node(T data)
    {
        Data = data;
    }
    public Node<T>? Parent 
    {   
        get => parent; 
        set => parent = value; // For the root parent can bee null
    }
    public List<Node<T>>? Children
    {
        get => children;
        set => children = value; // For the leaf nodes, children is null
    }
}