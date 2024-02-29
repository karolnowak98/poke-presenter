using System.Linq;
using GlassyCode.PokemonPresenter.Core.UI;
using GlassyCode.PokemonPresenter.Pokemons.Data.Models;
using GlassyCode.PokemonPresenter.Pokemons.Logic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GlassyCode.PokemonPresenter.Pokemons.AudioVisual
{
    public class PokemonDetailsWindow : Window
    {
        [SerializeField] private RawImage _frontImage;
        [SerializeField] private RawImage _backImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _heightText;
        [SerializeField] private TextMeshProUGUI _weightText;
        [SerializeField] private TextMeshProUGUI _abilitiesText;
        [SerializeField] private PokemonAttributeUI[] _attributeUIArray;

        private IPokeApiController _pokeApiController;
        
        [Inject]
        private void Construct(IPokeApiController pokeApiController)
        {
            _pokeApiController = pokeApiController;
            
            _pokeApiController.OnFinishDownloadingPokemonDetails += UpdateDetailsWindow;
        }
        
        private void OnDestroy()
        {
            _pokeApiController.OnFinishDownloadingPokemonDetails -= UpdateDetailsWindow;
        }
        
        private void UpdateDetailsWindow(PokemonDetails pokemonDetails)
        {
            SetImage(pokemonDetails.BackTexture2D, _backImage);
            SetImage(pokemonDetails.FrontTexture2D, _frontImage);
            
            _nameText.text = $"{pokemonDetails.name}".FirstCharacterToUpper().Replace("-"," ");
            _heightText.text = $"{pokemonDetails.height}M";
            _weightText.text = $"{pokemonDetails.weight}KG";
        
            _abilitiesText.text = string.Join(", ", pokemonDetails.abilities.
                Select(ability => ability.ability.name.FirstCharacterToUpper().Replace("-"," ")));
        
            for (var i = 0; i < _attributeUIArray.Length && i < pokemonDetails.stats.Length; i++)
            {
                _attributeUIArray[i].Slider.value = pokemonDetails.stats[i].base_stat;
                _attributeUIArray[i].Value.text = pokemonDetails.stats[i].base_stat.ToString();
            }
            
            Show();
        }
        
        private static void SetImage(Texture texture, RawImage image)
        {
            var isTexture = texture != null;
            
            image.gameObject.SetActive(isTexture);
            
            if (isTexture)
            {
                image.texture = texture;
            }
        }
    }
}
