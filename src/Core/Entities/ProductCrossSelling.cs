using Compori.Shopware.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shopware.Entities
{
    public class ProductCrossSelling : ProductCrossSelling<ProductCrossSellingAssignedProduct>
    {
    }

    /// <summary>
    /// Implements the shopware product cross selling entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/d0ecd30fc225b-product-cross-selling">Product Cross Selling on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "product_cross_selling", api: "product-cross-selling")]
    public class ProductCrossSelling<TProductCrossSellingAssignedProduct> : Entity
        where TProductCrossSellingAssignedProduct : ProductCrossSellingAssignedProduct
    {
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "position", Required = Required.Always)]
        public long Position { get; set; }

        [JsonProperty(PropertyName = "sortBy", NullValueHandling = NullValueHandling.Ignore)]
        public string SortBy { get; set; }

        [JsonProperty(PropertyName = "sortDirection", NullValueHandling = NullValueHandling.Ignore)]
        public string SortDirection { get; set; }

        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty(PropertyName = "limit", NullValueHandling = NullValueHandling.Ignore)]
        public long? Limit { get; set; }

        [JsonProperty(PropertyName = "productId", Required = Required.Always)]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "productVersionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductVersionId { get; set; }

        [JsonProperty(PropertyName = "productStreamId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductStreamId { get; set; }

        [JsonProperty(PropertyName = "assignedProducts", NullValueHandling = NullValueHandling.Ignore)]
        public List<TProductCrossSellingAssignedProduct> AssignedProducts { get; set; }

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
                if (!string.IsNullOrWhiteSpace(this.ProductId))
                {
                    result += " ProductId: " + this.ProductId;
                }
                if (!string.IsNullOrWhiteSpace(this.Id))
                {
                    result += " Id: " + this.Id;
                }
            }
            return result;
        }
    }
}
