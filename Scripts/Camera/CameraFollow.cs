using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //GameObjects / others
    [SerializeField]
    private Transform _target;

    //varibles
    [SerializeField]
    private float _smoothSpeed = 0.125f;

    //Vectors
    [SerializeField]
    private Vector3 _offset;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        //smoothing the camera
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, _smoothSpeed);
        transform.position = smoothedPosition;

        //This is for the 3D effect
        transform.LookAt(_target);
    }
}
