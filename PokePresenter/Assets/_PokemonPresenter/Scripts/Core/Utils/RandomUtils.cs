using UnityEngine;

namespace GlassyCode.PokemonPresenter.Core.Utils
{
    public static class RandomUtils
    {
        public static bool GetRandomBool => Random.Range(0, 2) == 0;
    }
}