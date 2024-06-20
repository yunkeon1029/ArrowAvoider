using Godot;

internal partial class MasterVolumeModifier : Node
{
	[Export]
	public float MasterVolume
	{
		get => GetMasterVolume();
		set => SetMasterVolume(value);
	}

	public void SetMasterVolume(float volume)
	{
		int masterBusIndex = AudioServer.GetBusIndex(BusName.Master);
		float masterBusDb = Mathf.LinearToDb(volume);

		AudioServer.SetBusVolumeDb(masterBusIndex, masterBusDb);
	}

	private float GetMasterVolume()
	{
		int masterBusIndex = AudioServer.GetBusIndex(BusName.Master);
		float masterBusDb = AudioServer.GetBusVolumeDb(masterBusIndex);

		return Mathf.DbToLinear(masterBusDb);
	}
}
