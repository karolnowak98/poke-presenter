using GlassyCode.PokemonPresenter.Core.Api.Data;
using UnityEngine;
using Zenject;

namespace GlassyCode.PokemonPresenter.Core.Api.Logic
{
    public class ApiInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _apiConnectionPrefab;
        [SerializeField] private ApiConfig _apiConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_apiConfig);
            Container.Bind<ApiController>().FromComponentInNewPrefab(_apiConnectionPrefab).AsSingle().NonLazy();
        }
    }
}