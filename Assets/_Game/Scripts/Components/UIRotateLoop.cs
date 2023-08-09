using UnityEngine;
public class UIRotateLoop : MonoBehaviour
{
    private RectTransform rectComponent;
        
    public float rotateSpeed = -200f;
        
    // Start is called before the first frame update
    void Start()
    {
        rectComponent = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}