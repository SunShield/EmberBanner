using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Models.Actions;
using EmberBanner.Core.Service.Classes.Collections;
using EmberBanner.Core.Service.Extensions;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.Arrows
{
    public class CardTargetsMatrixUi : EBMonoBehaviour
    {
        private static CardTargetsMatrixUi _instance;
        public static CardTargetsMatrixUi I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<CardTargetsMatrixUi>(true);
                return _instance;
            }
        }
        
        private const float OneSidedArrowLengthMultiplier = 1f;
        private const float ClashingArrowLengthMultiplier = 0.5f;
        private const float ArrowSideLength = 0.32f;
        private const float WeirdConstantToRuleTheUniverse = 0.08f;
        
        [SerializeField] private ActionTypeToArrowSpritesDictionary _arrowSprites;
        [SerializeField] private ActionArrowSprites _voidedArrowSprites;
        [SerializeField] private Transform _arrowsOrigin;
        
        private Dictionary<(BattleUnitCrystalView initiator, BattleUnitCrystalView target), Arrow> _arrows = new();

        private void Awake()
        {
            CardTargetsMatrix.I.onAttackMatrixChanged += RedrawArrows;
        }

        private void RedrawArrows()
        {
            ClearArrows();
            DestroyPreviousArrowGos();

            var attackMatrix = CardTargetsMatrix.I.AttackMatrix;
            var attackersToSkip = new HashSet<BattleUnitCrystalView>();
            foreach (var initiator in attackMatrix.Keys)
            {
                if (attackersToSkip.Contains(initiator)) continue;
                
                var clashingCrystal = CardTargetsMatrix.I.GetClashingCrystal(initiator);
                if (clashingCrystal != null)
                {
                    attackersToSkip.Add(attackMatrix[initiator]);
                    CreateArrow(initiator, clashingCrystal, true);
                    CreateArrow(clashingCrystal, initiator, true);
                }
                else
                {
                    CreateArrow(initiator, attackMatrix[initiator]);
                }
            }
        }

        private void ClearArrows() => _arrows.Clear();

        private void DestroyPreviousArrowGos()
        {
            foreach (Transform arrowElement in _arrowsOrigin)
            {
                Destroy(arrowElement.gameObject);
            }
        }

        private void CreateArrow(BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool isClashingArrow = false)
        {
            var actionsFittingTarget = new List<ActionModel>();
            if (initiator.Controller != target.Controller)
                actionsFittingTarget = initiator.Card.Entity.GetActionsTargetingEnemy();
            else if (initiator == target)
                actionsFittingTarget = initiator.Card.Entity.GetActionsTargetingSelf();
            else if (initiator.Controller == target.Controller)
                actionsFittingTarget = initiator.Card.Entity.GetActionsTargetingAlly();

            var action = actionsFittingTarget.FirstOrDefault();
            var actionType = action?.Type;
            
            var arrow = new Arrow()
            {
                Head = SpawnArrowHeads(actionType, initiator, target, isClashingArrow),
                Tail = SpawnArrowTail(actionType, initiator, target, isClashingArrow)
            };

            _arrows[(initiator, target)] = arrow;
        }
        
        private GameObject SpawnArrowTail(ActionType? type, BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool isClashingArrow = false)
        {
            var lengthMultiplier = GetClashingArrowLengthMultiplier(isClashingArrow);
            var distanceBetweenCrystals = GetDistanceBetweenCrystals(initiator, target, lengthMultiplier);
            var arrowPosition = GetArrowPosition(initiator, distanceBetweenCrystals);
            var tail = InstantiateArrow(type, arrowPosition);
            RotateTransformOntoTargetCrystal(target, tail);
            tail.transform.localScale = CalculateFinalArrowLength(isClashingArrow, distanceBetweenCrystals);

            return tail;
        }

        private float GetClashingArrowLengthMultiplier(bool isClashingArrow) 
            => !isClashingArrow 
                ? OneSidedArrowLengthMultiplier 
                : ClashingArrowLengthMultiplier;

        private Vector3 GetDistanceBetweenCrystals(BattleUnitCrystalView initiator, BattleUnitCrystalView target, float lengthMultiplier)
                    => (target.Tran.position - initiator.Tran.position) * lengthMultiplier;
        
        private Vector3 GetArrowPosition(BattleUnitCrystalView initiator, Vector3 distanceBetweenCrystals) 
            => initiator.Tran.position + (distanceBetweenCrystals / 2);

        private GameObject InstantiateArrow(ActionType? type, Vector3 arrowPosition)
            => Instantiate(type.HasValue ? _arrowSprites[type.Value].ArrowBody : _voidedArrowSprites.ArrowBody, arrowPosition, Quaternion.identity, _arrowsOrigin);

        private void RotateTransformOntoTargetCrystal(BattleUnitCrystalView target, GameObject tran)
            => tran.transform.LookAt2D(target.Tran.position, true);
        
        private Vector3 CalculateFinalArrowLength(bool isClashingArrow, Vector3 distanceBetweenCrystals)
            => new (distanceBetweenCrystals.magnitude / ArrowSideLength * 2 - (!isClashingArrow ? 0f : ArrowSideLength * 2), 1f, 1f);

        private GameObject SpawnArrowHeads(ActionType? type, BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool isClashingArrow = false)
        {
            var lengthMultiplier = GetClashingArrowLengthMultiplier(isClashingArrow);
            var distanceBetweenCrystals = GetDistanceBetweenCrystals(initiator, target, lengthMultiplier);
            ShiftArrowHeadForClashingArrow(isClashingArrow, ref distanceBetweenCrystals);
            var head = InstantiateHead(type, initiator, distanceBetweenCrystals);

            RotateTransformOntoTargetCrystal(target, head);
            head.transform.position = GetHeadPosition(target, isClashingArrow, distanceBetweenCrystals);

            return head;
        }

        private void ShiftArrowHeadForClashingArrow(bool isClashingArrow, ref Vector3 distanceBetweenCrystals)
        {
            if (isClashingArrow)
                distanceBetweenCrystals += distanceBetweenCrystals.normalized * WeirdConstantToRuleTheUniverse;
        }

        private GameObject InstantiateHead(ActionType? type, BattleUnitCrystalView initiator, Vector3 distanceBetweenCrystals)
            => Instantiate(type.HasValue ? _arrowSprites[type.Value].ArrowHead : _voidedArrowSprites.ArrowHead, initiator.Tran.position + ((distanceBetweenCrystals) / 2), Quaternion.identity, transform);

        private Vector3 GetHeadPosition(BattleUnitCrystalView target, bool isClashingArrow, Vector3 distanceBetweenCrystals)
            => !isClashingArrow ? target.Tran.position : target.Tran.position - distanceBetweenCrystals;

        public void OnTurnEnd()
        {
            ClearArrows();
            DestroyPreviousArrowGos();
        }
    }
}