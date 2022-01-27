using _Project.CodeBase.Data;

namespace _Project.CodeBase.Infrastructure.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}