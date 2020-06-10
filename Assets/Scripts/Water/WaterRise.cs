using UnityEngine;

public class WaterRise : MonoBehaviour {

    public Transform FullWaterObject;
    public float MinLocalHeight;
    public float MaxLocalHeight;
    public float IncreaseDelta;
    
    private void Start() {
        var oldPos = FullWaterObject.localPosition;
        FullWaterObject.localPosition = new Vector3(oldPos.x, MinLocalHeight, oldPos.z);
    }

    // Call this method to increase water level for a bit
    public void IncreaseWaterlevel() {
        var oldPos = FullWaterObject.localPosition;
        var newHeight = oldPos.y + IncreaseDelta;
        if (newHeight >= MaxLocalHeight) { return; }
        FullWaterObject.localPosition = new Vector3(oldPos.x, newHeight, oldPos.z);
    }
    
}
