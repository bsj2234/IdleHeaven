using UnityEngine;

public class UIClosedWindow : MonoBehaviour
{
    private bool _isOpen = false;
    [SerializeField] private bool _startOpen = false;
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
