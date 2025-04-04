namespace Golf
{
    public interface ILevelSource
    {
        void LoadScene(string sceneName);
        void TriggerSpawnTransition();
    }
}
