using UnityEngine;

public class AutoDoor : Mechanism {

    public Animation DoorAnimation;

    public override void Work() {
        DoorAnimation.Play();
    }
    
}
