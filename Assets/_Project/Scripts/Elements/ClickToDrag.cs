using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToDrag : MonoBehaviour
{
    public LayerMask clickMask;

    private GameObject _selectedGameObject;

    public Material unselectedMat;
    public Material selectedMat;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo, 50, clickMask))
            {
                _selectedGameObject = hitInfo.collider.gameObject;

                var targetPos = _selectedGameObject.transform.position;
                targetPos.y = 1;
                _selectedGameObject.transform.position = targetPos;
                _selectedGameObject.GetComponent<MeshRenderer>().material = selectedMat;
            }
        }        
    }
}
