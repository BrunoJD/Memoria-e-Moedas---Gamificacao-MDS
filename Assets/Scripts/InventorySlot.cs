using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler{
    public void OnDrop(PointerEventData eventData){
        Debug.Log("DROP NO SLOT");
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null)
            return;

        DraggablePlayer player = dropped.GetComponent<DraggablePlayer>();
        if (player == null)
            return;

        Transform oldSlot = player.parentAfterDrag;
        DraggablePlayer playerInside = GetComponentInChildren<DraggablePlayer>();

        if (playerInside != null){
            playerInside.parentAfterDrag = oldSlot;
            playerInside.transform.SetParent(oldSlot, false);
        }

        player.parentAfterDrag = transform;
    }
}