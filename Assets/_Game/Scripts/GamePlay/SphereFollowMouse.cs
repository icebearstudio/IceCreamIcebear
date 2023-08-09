using System;
using DG.Tweening;
using UnityEngine;
using Wacki.IndentSurface;
using Random = UnityEngine.Random;

public class SphereFollowMouse : MonoBehaviour
{
    public Transform planeTransform; // Reference to the plane's transform
    public float sphereHeight = 1f; // Height at which the sphere will move on the plane
    public float rotationSpeed = 5f; // Rotation speed based on mouse movement

    [Header("Layer")] 
    public LayerMask _layerIgnore;
    public LayerMask _layerCanPick;

    private Rigidbody rb;
    private Plane plane;
    private Vector3 lastMousePosition;
    private Vector3 lastMouseEulerAngle;
    private bool isMousePressed = false;
    private bool isCanPick;

    private IndentDraw _indentDraw;
    private bool _isReady;
    private bool _isOnPot;
    private bool _isReleased;
    private EIceCream _iceCreamId;

    [HideInInspector] public Action<EIceCream, GameObject> OnDropOnPotEvent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void Init(EIceCream iceCreamId)
    {
        plane = new Plane(Vector3.up, planeTransform.position + new Vector3(0, 2.5f, 0f));

        _indentDraw = planeTransform.GetComponent<IndentDraw>();

        _isReady = true;

        _iceCreamId = iceCreamId;

        SetStateIgnore();
    }

    public void SetStateIgnore()
    {
        gameObject.layer = _layerIgnore;
        isCanPick = false;
    }
    
    public void SetStateCanPick(bool flagPress = false)
    {
        // transform.position = new Vector3(transform.position.x, planeTransform.position.y, transform.position.z);
        gameObject.layer = _layerCanPick;
        rb.useGravity = true;

        isMousePressed = flagPress;

        // DOVirtual.DelayedCall(0.1f, () =>
        // {
            isCanPick = true;    
        // });
        
        Debug.Log("Set Can Pick " + isMousePressed);
    }

    private void OnMouseDown()
    {
        if (!isCanPick) return;
        
        _isReleased = false;
        isMousePressed = true;
        lastMousePosition = Input.mousePosition;
        lastMouseEulerAngle = transform.localEulerAngles;
        
        Debug.Log("Press Sphere Scoop!");
    }

    private void OnMouseUp()
    {
        Debug.Log("==> Check Mouse Up " + isCanPick + " " + _isOnPot);
        Release();
    }

    private void Release()
    {
        if (!isCanPick) return;
        
        if (_isReleased) return;

        isMousePressed = false;
        
        if (_isOnPot)
        {
            if (Helper.IsCanDropToPot(_iceCreamId))
            {
                transform.DOScale(Vector3.one * ConfigGame.ScaleScoopOnPot, 0.2f).SetEase(Ease.Linear);
            }
            else
            {
                // DOVirtual.DelayedCall(0.1f, () =>
                // {
                    Debug.Log("==> Add Force ");
                    // rb.AddForce(new Vector3(Random.Range(500f, 1500f), 0f, 0));
                    rb.AddForce(new Vector3(50f, 0f, 0), ForceMode.Impulse);
                // });
            }
            
            _isReleased = true;
        }
    }

    public void PutInPot()
    {
        Debug.Log("==> Put In Pot");
        OnDropOnPotEvent?.Invoke(_iceCreamId, gameObject);
    }

    private void Update()
    {
        if (!_isReady) return;

        if (isCanPick)
        {
            // return;
            if (isMousePressed)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float rayDistance;

                if (plane.Raycast(ray, out rayDistance))
                {
                    Vector3 targetPosition = ray.GetPoint(rayDistance);
                    // targetPosition.y = sphereHeight;

                    transform.position = targetPosition;
                    transform.localEulerAngles = lastMouseEulerAngle;
                }
            }
            else
            {
                Release();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMousePressed = true;
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isMousePressed = false;
            }

            if (isMousePressed)
            {
                transform.position = _indentDraw.PrevMousePosition;
            }
        }

        
        if (!isMousePressed)
        {
            // Stop the sphere from rotating when the mouse button is not pressed
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void SetInPot()
    {
        _isOnPot = true;
    }
    
    public void SetOutPot()
    {
        _isOnPot = false;
    }
}
