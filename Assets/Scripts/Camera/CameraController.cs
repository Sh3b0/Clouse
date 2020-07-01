using UnityEngine;

public class CameraController : MonoBehaviour {

    public static bool cloudVisible, cameraActive, fixY;
    public static Camera me;
    public static Animation anim;

    private Transform _target;
    private Vector3 _offset;
    
    // Suitable y position for every level, that shows the cloud (and player if not underground)
    private readonly int[] levelY = {0, 5, 5, 5, 7, 7, 40, 5};
    
    private void Start()
    {
        cloudVisible = cameraActive = fixY = true;
        me = Camera.main;
        _target = Player.me.transform;
        _offset = new Vector3(0, 0, transform.position.z);
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        //print(LevelsManager.CurrentLevel);
        if (!cameraActive) return;
        
        // Follow active character
        if (Player.playerActive) _target = Player.me.transform;
        else if (Player.cloudActive) _target = Cloud.me;
        else _target = Player.me.transform;
        
        // Detect cloud visibility (Used in cloud script)
        Vector3 pos = me.WorldToViewportPoint(Cloud.me.position);
        cloudVisible = pos.x >= 0 && pos.x <= 1 && pos.y >= 0 && pos.y <= 1 && pos.z > 0;

        // Peek animation
        if (Player.playerActive && !anim.isPlaying) 
        {
            if (Input.GetAxis("Vertical") < 0)
                anim.Play("PeekDown");
            
            else if (Input.GetAxis("Vertical") > 0)
                anim.Play("PeekUp");
        }
    }
    
    private void LateUpdate() 
    {
        if (!cameraActive) return;
        
        // Follow target smoothly
        Vector3 camPos = transform.position, dest = _target.position + _offset;
        if (fixY) dest.y = levelY[LevelsManager.CurrentLevel];
        transform.position = Vector3.Lerp(camPos, dest, 0.01f);
    }
}