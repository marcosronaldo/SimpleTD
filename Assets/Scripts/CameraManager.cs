using System;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera targetGroupCamera;
    [SerializeField]
    private CinemachineTargetGroup targetGroup;
    [SerializeField]
    private CinemachineVirtualCamera mapCamera;
    [SerializeField]
    private CinemachineVirtualCamera topCamera;

    public Camera mainCamera => Camera.main;

    public void ClearTargetGroupCamera()
    {
        foreach (var t in targetGroup.m_Targets)
        {
            targetGroup.RemoveMember(t.target);    
        }
    }

    public static CameraManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum Focus
    {
        Map,
        Top,
        TargetGroup,
    }

    public void ChangeCamera(Focus focus)
    {
        switch (focus)
        {
            case Focus.Map:
                targetGroupCamera.gameObject.SetActive(false);
                mapCamera.gameObject.SetActive(true);
                topCamera.gameObject.SetActive(false);
                break;
            case Focus.Top:
                targetGroupCamera.gameObject.SetActive(false);
                mapCamera.gameObject.SetActive(false);
                topCamera.gameObject.SetActive(true);
                break;
            case Focus.TargetGroup:
                targetGroupCamera.gameObject.SetActive(true);
                mapCamera.gameObject.SetActive(false);
                topCamera.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(focus), focus, null);
        }
    }
}
