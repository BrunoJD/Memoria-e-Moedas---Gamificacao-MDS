using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler{
    public void OnDrop(PointerEventData eventData){
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null)
            return;

        DraggablePlayer player = dropped.GetComponent<DraggablePlayer>();
        if (player == null)
            return;

        Transform oldSlot = player.parentAfterDrag;
        DraggablePlayer playerInside = GetComponentInChildren<DraggablePlayer>();

        if (playerInside != null){
            playerInside.transform.SetParent(oldSlot);
            playerInside.parentAfterDrag = oldSlot;
        }

        player.transform.SetParent(transform);
        player.parentAfterDrag = transform;
    }
}