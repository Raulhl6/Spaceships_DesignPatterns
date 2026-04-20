using UnityEngine;

public class ViewportCheckLimits : ICheckLimits
{
    private readonly Transform _transform;
    private readonly Camera _camera;

    public ViewportCheckLimits(Transform transform, Camera camera)
    {
        _transform = transform;
        _camera = camera;
    }
    
    public void ClampFinalPosition()
    {
        Vector3 viewportPoint = _camera.WorldToViewportPoint(_transform.position);
        viewportPoint.x = Mathf.Clamp(viewportPoint.x, 0.03f, 0.97f);
        viewportPoint.y = Mathf.Clamp(viewportPoint.y, 0.03f, 0.97f);
        
        _transform.position = _camera.ViewportToWorldPoint(viewportPoint);
    }
}
