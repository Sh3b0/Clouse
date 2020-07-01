using UnityEngine;

public class Generator : MonoBehaviour
{
    // This is what generator activates
    public Mechanism ConnectedMechanism;
    public SpriteRenderer Icon;

    public void Work() // Do the intended function of the generator. 
    {
        if (Icon.color != Color.green) // Activate once.
        {
            Icon.color = Color.green;
            if (ConnectedMechanism) ConnectedMechanism.Work();
        }
    }
}