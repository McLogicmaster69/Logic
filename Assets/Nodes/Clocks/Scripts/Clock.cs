using UnityEngine;

namespace Logic.Nodes
{
    public class Clock : LogicComponent
    {
        [SerializeField] private Sprite[] _clockFrames;
        [SerializeField] private float _clockSpeed;
        [SerializeField] private bool _active;

        private SpriteRenderer _renderer;
        private float _timer = 0f;
        private int _frame = 0;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public float ClockSpeed => _clockSpeed;
        public bool Active => _active;

        public void SetClockSpeed(float speed)
        {
            _clockSpeed = speed;
        }

        public void SetActive(bool active)
        {
            _active = active;
        }

        protected override void Tick()
        {
            base.Tick();
            Output[0] = _active && (_frame % 2 == 0);

            if (_active)
                RunClock();
        }

        private void RunClock()
        {
            _timer += Time.deltaTime;
            if (_timer >= _clockSpeed)
            {
                _timer = 0f;
                _frame = (_frame + 1) % 8;
            }

            _renderer.sprite = _clockFrames[_frame];
        }
    }
}