using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Utils
{
    public class HelperPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemModelsNumberTmp;
        [SerializeField] private Button _addRandomModelBtn;
        [SerializeField] private Button _removeCheckedModelsBtn;
        [SerializeField] private GlassyScrollView _glassyScrollView;

        private void Start()
        {
            _itemModelsNumberTmp.text = $"Models number: {_glassyScrollView.CurrentModelsNumber}";
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            _addRandomModelBtn.onClick.RemoveAllListeners();
            _removeCheckedModelsBtn.onClick.RemoveAllListeners();
        }

        private void UpdateModelsNumberTmp(int modelsNumber) => _itemModelsNumberTmp.text = $"Models number: {modelsNumber}";
    }
}