namespace Backend.Domain.Common
{
    public abstract class JoinEntity<TLeft, TRight>
    where TLeft : Entity
    where TRight : Entity
    {
        public required TLeft Left { get; init; }
        public required TRight Right { get; init; }
    }
}
