using UnityEngine;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Utils
{
    public static class RandomUtils
    {
        public static bool GetRandomBool => Random.Range(0, 2) == 0;
    }
}