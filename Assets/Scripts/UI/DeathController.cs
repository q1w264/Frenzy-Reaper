using Core.Health;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]
    public class DeathController : MonoBehaviour
    {
        public Health playerHealth;
        private UIDocument _uiDocument;
        public PlayerInput playerInput;
        private VisualElement _root;
        private Button _retryButton;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            playerHealth.OnDeath += ShowView;
        }

        private void Start()
        {
            _retryButton = _root.Q<Button>("button-retry");
            _retryButton.clicked += ReloadCurrentScene;
            _root.style.display = DisplayStyle.None;
        }

        private void OnDestroy()
        {
            if (_retryButton != null)
                _retryButton.clicked -= ReloadCurrentScene;
            playerHealth.OnDeath -= ShowView;
        }

        private void ShowView()
        {
            _root.style.display = DisplayStyle.Flex;
            playerInput.defaultActionMap = "UI";
            _retryButton.Focus();
        }

        private void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
