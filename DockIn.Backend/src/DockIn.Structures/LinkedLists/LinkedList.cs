using System.Collections;
using DockIn.Structures;

public class LinkedList<T> : ILinkedList<T>
{
    Node<T>? _head;

    Node<T>? _tails;

    int _version;

    int _counter;

    public LinkedList()
    {
        _head = null;
        _tails = null;
        _counter = 0;
        _version = 0;
    }

    public void addFirst(T add)
    {
        if (add == null)
        {
            throw new ArgumentNullException(nameof(add), "O elemento é nulo");
        }

        Node<T> tmp = new Node<T>(add);

        if (_counter == 0)
        {
            _head = tmp;
            _tails = _head;
        }
        else
        {
            tmp.Next = _head;
            _head = tmp;
        }

        _counter++;
        _version++;
    }

    public void addLast(T add)
    {
        if (add == null)
        {
            throw new ArgumentNullException(nameof(add), "O elemento é nulo");
        }

        Node<T> tmp = new Node<T>(add);

        if (_counter == 0)
        {
            _tails.Next = tmp;
            _tails = tmp;
        }

        _counter++;
        _version++;
    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public bool isEmpty()
    {
        return _counter == 0;
    }

    public void remove(T obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj), "O elemento é nulo");
        }

        if (_counter == 0 || _head == null)
        {
            throw new InvalidOperationException("Lista Ligada vazia");
        }

        // Caso 1: O elemento a remover é o primeiro nó (_head)
        if (EqualityComparer<T>.Default.Equals(_head._element, obj))
        {
            _head = _head.Next;
            _counter--;
            _version++;

            if (_head == null)
            {
                _tails = null;
            }
            return;
        }

        // Caso 2: O elemento está no meio ou na cauda (_tails)
        Node<T>? curr = _head;

        while (curr != null && curr.Next != null)
        {
            if (EqualityComparer<T>.Default.Equals(curr.Next._element, obj))
            {
                // Se o nó a remover for a cauda (_tails)
                if (curr.Next == _tails)
                {
                    _tails = curr;
                }

                // Desconecta o nó
                curr.Next = curr.Next.Next;

                _counter--;
                _version++;
                return;
            }

            curr = curr.Next;
        }
    }

    public void removeFirst()
    {
        if (_counter == 0)
        {
            throw new ArgumentOutOfRangeException("Lista Ligada vazia");
        }

        if (_head == _tails)
        {
            _head = null;
            _tails = null;
        }
        else
        {
            _head = _head.Next;
        }

        _counter--;
        _version++;
    }

    public void removeLast()
    {
        if (_counter == 0 || _head == null)
        {
            throw new ArgumentOutOfRangeException("Lista Ligada vazia");
        }

        if (_head == _tails)
        {
            _head = null;
            _tails = null;
        }
        else
        {
            Node<T> curr = _head;
            while (curr.Next != null)
            {
                if (curr.Next == _tails)
                {
                    _tails = curr;
                    _tails.Next = null;
                    break;
                }

                curr = curr.Next;
            }
        }

        _counter--;
        _version++;
    }

    public int size()
    {
        return _counter;
    }

    T? ILinkedList<T>.first()
    {
        return _head._element;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator<T> ILinkedList<T>.GetReverseEnumerator()
    {
        throw new NotImplementedException();
    }

    T ILinkedList<T>.last()
    {
        return _tails._element;
    }

    private class Node<U>
    {
        public U _element { get; }

        public Node<U>? Next { get; set; }

        public Node(U element)
        {
            _element = element;
            Next = null;
        }

    }
}
