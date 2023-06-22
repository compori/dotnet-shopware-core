using Compori.Shopware.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shopware.Types
{
    public class BulkUpsert<TEntity> : Bulk where TEntity : IEntity
    {
        [JsonProperty(PropertyName = "payload")]
        public List<TEntity> Payload { get; set; }

        public BulkUpsert(List<TEntity> payload) : this(
            Attributes.ShopwareEntityAttribute.GetName<TEntity>(),
            payload)
        {
        }

        public BulkUpsert(string entity, List<TEntity> payload) : base(entity)
        {
            this.Action = "upsert";
            this.Payload = payload;
        }
    }
}
