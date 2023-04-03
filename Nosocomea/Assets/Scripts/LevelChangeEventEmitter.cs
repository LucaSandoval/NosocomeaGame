// Represents the event emitter interface
public interface LevelChangeEventEmitter
{
  // Called when the level changes, emits the event
  void OnLevelChange(int level);

  // Register a listener
  void RegisterListener(LevelChangeEventListener listener);

  // Unregister a listener
  void UnregisterListener(LevelChangeEventListener listener);
}
