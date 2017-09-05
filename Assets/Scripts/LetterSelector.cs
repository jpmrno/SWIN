using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterSelector : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Text TextIndicator;
    
    private static readonly char[] Letters = {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    };

    private int _current = 0;
    private bool _selected = false;
    private float _remainingCoolDownTime;

    private void Start()
    {
        TextIndicator.text = Letters[_current].ToString();
    }
    
    private void Update()
    {
        if (!_selected) return;
        
        _remainingCoolDownTime -= Time.deltaTime;

        // TODO: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        if (Input.GetKey(KeyCode.DownArrow) && _remainingCoolDownTime < 0)
        {
            LetterDown();
        }
        else if (Input.GetKey(KeyCode.UpArrow) && _remainingCoolDownTime < 0)
        {
            LetterUp();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _selected = true;
    }

    public void OnDeselect(BaseEventData data)
    {
        _selected = false;
    }

    public void LetterDown()
    {
        _current--;
        if (_current < 0) _current = Letters.Length - 1;

        TextIndicator.text = Letters[_current].ToString();
        _remainingCoolDownTime = 0.1F;
    }

    public void LetterUp()
    {
        _current++;
        if (_current >= Letters.Length)
            _current = 0;

        TextIndicator.text = Letters[_current].ToString();
        _remainingCoolDownTime = 0.1F;
    }
}