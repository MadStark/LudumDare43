using System.Collections;

public abstract class GameplaySequence : IEnumerator
{
    private IEnumerator _enumerator;
    private IEnumerator Enumerator {
        get {
            if (_enumerator == null)
                _enumerator = Run();
            return _enumerator;
        }
    }

    public object Current => Enumerator.Current;

    public bool MoveNext()
    {
        return Enumerator.MoveNext();
    }

    public void Reset()
    {
        Enumerator.Reset();
    }

    protected abstract IEnumerator Run();
}
