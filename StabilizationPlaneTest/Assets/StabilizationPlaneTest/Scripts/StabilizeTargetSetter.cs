using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace HoloLens.StabilizationTest {
    [RequireComponent(typeof(StabilizationPlane))]
    public class StabilizeTargetSetter : MonoBehaviour {

        StabilizationPlane stabilizationPlane;
        StabilizationPlane StabilizationPlane {
            get {
                if (stabilizationPlane == null) {
                    stabilizationPlane = GetComponent<StabilizationPlane>();
                }

                return stabilizationPlane;
            }

            set { stabilizationPlane = value; }
        }

        // Use this for initialization
        void Start() {
            InteractionManager.InteractionSourcePressed += OnTapped;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                var obj = GetTappedObject();
                if (obj != null) { StabilizationPlane.focusedObject = obj; }
            }
        }

        private void OnTapped(InteractionSourcePressedEventArgs e) {
            var obj = GetTappedObject();
            if(obj != null) { StabilizationPlane.focusedObject = obj; }
        }

        private GameObject GetTappedObject() {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                Debug.LogFormat("Hit:{0}",hit.transform.name);
                return hit.transform.gameObject;
            }
            else {
                Debug.Log("No Hit");
                return null;
            }
        }
    }

}