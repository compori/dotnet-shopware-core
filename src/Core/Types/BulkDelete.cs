using Compori.Shopware.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Compori.Shopware.Types
{
    public class BulkDelete<TEntity> : BulkDelete where TEntity : IEntity
    {
        public BulkDelete(List<string> idList) : base(
            Attributes.ShopwareEntityAttribute.GetName<TEntity>(), 
            idList)
        { }

        public BulkDelete(string primaryField, string foreignField, Dictionary<string, List<string>> relations) : base(
            Attributes.ShopwareEntityAttribute.GetName<TEntity>(), 
            primaryField, foreignField, relations)
        { }

        public BulkDelete(List<Dictionary<string, string>> payload) : base(
            Attributes.ShopwareEntityAttribute.GetName<TEntity>(), 
            payload)
        { }
    }

    public class BulkDelete : Bulk
    {
        [JsonProperty(PropertyName = "payload")]
        public List<Dictionary<string, string>> Payload { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkDelete"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="idList">The identifier list.</param>
        public BulkDelete(string entity, List<string> idList) : base(entity)
        {
            this.Action = "delete";
            this.Payload = idList
                .Select(v => new Dictionary<string, string> { { "id", v } })
                .ToList();
        }

        public BulkDelete(string entity, string primaryField, string foreignField, Dictionary<string, List<string>> relations) : base(entity)
        {
            this.Action = "delete";
            this.Payload = new List<Dictionary<string, string>>();

            foreach (var item in relations)
            {
                if (item.Value.Count == 0)
                {
                    continue;
                }

                foreach (var relation in item.Value)
                {
                    this.Payload.Add(new Dictionary<string, string>{
                        { primaryField, item.Key },
                        { foreignField, relation }
                    });
                }
            }
        }

        public BulkDelete(string entity, List<Dictionary<string, string>> payload) : base(entity)
        {
            this.Action = "delete";
            this.Payload = payload;
        }
    }
}
