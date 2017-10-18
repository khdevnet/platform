namespace Plugin.Core.Extensibility
{
    public interface IAppSettingsProvider
    {
        string GetValue(string key);

        bool ContainsKey(string key);
    }
}