using System;
using UnityEngine;

public class WaterRise : MonoBehaviour {

    public float MinLocalHeight, MaxLocalHeight, WaterLayerHeight;
    public float MinIncreaseThreshold, MaxDecreaseThreshold;
    public float OneSideIncrease, OneSideShift;
    public float DecreaseMultiplier;
    public float BaseArchimedesForce, ForceMultiplier;
    public WaterPart WaterLayerExample;
    public int WaterDropsForIncrease;
    public Transform WaterLayersSpawnOrigin;
    
    private WaterPart[] _waterLayers;
    private int _numOfLayers, _waterBank;

    private void Start() {
        _waterBank = 0;
        _waterLayers = new WaterPart[(int)((MaxLocalHeight - MinLocalHeight) / WaterLayerHeight)];

        // Create first water layer
        _waterLayers[0] = Instantiate(WaterLayerExample, WaterLayersSpawnOrigin);
        _waterLayers[0].ArchimedesForce = BaseArchimedesForce;
        _waterLayers[0].ParentWater = this;
        _numOfLayers = 1;
    }

    // Call this method to increase water level for a bit
    public void IncreaseWaterlevel() {
        _waterBank++;
        if (_numOfLayers == _waterLayers.Length) return;
        if (_waterBank < WaterDropsForIncrease) return;
        
        // Create next water layer
        _waterLayers[_numOfLayers] = Instantiate(WaterLayerExample, WaterLayersSpawnOrigin);
        _waterLayers[_numOfLayers].transform.localPosition += new Vector3(0, WaterLayerHeight * _numOfLayers, 0);
        _waterLayers[_numOfLayers].ArchimedesForce = BaseArchimedesForce * (float)Math.Pow(ForceMultiplier, _numOfLayers);
        _waterLayers[_numOfLayers].ParentWater = this;
        _numOfLayers++;
        _waterBank = 0;
    }

    // This method will be called if there are no colliders at both sides of the water gap
    public void DecreaseWaterLevel() {
        if (_numOfLayers == 1) return;
        
        // Delete one water layer
        Destroy(_waterLayers[_numOfLayers - 1]);
        _numOfLayers--;
    }
    
}
