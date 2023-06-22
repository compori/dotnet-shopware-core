using Compori.Shopware.Attributes;
using Compori.Shopware.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware product price entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/be992da45e91c-product-price">Product Price on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "product_price", api: "product-price")]
    public class ProductPrice : Entity
    {
        [JsonProperty(PropertyName = "productId", Required = Required.Always)]
        public string ProductId  { get; set; }

        [JsonProperty(PropertyName = "productVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductVersionId  { get; set; }

        [JsonProperty(PropertyName = "quantityStart", Required = Required.Always)]
        public long QuantityStart { get; set; }

        [JsonProperty(PropertyName = "quantityEnd", NullValueHandling = NullValueHandling.Ignore)]
        public long? QuantityEnd { get; set; }

        [JsonProperty(PropertyName = "ruleId", NullValueHandling = NullValueHandling.Ignore)]
        public string RuleId  { get; set; }

        [JsonProperty(PropertyName = "price", NullValueHandling = NullValueHandling.Ignore)]
        public List<Price> Price { get; set; }        
    }
}
