namespace ShapeLib
{
    public readonly struct Result<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        private Result(T value)
        {
            _value = value;
            _hasValue = true;
        }

        public bool IsSome()
        {
            return _hasValue;
        }

        public bool IsNone()
        {
            return !_hasValue;
        }

        public T Get()
        {
            return _value;
        }

        public static implicit operator Result<T>(T value)
        {
            return value == null ? new Result<T>() : new Result<T>(value);
        }
    }
}