using Core.Health;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class GUIController : MonoBehaviour
    {
        public Health playerHealth;
        private UIDocument  _uiDocument;
        private VisualElement _root;

        void Start()
        {
            if (playerHealth == null)
            {
                Debug.LogError("Player Health is not assigned in the GUIController.");
                return;
            }
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument?.rootVisualElement;
            if (_root != null)
            {
                _root.dataSource =  playerHealth;
            }
        }
        
        private void OnDisable()
        {
            if (_root != null)
            {
                _root.dataSource = null;
            }
        }
    }
}