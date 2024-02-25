using UnityEngine;
using UnityEngine.UI;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Utils
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private Button _xButton;

        private void OnEnable()
        {
            _xButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _xButton.onClick.AddListener(Hide);
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}