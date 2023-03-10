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
            CardTargetsMatrix.I.onAttackAdded   += OnAttackAdded;
            CardTargetsMatrix.I.onAttackRemoved += OnAttackRemoved;
        }

        private void OnAttackAdded(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            CreateArrow(initiator, target);
        }

        private void OnAttackRemoved(BattleUnitCrystalView initiator)
        {
            RemoveArrow(initiator);
        }

        private void CreateArrow(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            var actionType = initiator.Card.Entity.MainAction.Model.Type;
            
            var arrow = new Arrow()
            {
                Head = SpawnArrowHeads(actionType, initiator, target).head,
                Tail = SpawnArrowTail(actionType, initiator, target)
            };

            _arrows[(initiator, target)] = arrow;
        }
        
        private GameObject SpawnArrowTail(ActionType type, BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            var distance = target.Tran.position - initiator.Tran.position;
            var tail = Instantiate(_arrowSprites[type].ArrowBody, initiator.Tran.position + distance / 2, Quaternion.identity, _arrowsOrigin);
            tail.transform.LookAt2D(target.Tran.position, true);
            tail.transform.localScale = new Vector3(distance.magnitude / 0.32f * 2, 1f, 1f);

            return tail;
        }
        
        private (GameObject head, GameObject secondHead) SpawnArrowHeads(ActionType type, BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            var distance = target.Tran.position - initiator.Tran.position;
            var head = Instantiate(_arrowSprites[type].ArrowHead, initiator.Tran.position + distance / 2, Quaternion.identity, transform);

            GameObject secondHead = null;
            /*if (isClash)
            {
                secondHead = Instantiate(!isClash ? _arrowHeadPrototypes[0] : _arrowHeadPrototypes[1], start.transform.position + distance / 2, Quaternion.identity, transform);
                secondHead.transform.LookAt2D(start.transform.position, true);
                secondHead.transform.position = start.transform.position;
            }*/
            head.transform.LookAt2D(target.Tran.position, true);
            head.transform.position = target.Tran.position;

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