
namespace Application.Interfaces
{
    public interface IBackgroundJobService
    {
        void HandleExpiredBook();
    }
}