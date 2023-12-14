using UnityEngine;

public class FocusOnX : MonoBehaviour
{
    [SerializeField] private Transform _objectX;

    private void Update()
    {
        Vector3 newPos = new Vector3(_objectX.position.x, _objectX.position.y, -10f);
        transform.position = newPos;
    }
}
