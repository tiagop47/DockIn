namespace DockIn.Structures;

public interface ILinkedList<T> : IEnumerable<T>
{
    IEnumerator<T> GetReverseEnumerator();
    void addLast(T add);
    void addFirst(T add);
    void removeFirst();
    void remove(T obj);
    void removeLast();
    T last();
    int size();
    T? first();
    bool isEmpty();


}
