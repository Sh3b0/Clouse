using UnityEngine;

public class WaterPart : MonoBehaviour {

    public SpriteRenderer[] WaterSides;
    public Transform LeftUpRaycast, LeftDownRaycast;
    public Transform RightUpRaycast, RightDownRaycast;
    public float ArchimedesForce;
    public WaterRise ParentWater;
    public ArchimedesForce ArchimedesScript;
    public bool IsTopLayer;

    private Transform _thisObject;

    private void Start() {
        _thisObject = transform;
    }
    
    // I would not recommend to touch anything here
    private void Update() {
        if (!ParentWater) return;
        const int layerMask = 1; // All defaults

        // Send raycasts from edges of water surface
        var left = Physics.Raycast(LeftUpRaycast.position, transform.TransformDirection(Vector3.left),
            out var leftUpHit, Mathf.Infinity, layerMask) & 
                   Physics.Raycast(LeftDownRaycast.position, transform.TransformDirection(Vector3.left),
            out var leftDownHit, Mathf.Infinity, layerMask);
        
        var right = Physics.Raycast(RightUpRaycast.position, transform.TransformDirection(Vector3.right),
            out var rightUpHit, Mathf.Infinity, layerMask) & 
                    Physics.Raycast(RightDownRaycast.position, transform.TransformDirection(Vector3.right),
            out var rightDownHit, Mathf.Infinity, layerMask);
        
        // Ignore infinite cases
        if (!left && !right) return;
        
        // Choose closest hit
        var leftHit = leftDownHit.distance < leftUpHit.distance ? leftDownHit : leftUpHit;
        var rightHit = rightDownHit.distance < rightUpHit.distance ? rightDownHit : rightUpHit;
        
        // If there is collider close enough on one side, decrease water size
        if (leftHit.distance <= ParentWater.MaxDecreaseThreshold) {
            DecreaseAtLeft();
        } else if (rightHit.distance <= ParentWater.MaxDecreaseThreshold) {
            DecreaseAtRight();
        }
        
        // Increase water width if no colliders close to surface left or right
        if (leftHit.distance >= ParentWater.MinIncreaseThreshold && rightHit.distance >= ParentWater.MinIncreaseThreshold) {
            IncreaseToBothSides();
        } else if (leftHit.distance >= ParentWater.MinIncreaseThreshold) {
            IncreaseToLeft();
        } else if (rightHit.distance >= ParentWater.MinIncreaseThreshold) {
            IncreaseToRight();
        } else if (leftHit.collider.gameObject.CompareTag(Constants.TAG_DRAIN) ||
                   rightHit.collider.gameObject.CompareTag(Constants.TAG_DRAIN)) {
            ParentWater.DecreaseWaterLevel(); // Decrease water height if Drain was found
        }
    }

    private void IncreaseToBothSides() {
        _thisObject.localScale += new Vector3(ParentWater.OneSideIncrease, 0, 0) * (2 * Time.deltaTime);
    }

    private void IncreaseToLeft() {
        _thisObject.localPosition -= new Vector3(ParentWater.OneSideShift, 0, 0) * Time.deltaTime;
    }

    private void IncreaseToRight() {
        _thisObject.localPosition += new Vector3(ParentWater.OneSideShift, 0, 0) * Time.deltaTime;
    }
    
    private void DecreaseAtLeft() {
        _thisObject.localScale -= new Vector3(ParentWater.OneSideIncrease, 0, 0) * (ParentWater.DecreaseMultiplier * Time.deltaTime);
        _thisObject.localPosition += new Vector3(ParentWater.OneSideShift, 0, 0) * (ParentWater.DecreaseMultiplier * Time.deltaTime);
    }

    private void DecreaseAtRight() {
        _thisObject.localScale -= new Vector3(ParentWater.OneSideIncrease, 0, 0) * (ParentWater.DecreaseMultiplier * Time.deltaTime);
        _thisObject.localPosition -= new Vector3(ParentWater.OneSideShift, 0, 0) * (ParentWater.DecreaseMultiplier * Time.deltaTime);
    }

    public void DisableTopLayer() {
        ArchimedesScript.DisableBlocks();
    }

    public void SetColor(Color newColor) {
        foreach (var waterSide in WaterSides) {
            waterSide.color = newColor;
        }
    }
    
}
