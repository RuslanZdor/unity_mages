using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapPointController : MonoBehaviour, IPointerClickHandler {

	public MapPoint mapPoint;

    public void OnPointerClick(PointerEventData eventData) {
        if (mapPoint.isEnable) {
            transform.parent.parent.GetComponent<MapController>().openFight(mapPoint);
        }
    }

    public void disablePoint() {
        mapPoint.isEnable = false;
        transform.GetComponent<Image>().color = new Color((float)157 / 256, (float)157 / 256, (float)157 / 256, (float)157 / 256);
    }

    public void enablePoint() {
        mapPoint.isEnable = true;
        transform.GetComponent<Image>().color = new Color((float)245 / 256, (float)48 / 256, (float)48 / 256, (float)157 / 256);
    }
}
