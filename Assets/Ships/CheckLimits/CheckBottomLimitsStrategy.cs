using UnityEngine;

public class CheckBottomLimitsStrategy : ICheckDestroyLimits
{
    private Camera _camera;
    
    public CheckBottomLimitsStrategy(Camera camera)
    {
        this._camera = camera;
    }
    
    public bool IsInsideTheLimits(Vector3 position)
    {
        var viewportPoint = _camera.WorldToViewportPoint(position);
        return viewportPoint.y > 0;
    }
}
