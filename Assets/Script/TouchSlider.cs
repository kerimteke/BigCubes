using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

//if (OnPointerDownEvent != null)     // Event dolu ise, ben dokundu isem:...

public class TouchSlider : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnPointerDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction OnPointerUpEvent;

    Slider UISlider;

    private void Awake()
    {
        UISlider = GetComponent<Slider>();
        UISlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();   // yani bu eşit değilse NULL'a yani boş değilse sen bunu çalıştır...
        OnPointerDragEvent?.Invoke(UISlider.value); // Slider sürükleniyor-değeri değişiyor ise sen bu değeri alacaksın..
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke();
        UISlider.value = 0f;
    }

    void OnSliderValueChanged(float Value)
    {
        OnPointerDragEvent?.Invoke(Value);      // slider değiştikçe buradaki action'a value değerini aktarsın..
    }

    private void OnDestroy()    // Listenerleri olası slider deaktif vb. oluşuna karşı pasif yapmak..
    {
        UISlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

}
