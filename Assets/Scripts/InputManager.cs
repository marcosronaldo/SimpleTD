using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            CameraManager.Instance.ChangeCamera(CameraManager.Focus.Top);
        else if (Input.GetKeyDown(KeyCode.M))
            CameraManager.Instance.ChangeCamera(CameraManager.Focus.Map);
        else if (Input.GetKeyDown(KeyCode.F)) CameraManager.Instance.ChangeCamera(CameraManager.Focus.TargetGroup);
        if (Input.GetKeyDown(KeyCode.F1)) TryToPutTower(Tower.Type.Damage);
        if (Input.GetKeyDown(KeyCode.F2)) TryToPutTower(Tower.Type.Range);
        if (Input.GetKeyDown(KeyCode.F3)) TryToPutTower(Tower.Type.Slow);
    }

    private void TryToPutTower(Tower.Type type)
    {
        var ray = CameraManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            var objectHit = hit.transform;
            var tile = objectHit.GetComponent<Tile>();
            if (tile && tile.IsEmpty)
                GameManager.Instance.PutTower(tile, type);
        }
    }
}