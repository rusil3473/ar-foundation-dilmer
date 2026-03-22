using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    
    public GameObject PlacedPrefab{
        get{
            return placedPrefab;
        }
       set{
        placedPrefab = value;
        }
    }

    private ARRaycastManager arRaycasterManager;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        arRaycasterManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

        if (arRaycasterManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) { 
            var hitPose = hits[0].pose;
            Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
        }

    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

}
