using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CameraManager.Instance.ChangeCamera(CameraManager.Focus.Top);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            CameraManager.Instance.ChangeCamera(CameraManager.Focus.Map);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            CameraManager.Instance.ChangeCamera(CameraManager.Focus.TargetGroup);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TryToPutTower(Tower.Type.Damage);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            TryToPutTower(Tower.Type.Range);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            TryToPutTower(Tower.Type.Slow);
        }
    }

    private void TryToPutTower(Tower.Type type)
    {
        Ray ray = CameraManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
            
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            var tile = objectHit.GetComponent<Tile>();
            if(tile)
                GameManager.Instance.PutTower(tile, type);
        }
    }
}
