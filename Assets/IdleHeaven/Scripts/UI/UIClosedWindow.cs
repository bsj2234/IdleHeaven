using UnityEngine;

public class UIClosedWindow : MonoBehaviour
{
    [SerializeField] private bool _startOpen = false;
    private bool _isOpen = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    private void Start()
    {
        if(_startOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void FlipFlopOpen()
    {

        if (_isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _isOpen = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        _isOpen = false;
    }
}
