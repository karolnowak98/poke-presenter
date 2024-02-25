using UnityEngine;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Utils
{
    public abstract class Panel : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}