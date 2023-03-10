using System.Collections.Generic;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Service.Classes.Collections;
using EmberBanner.Core.Service.Extensions;
using EmberBanner.Unity.Battle.Systems.CardPlaying.PrePlaying;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.Arrows
{
    public class CardTargetsMatrixUi : EBMonoBehaviour
    {
        [SerializeField] private ActionTypeToArrowSpritesDictionary _arrowSprites;
        [SerializeField] private Transform _arrowsOrigin;
        
        private Dictionary<(BattleUnitCrystalView initiator, BattleUnitCrystalView target), Arrow> _arrows = new();

        private void Awake()
        {
            CardTargetsMatrix.I.onAttackMatrixChanged += RedrawArrows;
        }

        private void RedrawArrows()
        {
            _arrows.Clear();
            foreach (Transform arrowElement in _arrowsOrigin)
            {
                Destroy(arrowElement.gameObject);
            }

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

        private void CreateArrow(BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool clashingArrow = false)
        {
            var actionType = initiator.Card.Entity.MainAction.Model.Type;
            
            var arrow = new Arrow()
            {
                Head = SpawnArrowHeads(actionType, initiator, target, clashingArrow).head,
                Tail = SpawnArrowTail(actionType, initiator, target, clashingArrow)
            };

            _arrows[(initiator, target)] = arrow;
        }
        
        private GameObject SpawnArrowTail(ActionType type, BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool clashingArrow = false)
        {
            var multiplier = !clashingArrow ? 1f : 0.5f;
            var distance = (target.Tran.position - initiator.Tran.position) * multiplier;
            var tail = Instantiate(_arrowSprites[type].ArrowBody, initiator.Tran.position + (distance / 2), Quaternion.identity, _arrowsOrigin);
            tail.transform.LookAt2D(target.Tran.position, true);
            tail.transform.localScale = new Vector3(distance.magnitude / 0.32f * 2 - (!clashingArrow ? 0f : 0.64f), 1f, 1f);

            return tail;
        }
        
        private (GameObject head, GameObject secondHead) SpawnArrowHeads(ActionType type, BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool clashingArrow = false)
        {
            var multiplier = !clashingArrow ? 1f : 0.5f;
            var distance = (target.Tran.position - initiator.Tran.position) * multiplier;
            if (clashingArrow)
                distance = distance + distance.normalized * 0.08f;
            var head = Instantiate(_arrowSprites[type].ArrowHead, initiator.Tran.position + ((distance) / 2), Quaternion.identity, transform);

            GameObject secondHead = null;
            /*if (isClash)
            {
                secondHead = Instantiate(!isClash ? _arrowHeadPrototypes[0] : _arrowHeadPrototypes[1], start.transform.position + distance / 2, Quaternion.identity, transform);
                secondHead.transform.LookAt2D(start.transform.position, true);
                secondHead.transform.position = start.transform.position;
            }*/
            head.transform.LookAt2D(target.Tran.position, true);
            head.transform.position = !clashingArrow ? target.Tran.position : target.Tran.position - distance;

            return (head, secondHead);
        }

        private void RemoveArrow(BattleUnitCrystalView initiator)
        {
            var keysToRemove = new List<(BattleUnitCrystalView initiator, BattleUnitCrystalView target)>();
            foreach (var arrowKey in _arrows.Keys)
            {
                if (arrowKey.initiator != initiator) continue;
                
                var arrow = _arrows[(initiator, arrowKey.target)];
                Destroy(arrow.Head);
                Destroy(arrow.Tail);
                keysToRemove.Add((initiator, arrowKey.target));
            }

            foreach (var keyToRemove in keysToRemove)
            {
                _arrows.Remove(keyToRemove);
            }
        }
    }
}