using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GlassyCode.PokemonPresenter.Core.Api.AudioVisual
{
    public class PokemonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pokemonNameTmp;
        [SerializeField] private Button _openDetailsWindowBtn;

        public TextMeshProUGUI PokemonNameTmp => _pokemonNameTmp;
        public Button OpenDetailsWindowBtn => _openDetailsWindowBtn;
    }
}