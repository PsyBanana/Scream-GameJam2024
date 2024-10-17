using StimpakEssentials;

public class Player3D : SingletonBehaviour<Player3D>
{
    public void TogglePlayer(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
