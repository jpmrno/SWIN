using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextVolumeSlider : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Text TextIndicator;

    private bool _selected = false;

    private void Start()
    {
        TextIndicator.text = ((int) (AudioListener.volume * 100)).ToString(CultureInfo.InvariantCulture);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _selected = true;
    }
    
    public void OnDeselect(BaseEventData data)
    {
        _selected = false;
    }

    private void Update()
    {
        if (!_selected) return;
        
        // TODO: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            VolumeDown();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            VolumeUp();
        }
    }

    public void VolumeDown()
    {
        if (!(AudioListener.volume > 0)) return;

        AudioListener.volume = AudioListener.volume - 0.01F;
        TextIndicator.text = ((int) (AudioListener.volume * 100)).ToString(CultureInfo.InvariantCulture);
    }

    public void VolumeUp()
    {
        if (!(AudioListener.volume < 1)) return;

        AudioListener.volume = AudioListener.volume + 0.01F;
        TextIndicator.text = ((int) (AudioListener.volume * 100)).ToString(CultureInfo.InvariantCulture);
    }
}