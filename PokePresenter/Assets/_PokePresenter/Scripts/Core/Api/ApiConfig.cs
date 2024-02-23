using UnityEngine;

namespace GlassyCode.PokePresenter.Core.Api
{
    [CreateAssetMenu(menuName = "Configs/Api Config", fileName = "Api Config")]
    public class ApiConfig : ScriptableObject
    {
        //With odin I would make a serialized dictionary <key, url>
        //For my case I just created pu
        
        public const string GetPokesEndpoint = "https://pokeapi.co/api/v2/pokemon?limit=20";
    }
}