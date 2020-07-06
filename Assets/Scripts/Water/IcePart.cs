using UnityEngine;
using UnityUtilities;

public class IcePart : MonoBehaviour {

    public void SetLayerSize(Transform origin) {
        var newTransform = transform;
        newTransform.localScale = origin.localScale;

        var (_, y, z) = newTransform.position;
        newTransform.position = new Vector3(origin.position.x, y, z);
    }
    
}
