using UnityEngine;

public class ViewportCheckLimits : ICheckLimits
{
    private readonly Camera _camera;

    public ViewportCheckLimits(Camera camera)
    {
        _camera = camera;
    }
    
    public Vector2 ClampFinalPosition(Vector2 currentPosition)
    {
        Vector3 viewportPoint = _camera.WorldToViewportPoint(currentPosition);
        viewportPoint.x = Mathf.Clamp(viewportPoint.x, 0.03f, 0.97f);
        viewportPoint.y = Mathf.Clamp(viewportPoint.y, 0.03f, 0.97f);
        
        return _camera.ViewportToWorldPoint(viewportPoint);
    }
}
