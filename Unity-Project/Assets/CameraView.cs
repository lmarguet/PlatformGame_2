using UnityEngine;

public class CameraView : MonoBehaviour
{
    private Transform _followTransform;
    private float _offsetZ;
    private Vector3 _initPosition;

    private bool CanFollow { get; set; }

    private void Start()
    {
        _initPosition = transform.position;
        OffsetX = 0;
    }

    public void StartFollowing(Transform followTransform)
    {
        _followTransform = followTransform;

        transform.position = _initPosition;
        _offsetZ = transform.position.z - followTransform.position.z;

        CanFollow = true;
    }

    private void LateUpdate()
    {
        if (CanFollow)
        {
            var xPos = Mathf.Lerp(OffsetX, 0, 0.00001f);
            transform.position = new Vector3(xPos, 0, _followTransform.position.z + _offsetZ);
        }
    }

    public float OffsetX { get; set; }
}