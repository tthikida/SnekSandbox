using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float speed = 1f;
    Camera _mainCamera;
    Vector3 _mouseInput;


    private void Initialize()
    {
        _mainCamera = Camera.main;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Initialize();
    }

    void Update()
    {
        if ( !IsOwner || !Application.isFocused) return; 
        _mouseInput.x = Input.mousePosition.x;
        _mouseInput.y = Input.mousePosition.y;
        _mouseInput.z = _mainCamera.nearClipPlane;
        Vector2 screenMousePos = Input.mousePosition;
        Vector3 worldMousePos = _mainCamera.ScreenToWorldPoint(screenMousePos);
        Debug.Log($"mouseWorldPos: {worldMousePos}");
        transform.position = Vector3.MoveTowards(transform.position, worldMousePos, Time.deltaTime * speed);
        if(worldMousePos != transform.position )
        {
            Vector3 targetDirection = worldMousePos - transform.position;
            transform.up = targetDirection;
        }
    }
}
