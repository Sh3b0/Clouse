using System;
using UnityEngine;

public class WaterRise : MonoBehaviour {

    public float MinLocalHeight, MaxLocalHeight, WaterLayerHeight;
    public float MinIncreaseThreshold, MaxDecreaseThreshold;
    public float OneSideIncrease, OneSideShift;
    public float DecreaseMultiplier;
    public float BaseArchimedesForce, ForceMultiplier;
    public float TimeOfIceMelt;
    public WaterPart WaterLayerExample;
    public IcePart IceLayerExample;
    public int WaterDropsForIncrease, IterationsForDecrease = 10;
    public int TopLayerPassesBeforeBlock;
    public Transform WaterLayersSpawnOrigin;
    public Color DarkestLayerColor, BrightestLayerColor;
    
    public class SerializedWaterLayer {
        public Vector3 LocalPos, LocalScale;
        public SerializedWaterLayer(Vector3 pos, Vector3 scale) {
            LocalPos = pos;
            LocalScale = scale;
        }
    }
    
    private WaterPart[] _waterLayers;
    private IcePart _iceLayer;
    private int _numOfLayers, _waterBank, _decCounter;
    private bool _isFrozen;
    private float _meltTimer;

    private void Start() {
        _waterBank = 0;
        _decCounter = 0;
        _isFrozen = false;
        _waterLayers = new WaterPart[(int)((MaxLocalHeight - MinLocalHeight) / WaterLayerHeight)];

        // Create first water layer
        _waterLayers[0] = Instantiate(WaterLayerExample, WaterLayersSpawnOrigin);
        _waterLayers[0].ArchimedesForce = BaseArchimedesForce;
        _waterLayers[0].SetColor(CalculateLayerColor(0));
        _waterLayers[0].ParentWater = this;
        _waterLayers[0].IsTopLayer = true;
        _numOfLayers = 1;
    }

    private void Update() {
        if (!_isFrozen) return;
        _meltTimer += Time.deltaTime;
        if (_meltTimer >= TimeOfIceMelt)
            UnfreezeLake();
    }

    // Call this method to increase water level for a bit
    public void IncreaseWaterlevel() {
        _waterBank++;
        if (_waterBank < WaterDropsForIncrease || _numOfLayers == _waterLayers.Length || _isFrozen) return;
        
        // Create next water layer
        _waterLayers[_numOfLayers] = Instantiate(WaterLayerExample, WaterLayersSpawnOrigin);
        _waterLayers[_numOfLayers].transform.localPosition += new Vector3(0, WaterLayerHeight * _numOfLayers, 0);
        _waterLayers[_numOfLayers].ArchimedesForce = BaseArchimedesForce * (float)Math.Pow(ForceMultiplier, _numOfLayers);
        _waterLayers[_numOfLayers].SetColor(CalculateLayerColor(_numOfLayers));
        _waterLayers[_numOfLayers].ParentWater = this;
        _waterLayers[_numOfLayers].IsTopLayer = true;
        
        _waterLayers[_numOfLayers - 1].IsTopLayer = false;
        _waterLayers[_numOfLayers - 1].DisableTopLayer();
        
        _numOfLayers++;
        _waterBank = 0;
    }

    // This method will be called if there are no colliders at both sides of the water gap
    public void DecreaseWaterLevel() {
        if (_numOfLayers == 1 || _isFrozen) return;

        _decCounter++;
        if (_decCounter < IterationsForDecrease) {
            return;
        }
        _decCounter = 0;

        _waterLayers[_numOfLayers - 1].IsTopLayer = false;
        _waterLayers[_numOfLayers - 1].DisableTopLayer();
        
        // Delete one water layer
        Destroy(_waterLayers[_numOfLayers - 1].gameObject);
        _numOfLayers--;

        _waterLayers[_numOfLayers - 1].IsTopLayer = true;
    }

    // Save all non-null layers of water object as their scale
    public SerializedWaterLayer[] SerializeWaterLayers() {
        var layersScales = new SerializedWaterLayer[_waterLayers.Length];

        for (var i = 0; i < _waterLayers.Length; i++) {
            if (!_waterLayers[i]) break;
            layersScales[i] = new SerializedWaterLayer(_waterLayers[i].transform.localPosition,
                _waterLayers[i].transform.localScale);
        }

        return layersScales;
    }

    public void RecoverWaterFast(SerializedWaterLayer[] layersSaves) {
        // Threat first one separately, as it is already on scene
        _waterLayers[0].transform.localPosition = layersSaves[0].LocalPos;
        _waterLayers[0].transform.localScale = layersSaves[0].LocalScale;
        
        // If no more layers were saved, return
        if (layersSaves.Length < 2 || layersSaves[1] == null) return;
        
        // Otherwise, first layer is not the top one
        _waterLayers[0].IsTopLayer = false;
        _waterLayers[0].DisableTopLayer();

        // For each other save create layer and make it of saved size
        for (var i = 1; i <= layersSaves.Length; i++) {
            // When reached last saved layer, mark it as top and break loop
            if (i == layersSaves.Length || layersSaves[i] == null) {
                _waterLayers[i - 1].IsTopLayer = true;
                _numOfLayers = i;
                _waterBank = 0;
                break;
            }
            
            // Create next water layer
            _waterLayers[i] = Instantiate(WaterLayerExample, WaterLayersSpawnOrigin);
            _waterLayers[i].transform.localPosition = layersSaves[i].LocalPos;
            _waterLayers[i].transform.localScale = layersSaves[i].LocalScale;
            _waterLayers[i].ArchimedesForce = BaseArchimedesForce * (float)Math.Pow(ForceMultiplier, _numOfLayers);
            _waterLayers[i].SetColor(CalculateLayerColor(i));
            _waterLayers[i].ParentWater = this;
        }
    }

    // Make top layer frozen (collider), forbid any increasing / decreasing of layers
    public void FreezeLake() {
        if (_isFrozen) return;
        
        _meltTimer = 0.0f;
        _isFrozen = true;
        
        // Create ice layer and set appropriate size
        _iceLayer = Instantiate(IceLayerExample, WaterLayersSpawnOrigin);
        _iceLayer.transform.localPosition += new Vector3(0, WaterLayerHeight * _numOfLayers, 0);
        _iceLayer.SetLayerSize(_waterLayers[_numOfLayers - 1].transform);
        
    }

    // Return to normal lake state
    private void UnfreezeLake() {
        _isFrozen = false;
        Destroy(_iceLayer.gameObject);
    }

    private Color CalculateLayerColor(int layerNum) {
        var totalLayers = _waterLayers.Length;
        var fraction = (float) layerNum / totalLayers;
        
        var r = DarkestLayerColor.r + (BrightestLayerColor.r - DarkestLayerColor.r) * fraction;
        var g = DarkestLayerColor.g + (BrightestLayerColor.g - DarkestLayerColor.g) * fraction;
        var b = DarkestLayerColor.b + (BrightestLayerColor.b - DarkestLayerColor.b) * fraction;
        var a = DarkestLayerColor.a + (BrightestLayerColor.a - DarkestLayerColor.a) * fraction;
        
        return new Color(r, g, b, a);
    }

}
