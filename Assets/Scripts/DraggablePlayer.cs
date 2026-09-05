using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggablePlayer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root, false);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("On Drag");
        transform.position = eventData.position;

        // Linha de diagnóstico: mostra o objeto sob o cursor
        if (eventData.pointerCurrentRaycast.gameObject != null) {
            Debug.Log("Mouse sobre: " + eventData.pointerCurrentRaycast.gameObject.name);
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag, false);
        image.raycastTarget = true;
    }
}
