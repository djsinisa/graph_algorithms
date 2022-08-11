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
        get {return parent;} 
        set {parent = value;}
    }
    public List<Node<T>>? Children
    {
        get {return children;}
        set {children = value;}
    }
}