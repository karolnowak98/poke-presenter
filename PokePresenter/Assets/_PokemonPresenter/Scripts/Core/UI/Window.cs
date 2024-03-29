using UnityEngine;
using UnityEngine.UI;

namespace GlassyCode.PokemonPresenter.Core.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private Button _cancelButton;

        private void OnEnable()
        {
            _cancelButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _cancelButton.onClick.AddListener(Hide);
        }

        protected virtual void Show() => gameObject.SetActive(true);
        protected virtual void Hide() => gameObject.SetActive(false);
    }
}