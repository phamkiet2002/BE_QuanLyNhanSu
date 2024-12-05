namespace QuanLyNhanSu.Domain.Exceptions;
public static class PositionException
{
    public class PositionNotFoundException : Exception
    {
        public PositionNotFoundException(Guid id) : base($"Position with id {id} is not found")
        {
        }
    }
}
