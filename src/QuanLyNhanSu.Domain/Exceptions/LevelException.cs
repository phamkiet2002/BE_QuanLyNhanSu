namespace QuanLyNhanSu.Domain.Exceptions;
public static class LevelException
{
    public class LevelNotFoundException : Exception
    {
        public LevelNotFoundException(Guid id)
            : base($"Level with id {id} is not found") { }
    }
}
