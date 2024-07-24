using System;
using System.Collections;
using UnityEngine;

namespace IdleHeaven
{
    public enum Directions
    {
        up, down, left, right
    }
    public class Direction
    {
        public static Vector3 GetDirection(Directions direction) => direction switch
        {
            Directions.left => Vector3.left,
            Directions.right => Vector3.right,
            Directions.up => Vector3.up,
            Directions.down => Vector3.down,
            _ => Vector3.zero,
        };
    }

    public class UiDrawerEffect : MonoBehaviour
    {
        Vector3 _targetPosition;
        Vector3 _hidePosition;
        bool _isShown = false;

        RectTransform _rect;

        [SerializeField] private Directions hideDirection;
        [SerializeField] private float hideDistance = 50f;

        [Space(10f)]
        [Header("버튼 사용 여부")]
        [SerializeField] private bool isUseButton;
        [SerializeField] private UIClosedWindow Button_backgroundQuit;

        public Action OnDrawerClosed;
        private void Start()
        {
            _rect = GetComponent<RectTransform>();
            _targetPosition = _rect.anchoredPosition;
            Vector3 hideDir = Direction.GetDirection(hideDirection);
            _hidePosition = new Vector3(_targetPosition.x, _targetPosition.y, _targetPosition.z);
            _hidePosition += hideDir * hideDistance;            
            _rect.anchoredPosition = _hidePosition;
        }

        public void TryFlipFlopDrawer()
        {
            StopAllCoroutines();
            if (_isShown)
            {
                StartCoroutine(HideDrawerCoroutine());
                TryFlipFlopOpenButton();
            }
            else
            {
                StartCoroutine(ShowDrawerCoroutine());
                TryFlipFlopOpenButton();
            }
            _isShown = !_isShown;
        }
        public void TryOpenDrawer()
        {
            if (!_isShown)
            {
                StartCoroutine(ShowDrawerCoroutine());
                TryFlipFlopOpenButton();
            }
            _isShown = true;
        }
        public void TryCloseDrawer()
        {
            if (_isShown)
            {
                StartCoroutine(HideDrawerCoroutine());
                TryFlipFlopOpenButton();
            }
            _isShown = false;
        }


        public void OpenDrawer()
        {
            StopAllCoroutines();
            StartCoroutine(ShowDrawerCoroutine());
            TryFlipFlopOpenButton();
            _isShown = true;
        }
        public void CloseDrawer()
        {
            StopAllCoroutines();
            StartCoroutine(HideDrawerCoroutine());
            TryFlipFlopOpenButton();
            _isShown = false;
        }

        private void TryFlipFlopOpenButton()
        {
            if (!isUseButton)
            {
                return;
            }
            Button_backgroundQuit.FlipFlopOpen();
        }

        private IEnumerator ShowDrawerCoroutine()
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * 5;
                _rect.anchoredPosition = Vector3.Lerp(_hidePosition, _targetPosition, t);
                yield return null;
            }
        }
        private IEnumerator HideDrawerCoroutine()
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * 5;
                _rect.anchoredPosition = Vector3.Lerp(_targetPosition, _hidePosition, t);
                yield return null;
            }
            OnDrawerClosed?.Invoke();
        }

        public void OpenWithDelayedClose(float delay)
        {
            OpenDrawer();
            StartCoroutine(DelayedClose(delay));
        }

        private IEnumerator DelayedClose(float delay)
        {
            yield return new WaitForSeconds(delay);
            CloseDrawer();
        }
    }
}