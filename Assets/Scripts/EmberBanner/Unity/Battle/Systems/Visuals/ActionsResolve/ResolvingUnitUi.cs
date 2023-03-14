using EmberBanner.Core.Enums.Battle;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Service;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.ActionsResolve
{
    public class ResolvingUnitUi : EBMonoBehaviour
    {
        [SerializeField] private SpriteRenderer _unitSprite;
        [SerializeField] private BattleUnitCrystalsView _unitCrystals;
        [SerializeField] private Transform _unitHealthBar;
        [SerializeField] private TextMeshPro _unitHealthText;

        private BattleUnitView _unit;
        
        public void SetUnit(BattleUnitView unit)
        {
            gameObject.SetActive(true);
            _unit = unit;
            SetGraphics();
            AddCrystals();
        }
        
        private void SetGraphics()
        {
            _unitSprite.sprite = _unit.Entity.Model.Sprite;
            if (_unit.Controller == UnitControllerType.Enemy)
                _unitSprite.transform.localScale = new(-1f, 1f, 1f);
        }

        private void AddCrystals()
        {
            _unitCrystals.SetCrystals(_unitCrystals.Crystals);
        }
        
        private void Update()
        {
            if (_unit == null) return;
            
            _unitHealthBar.localScale = new Vector3((float)_unit.Entity.CurrentHealth / _unit.Entity.MaxHealth.CalculateValue(), 1f, 1f);
            _unitHealthText.text = $"{_unit.Entity.CurrentHealth}/{_unit.Entity.MaxHealth.CalculateValue()}";
        }

        public void Clear()
        {
            _unit = null;
            gameObject.SetActive(false);
        }
    }
}