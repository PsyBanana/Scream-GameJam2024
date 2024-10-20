public interface ISaveable
{
    object CaptureObjectState();
    void RestoreState(object state);
}
