﻿using Leopotam.Ecs;
using Scripts.Data;
using Scripts.Items.Items;
using UnityEngine;

namespace Scripts.Items.Systems
{
    public class ItemMoverToStackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Item, MoveToStack> _filter = null;
        private readonly ItemMovingData _movingData;
        
        public void Run()
        {
            if(_filter.IsEmpty())
                return;

            foreach (var i in _filter)
            {
                ref Item item = ref _filter.Get1(i);
                ref EcsEntity entity = ref _filter.GetEntity(i);

                Vector3 toPosition = Vector3.Lerp(item.Transform.localPosition, Vector3.zero, _movingData.MoveSpeed * Time.deltaTime);
                item.Transform.localPosition = toPosition;
                item.Transform.localRotation = 
                    Quaternion.RotateTowards(item.Transform.localRotation, Quaternion.Euler(Vector3.zero), _movingData.RotationSpeed * Time.deltaTime );

                if (item.Transform.localPosition == Vector3.zero)
                {
                    item.Transform.localRotation = Quaternion.Euler(Vector3.zero);
                    item.Transform.localPosition = Vector3.zero;
                    entity.Del<MoveToStack>();
                    entity.Get<ItemInStack>() = new ItemInStack()
                    {
                        key = item.Transform.parent.name
                    };
                }
            }
        }
    }
}