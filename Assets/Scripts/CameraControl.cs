using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform    target;
    public Vector3      offset;
    public float        cameraSpeed = 5.0f;
    public bool         enforceBounds = false;
    public Rect         bounds;

    Camera camera;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        if (target == null) return;
        
        Vector3 newPos = target.position + offset;

        Vector3 delta = newPos - transform.position;

        newPos = transform.position + delta * cameraSpeed * Time.fixedDeltaTime;

        if (enforceBounds)
        {
            float sizeY = camera.orthographicSize;
            float sizeX = sizeY * camera.aspect;

            newPos.x = Mathf.Clamp(newPos.x, bounds.xMin + sizeX, bounds.xMax - sizeX);
            newPos.y = Mathf.Clamp(newPos.y, bounds.yMin + sizeY, bounds.yMax - sizeY);
        }

        newPos.z = transform.position.z;

        transform.position = newPos;
    }

    private void OnDrawGizmos()
    {
        if (enforceBounds)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(bounds.center, new Vector3(bounds.width, bounds.height, 0.0f));
        }
    }
}
