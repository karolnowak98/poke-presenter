using UnityEngine;
using Zenject;

namespace GlassyCode.PokemonPresenter.Pokemons.Logic
{
    public class PokeApiInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _apiPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<IPokeApiController>().To<PokeApiController>().FromComponentInNewPrefab(_apiPrefab).AsSingle().NonLazy();
        }
    }
}