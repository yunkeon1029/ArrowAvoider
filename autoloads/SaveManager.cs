using Godot;
using Godot.Collections;

internal partial class SaveManager : Node, ISingleton
{
    public const string SAVE_FILE_PATH = "user://SaveFile.save";

    public Dictionary<string, Variant> SaveData { get; private set; }

    public SaveManager()
    {
        TreeEntered += () => Singletons.AddInstance(this);
        TreeExited += () => Singletons.RemoveInstance(this);
    }

    public override void _EnterTree()
    {
        if (!FileAccess.FileExists(SAVE_FILE_PATH))
            FileAccess.Open(SAVE_FILE_PATH, FileAccess.ModeFlags.Write);

		string jsonString = FileAccess.Open(SAVE_FILE_PATH, FileAccess.ModeFlags.Read)
									  .GetAsText();

        if (jsonString == "" || jsonString == null)
            jsonString = "{ }";

        SaveData = Json.ParseString(jsonString)
					   .AsGodotDictionary<string, Variant>();
    }

    public override void _ExitTree()
    {
		using var saveFile = FileAccess.Open(SAVE_FILE_PATH, FileAccess.ModeFlags.Write);
		string jsonString =  Json.Stringify(SaveData, "\t");

		saveFile.StoreLine(jsonString);
    }

    public void SetData(string name, Variant? value)
    {
        if (value == null)
            SaveData.Remove(name);

        if (value != null)
            SaveData[name] = (Variant)value;
    }

    public Variant? GetData(string name)
    {
        if (!SaveData.ContainsKey(name))
            return null;

        return SaveData[name];
    }
}