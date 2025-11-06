using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRGrabber : MonoBehaviour
{
    public bool isLeftHand = true; 
    private InputDevice device;

    private GameObject heldObject;
    private Rigidbody heldRigidbody;
    private Transform grabPoint;
    public float rayDistanceForPC;

    // For PC mode
    private bool simulateInEditor = true;
    private Camera mainCam;

    void Start()
    {
        if(Application.isEditor)
        {
            simulateInEditor = true;
        }

        grabPoint = new GameObject("GrabPoint").transform;
        grabPoint.SetParent(transform);
        grabPoint.localPosition = Vector3.zero;
        grabPoint.localRotation = Quaternion.identity;

        mainCam = Camera.main;
    }

    void Update()
    {
        if (!device.isValid) {
            InitializeDevice();
        }

        bool isGripping = false;

        // XR path
        if (device.isValid && device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue))
        {
            isGripping = gripValue;
        }
        // PC fallback path
        else if (simulateInEditor)
        {
            // Move "hand" with mouse ray
            if (mainCam != null && Application.isFocused)
            {
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                transform.position = ray.origin + ray.direction * rayDistanceForPC; // 2m in front of camera
                transform.rotation = Quaternion.LookRotation(ray.direction);
            }

            isGripping = Input.GetKey(KeyCode.O); // E to grab
        }

        // Grabbing logic
        if (isGripping && heldObject == null)
            TryGrab();
        else if (!isGripping && heldObject != null)
            Release();
    }

    void InitializeDevice()
    {
        var devices = new List<InputDevice>();
        InputDeviceCharacteristics controllerCharacteristics =
            InputDeviceCharacteristics.HeldInHand |
            InputDeviceCharacteristics.Controller |
            (isLeftHand ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right);

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
            device = devices[0];
        //else
         //   transform.GetChild(0).gameObject.SetActive(false); //disable hand object
    }

    void TryGrab()
    {
        Collider[] hits = Physics.OverlapSphere(grabPoint.position, 0.1f);
        foreach (var hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                heldObject = hit.gameObject;
                heldRigidbody = hit.attachedRigidbody;
                heldRigidbody.isKinematic = true;
                heldObject.transform.SetParent(grabPoint);
                return;
            }
        }
    }

    void Release()
    {
        if (heldObject == null) return;

        heldObject.transform.SetParent(null);
        heldRigidbody.isKinematic = false;

        // Throw if VR active
        if (device.isValid)
        {
            if (device.TryGetFeatureValue(CommonUsages.deviceVelocity, out var vel))
                heldRigidbody.linearVelocity = vel;
            if (device.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out var angVel))
                heldRigidbody.angularVelocity = angVel;
        }

        heldObject = null;
        heldRigidbody = null;
    }

    void OnDrawGizmos()
    {
        if (grabPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(grabPoint.position, 0.1f);
        }
    }
}