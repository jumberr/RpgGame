using _Project.CodeBase.Data;

namespace _Project.CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}