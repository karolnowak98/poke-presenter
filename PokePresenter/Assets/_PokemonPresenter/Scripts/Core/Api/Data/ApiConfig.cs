using UnityEngine;

namespace GlassyCode.PokemonPresenter.Core.Api.Data
{
    [CreateAssetMenu(menuName = "Configs/Api Config", fileName = "Api Config")]
    public class ApiConfig : ScriptableObject
    {
        //With odin I would make a serialized dictionary <key, url>
        //That's more testable way
        //For my case I just created const
        
        public const string GetPokesEndpoint = "https://pokeapi.co/api/v2/pokemon";
    }
}