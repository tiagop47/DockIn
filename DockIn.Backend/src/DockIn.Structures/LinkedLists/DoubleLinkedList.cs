using System.Collections;

namespace DockIn.Structures;

public class DoubleLinkedList<T> : ILinkedList<T>
{
    public int Counter;
    private int _version;
    private Node<T>? _head;
    private Node<T>? _tail;

    public DoubleLinkedList()
    {
        _head = default;
        _tail = default;
        Counter = 0;
        _version = 0;
    }
    public void addFirst(T add)
    {
        if (add == null)
        {
            throw new ArgumentNullException("O elemento é null");
        }

        Node<T> tmp = new Node<T>(add);

        if (Counter == 0)
        {
            _head = tmp;
            _tail = _head;
        }
        else
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _head.Prev = tmp;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            tmp.Next = _head;
            _head = tmp;
        }

        Counter++;
        _version++;
    }

    public void addLast(T add)
    {
        if (add == null)
        {
            throw new ArgumentNullException("O elemento é null");
        }

        Node<T> tmp = new Node<T>(add);

        if (Counter == 0)
        {
            _head = tmp;
            _tail = _head;
        }
        else
        {
            _tail.Next = tmp;
            tmp.Prev = _tail;
            _tail = tmp;
        }

        Counter++;
        _version++;
    }

    public T? first()
    {
        if (Counter == 0)
            throw new ArgumentOutOfRangeException("A lista está vazia");

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        if (_head.Element != null)
        {
            return _head.Element;
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return default;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new ForwardEnumaretor(this);
    }

    public bool isEmpty()
    {
        return Counter == 0;
    }

    public T last()
    {
        if (_tail is null)
            throw new ArgumentOutOfRangeException("A lista está vazia");

        return _tail.Element!;
    }

    public void remove(T obj)
    {
        if (Counter == 0)
        {
            throw new ArgumentOutOfRangeException("A lista está vazia.");
        }

        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj), "O elemento a remover não pode ser null.");
        }

        var comparer = EqualityComparer<T>.Default;

        if (_head != null && comparer.Equals(_head.Element, obj))
        {
            removeFirst();
            return;
        }

        if (_tail != null && comparer.Equals(_tail.Element, obj))
        {
            removeLast();
            return;
        }

        Node<T>? curr = _head;

        while (curr != null)
        {
            if (comparer.Equals(curr.Element, obj))
            {
                if (curr.Prev != null)
                {
                    curr.Prev.Next = curr.Next;
                }

                if (curr.Next != null)
                {
                    curr.Next.Prev = curr.Prev;
                }

                Counter--;
                _version++;
                return;
            }

            curr = curr.Next;
        }
    }

    public void removeFirst()
    {
        if (Counter == 0)
        {
            throw new ArgumentOutOfRangeException("");
        }

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            _head = _head.Next;
            _head.Prev = default;
        }

        Counter--;
        _version++;
    }

    public void removeLast()
    {
        if (Counter == 0)
        {
            throw new ArgumentOutOfRangeException("");
        }

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            _tail = _tail.Prev;
            _tail.Next = default;
        }

        Counter--;
        _version++;
    }


    public int size()
    {
        return Counter;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetReverseEnumerator()
    {
        return new BackwardsEnumerator(this);
    }

    private class BackwardsEnumerator : IEnumerator<T>
    {
        private readonly DoubleLinkedList<T> _list;
        private readonly int _expectedVersion;
        private Node<T>? _current;
        private bool _started;

        public BackwardsEnumerator(DoubleLinkedList<T> list)
        {
            _list = list;
            _current = list._tail;
            _expectedVersion = list._version;
            _started = false;
        }

        private void CheckVersion()
        {
            if (_expectedVersion != _list._version)
            {
                throw new InvalidOperationException("A coleção foi modificada durante a iteração.");
            }
        }

        public T Current
        {
            get
            {
                CheckVersion();
                if (_current == null || _current.Element == null)
                {
                    throw new InvalidOperationException("Enumerador inválido.");
                }

                return _current.Element;
            }
        }

        object IEnumerator.Current => Current!;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            CheckVersion();

            if (!_started)
            {
                _started = true;
            }
            else
            {
                _current = _current?.Prev;
            }

            return _current != null;
        }

        public void Reset()
        {
            CheckVersion();
            _current = _list._tail;
            _started = false;
        }
    }

    private class ForwardEnumaretor : IEnumerator<T>
    {
        private readonly DoubleLinkedList<T> _list;
        private readonly int _expectedVersion;
        private Node<T>? _current;
        private bool _started;

        public ForwardEnumaretor(DoubleLinkedList<T> list)
        {
            _list = list;
            _current = list._head;
            _expectedVersion = list._version;
            _started = false;
        }

        private void CheckVersion()
        {
            if (_expectedVersion != _list._version)
            {
                throw new InvalidOperationException("A coleção foi modificada durante a iteração.");
            }
        }

        public T Current
        {
            get
            {
                CheckVersion();
                if (_current == null || _current.Element == null)
                {
                    throw new InvalidOperationException("Enumerador inválido.");
                }

                return _current.Element;
            }
        }

        object IEnumerator.Current => Current!;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            CheckVersion();

            if (!_started)
            {
                _started = true;
            }
            else
            {
                _current = _current?.Next;
            }

            return _current != null;
        }

        public void Reset()
        {
            CheckVersion();
            _current = _list._head;
            _started = false;
        }
    }

    private class Node<U>
    {
        public U? Element { get; }

        public Node<U>? Next { get; set; }

        public Node<U>? Prev { get; set; }

        public Node(U element)
        {
            Element = element;
            Next = null;
        }
    }
}
