using UnityEngine;

namespace HoloLens.StabilizationTest {
    public class StabilizationPlane : MonoBehaviour {

        /// <summary>
        /// The object you would like to be stable
        /// </summary>
        public GameObject focusedObject;
        /// <summary>
        /// The flag to show dammy stabilization plane.
        /// </summary>
        public bool IsVisible;

        private Renderer planeRenderer;

        private Renderer PlaneRenderer {
            get {
                if (planeRenderer == null) {
                    planeRenderer = GetComponent<Renderer>();
                }
                return planeRenderer;
            }
        }

        void Update() {
            UpdateStabilizationPlane();
        }

        /// <summary>
        /// Update Stabilization Plane position and normal
        /// </summary>
        private void UpdateStabilizationPlane() {

            if (focusedObject == null) {
                if (PlaneRenderer.enabled == true) { PlaneRenderer.enabled = false; }
                return;
            }

            var normal = -Camera.main.transform.forward;
            var position = focusedObject.transform.position;
            UnityEngine.XR.WSA.HolographicSettings.SetFocusPointForFrame(position, normal);

            if (IsVisible) {
                if (PlaneRenderer.enabled == false) { PlaneRenderer.enabled = true; return; }
                transform.forward  = normal;
                transform.position = position;
            }
            else {
                if (PlaneRenderer.enabled == true) { PlaneRenderer.enabled = false; }
            }
        }
    }

}