using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    public class ShippingMethod : ShippingMethod<ShippingMethodTag>
    {
    }

    /// <summary>
    /// Implements the shopware shipping method entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/7c0c849bf5536-shipping-method">Shipping Method on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "shipping_method", api: "shipping-method")]
    public class ShippingMethod<TTaggableWith> : Entity, ITaggable<TTaggableWith> where TTaggableWith : ShippingMethodTag
    {
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public long? Position { get; set; }

        [JsonProperty(PropertyName = "mediaId", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaId { get; set; }

        [JsonProperty(PropertyName = "deliveryTimeId", Required = Required.Always)]
        public string DeliveryTimeId { get; set; }

        [JsonProperty(PropertyName = "taxType", Required = Required.Always)]
        public string TaxType { get; set; }

        [JsonProperty(PropertyName = "taxId", NullValueHandling = NullValueHandling.Ignore)]
        public string TaxId { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "trackingUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingUrl { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                result = this.Name;
                if (!string.IsNullOrWhiteSpace(this.Id))
                {
                    result += " Id: " + this.Id;
                }
            }
            return result;
        }
    }
}
