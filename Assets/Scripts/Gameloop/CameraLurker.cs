using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraLurker : MonoBehaviour
{
    private Vector2 lurkingPoint
    {
        get
        {
            if (Character.singleton == null) return Vector2.zero;
            return Character.singleton.transform.position;
        }
    }
    [SerializeField] private float zPoisition;
    [SerializeField] private float inertia;
    private Camera cam;
    private Vector2 velocityChange;
    [SerializeField] private Transform fitInRect;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private Vector2 GetCameraWidthHeight()
    {
        Vector3 topLeftCorner = cam.ViewportToWorldPoint(new Vector3(0, 0));
        Vector3 downRightCorner = cam.ViewportToWorldPoint(new Vector3(1, 1));
        Vector2 result = downRightCorner - topLeftCorner;
        return result;
    }
    private Vector2 FitCameraPos(Vector2 position)
    {
        Vector2 cameraSize = GetCameraWidthHeight() / 2;
        Vector2 cameraPos = position;
        Vector2 rectPos = fitInRect.position;
        Vector2 rectSize = fitInRect.transform.localScale / 2;
        if (cameraPos.x + cameraSize.x > rectPos.x + rectSize.x)
        {
            cameraPos.x = rectPos.x + rectSize.x - cameraSize.x;
        }
        if (cameraPos.x - cameraSize.x < rectPos.x - rectSize.x)
        {
            cameraPos.x = rectPos.x - rectSize.x + cameraSize.x;
        }
        if (cameraPos.y + cameraSize.y > rectPos.y + rectSize.y)
        {
            cameraPos.y = rectPos.y + rectSize.y - cameraSize.y;
        }
        if (cameraPos.y - cameraSize.y < rectPos.y - rectSize.y)
        {
            cameraPos.y = rectPos.y - rectSize.y + cameraSize.y;
        }
        return cameraPos;
    }
    private bool IsAngled(Vector2 left, Vector2 right, bool A, bool B)
    {
        return (left.x < right.x) == A && (left.y < right.y) == B;
    }
    private void Update()
    {

        Vector2 currPos = transform.position;
        Vector2 destPos = FitCameraPos(lurkingPoint);
        Vector2 newPos = Vector2.SmoothDamp(currPos, destPos, ref velocityChange, Time.deltaTime * inertia);
        transform.position = new Vector3(newPos.x, newPos.y, zPoisition);
    }
}
