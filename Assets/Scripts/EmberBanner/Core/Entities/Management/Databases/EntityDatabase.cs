using System;
using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Entities.Management.SaveLoad.Data;
using EmberBanner.Core.Ingame.Management.SaveLoad;
using EmberBanner.Core.Models;
using EmberBanner.Core.Service.Utilities;
using Sirenix.Serialization;
using UnityEngine;

namespace EmberBanner.Core.Entities.Management.Databases
{
    public abstract class EntityDatabase<TEntity, TModel, TSaveData>
        where TEntity : SavableEntity<TModel, TSaveData>
        where TModel : AbstractModel
        where TSaveData : AbstractEntitySaveData, new()
    {
        public Dictionary<int, TEntity> Entities { get; } = new();
        private string Key => $"Entities_{typeof(TEntity).Name}";

        public void AddEntity(TEntity entity)
        {
            Entities.Add(entity.Id, entity);
            Save();
        }

        public void RemoveEntity(TEntity entity)
        {
            Entities.Remove(entity.Id);
            Save();
        }

        public void Save()
        {
            if (Entities.Count == 0) return;
            
            var saveDatas = Entities.ToDictionary(kv => kv.Key, kv => kv.Value.GenerateSaveData());
            var dataBytes = SerializationUtility.SerializeValue(saveDatas, DataFormat.Binary);
            var byteString = StringUtility.StringFromByteArray(dataBytes);
            PlayerPrefs.SetString(Key, byteString);
        }

        public void Load()
        {
            if (!PlayerPrefs.HasKey(Key)) return;
            
            var byteString = PlayerPrefs.GetString(Key);
            var dataBytes = StringUtility.ByteArrayFromString(byteString);
            var saveDatas = SerializationUtility.DeserializeValue<Dictionary<int, TSaveData>>(dataBytes, DataFormat.Binary);
            LoadEntities(saveDatas);
        }

        private void LoadEntities(Dictionary<int, TSaveData> saveDatas)
        {
            foreach (var saveData in saveDatas.Values)
            {
                LoadEntity(saveData);
            }
        }

        private void LoadEntity(TSaveData entitySaveData)
        {
            var model = GetModel(entitySaveData.ModelName);
            var entity = Activator.CreateInstance(typeof(TSaveData), entitySaveData.Id, model) as TEntity;
            entity.Initialize(entitySaveData);
            Entities.Add(entity.Id, entity);
        }

        protected abstract TModel GetModel(string name);
    }
}