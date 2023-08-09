using System;
using UnityEngine;
using System.Collections;
using System.Drawing;
using DG.Tweening;

namespace Wacki.IndentSurface
{

    public class IndentDraw : MonoBehaviour
    {
        public Texture2D texture;      
        public Texture2D stampTexture;  
        public RenderTexture tempTestRenderTexture;
        public int rtWidth = 512;
        public int rtHeight = 512;

        private RenderTexture targetTexture;
        private RenderTexture auxTexture;

        public Material mat;

        public LayerMask LayerMaskRaycast;

        public Vector3 PrevMousePosition
        {
            get => _prevMousePosition;
        }

        // mouse debug draw
        private Vector3 _prevMousePosition;
        private bool _mouseDrag = false;
        private bool _lastEventMouseDown = false;

        private bool _isDrawing;

        private PlaneIceCream _planeIceCream;

        void Awake()
        {
            _planeIceCream = GetComponent<PlaneIceCream>();
            
            targetTexture = new RenderTexture(rtWidth, rtHeight, 32);

            // temporarily use a given render texture to be able to see how it looks
            targetTexture = tempTestRenderTexture;
            auxTexture = new RenderTexture(rtWidth, rtHeight, 32);

            GetComponent<Renderer>().material.SetTexture("_Indentmap", targetTexture);
            Graphics.Blit(texture, targetTexture);  
        }

        private Vector2 _targetPoint;
        private Vector2 _lastPoint;

        private float _distanceToDraw = 0.0005f;

        // add an indentation at a raycast hit position
        public void IndentAt(RaycastHit hit)
        {
            if (hit.collider.gameObject != this.gameObject) return;
            
            float x = hit.textureCoord.x;
            float y = hit.textureCoord.y;

            _targetPoint = new Vector2(x, y);
            if(_lastPoint == Vector2.zero) _lastPoint = new Vector2(x, y);

            var dist = Vector3.Distance(_targetPoint, _lastPoint);
            int num = Mathf.FloorToInt(dist / _distanceToDraw);
            
            if (dist < _distanceToDraw) return;
            Debug.Log("==> Num " + num + " ||  " + dist);

            for (int i = 0; i < num; i++)
            {
                var point = LerpByDistance(_targetPoint, _lastPoint, _distanceToDraw * i);
                DrawAt(point.x * targetTexture.width, point.y * targetTexture.height, 1.0f);
            }

            _lastPoint = new Vector2(x, y);
        }

        public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
        {
            Vector3 P = x * Vector3.Normalize(B - A) + A;
            return P;
        }

        private bool draw;
        void Update()
        {
            if (!GameInfo.IsStateMakeConeIceCream) return;
            
            // bool draw = false;
            float drawThreshold = 0.01f;
            
            // force a draw on mouse down
            //draw = Input.GetMouseButtonDown(0);

            if (!draw)
            {
                // Debug.Log("==> Draw " + draw);
                return;
            }
            
            // set dragging state
            _mouseDrag = Input.GetMouseButton(0);

            if (Camera.main == null) return;            

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10f, LayerMaskRaycast))
            {
                
            }

            if (hit.collider == null)
            {
                Debug.Log("==> Hit Collider Null");
                if (_isDrawing)
                {
                    ExitPlane();
                }
                _isDrawing = false;
                return;
            }

            _isDrawing = true;

            if (_mouseDrag && (draw || Vector3.Distance(hit.point, _prevMousePosition) > drawThreshold))
            {
                Debug.Log(Vector3.Distance(hit.point, _prevMousePosition));
                if(Vector3.Distance(hit.point, _prevMousePosition) > _distanceToDraw){
                    _prevMousePosition = hit.point;
                    IndentAt(hit);
                }
            }
        }

        void ExitPlane()
        {
            Debug.Log("Exit Plane ==>");
            ScoopDone(true);
            _lastPoint = Vector3.zero;
        }

        void OnMouseDown()
        {
            Debug.Log("On Mouse Down");
            _lastEventMouseDown = true;
            draw = true;
            ScoopStart();
            _lastPoint = Vector3.zero;

        }
        
        void OnMouseUp()
        {
            Debug.Log("On Mouse Up");
            if(_lastEventMouseDown) ScoopDone();
            _lastEventMouseDown = false;
            draw = false;
            _lastPoint = Vector3.zero;
        }

        void ScoopStart()
        {
            // Callback Create Scoop!
            _planeIceCream.CreateScoop();
        }

        void ScoopDone(bool isExitPlane = false)
        {
            Debug.Log("Scoop Done!!!");
            
            draw = false;

            DOVirtual.DelayedCall(ConfigGame.DelayClearTexturePlaneIceCream, ClearTexture);

            // Callback Scoop Done!
            _planeIceCream.ScoopDone(isExitPlane);
        }

        void ClearTexture()
        {
            // Reset Target Texture Scoop!
            targetTexture.Release();
            GetComponent<Renderer>().material.SetTexture("_Indentmap", targetTexture);
            Graphics.Blit(texture, targetTexture);
        }

        /// <summary>
        /// todo:   it would probably be a bit more straight forward if we didn't use Graphics.DrawTexture
        ///         and instead handle everything ourselves. This way we could directly provide multiple 
        ///         texture coordinates to each vertex.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alpha"></param>
        void DrawAt(float x, float y, float alpha)
        {
            Graphics.Blit(targetTexture, auxTexture);

            // activate our render texture
            RenderTexture.active = targetTexture; 

            GL.PushMatrix();
            GL.LoadPixelMatrix(0, targetTexture.width, targetTexture.height, 0);

            x = Mathf.Round(x);
            y = Mathf.Round(y);

            // setup rect for our indent texture stamp to draw into
            Rect screenRect = new Rect();
            // put the center of the stamp at the actual draw position
            screenRect.x = x - stampTexture.width * 0.5f;
            screenRect.y = (targetTexture.height - y) - stampTexture.height * 0.5f;
            screenRect.width = stampTexture.width;
            screenRect.height = stampTexture.height;

            var tempVec = new Vector4();

            tempVec.x = screenRect.x / ((float)targetTexture.width);
            tempVec.y = 1 - (screenRect.y / (float)targetTexture.height);
            tempVec.z = screenRect.width / targetTexture.width;
            tempVec.w = screenRect.height / targetTexture.height;
            tempVec.y -= tempVec.w;

            mat.SetTexture("_MainTex", stampTexture);
            mat.SetVector("_SourceTexCoords", tempVec);
            mat.SetTexture("_SurfaceTex", auxTexture);

            // Draw the texture
            Graphics.DrawTexture(screenRect, stampTexture, mat);

            GL.PopMatrix();
            RenderTexture.active = null;
        }
    }

}