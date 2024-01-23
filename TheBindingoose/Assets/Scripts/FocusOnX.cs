using UnityEngine;

public class FocusOnX : MonoBehaviour
{
    [SerializeField] private GameObject _objectX;

    private void Update()
    {
        Vector3 newPos = new Vector3(_objectX.transform.position.x, _objectX.transform.position.y, -10f);
        transform.position = newPos;
    }
}
