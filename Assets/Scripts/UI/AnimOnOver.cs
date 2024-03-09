using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimOnOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator m_anim;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_anim.SetBool("enter", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_anim.SetBool("enter", false);
    }
}
