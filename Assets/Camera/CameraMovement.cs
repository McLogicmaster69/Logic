using UnityEngine;

namespace Logic.cam
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _shiftMultiplier = 2f;
        [SerializeField] private int _zoom = 5;
        [SerializeField] private int _maxZoom = 20;
        private Camera _camera;

        private readonly Vector3 RIGHT_VECTOR = new Vector3(1f, 0f, -1f);
        private readonly Vector3 FOWARD_VECTOR = new Vector3(1f, 0f, 1f);

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            transform.position += (Input.GetKey(KeyCode.LeftShift) ? _shiftMultiplier : 1f) * _speed * Time.deltaTime * _zoom * ((x * RIGHT_VECTOR) + (z * FOWARD_VECTOR));

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (_zoom > 1)
                    _zoom--;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (_zoom < _maxZoom)
                    _zoom++;
            }

            _camera.orthographicSize = _zoom;
        }
    }
}