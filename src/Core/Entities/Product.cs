using Compori.Shopware.Attributes;
using Compori.Shopware.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shopware.Entities
{
    public class Product : Product<Price, ProductVisibility, ProductManufacturer, Unit, ProductTag>
    {
    }

    /// <summary>
    /// Implements the shopware product entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/e9016e0b516af-product">Product on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "product", api: "product")]
    public class Product<TPrice, TProductVisibility, TProductManufacturer, TUnit, TTaggableWith> : Entity, ITaggable<TTaggableWith>
        where TPrice : Price
        where TProductVisibility : ProductVisibility
        where TProductManufacturer : ProductManufacturer
        where TUnit : Unit
        where TTaggableWith : ProductTag
    {
        [JsonProperty(PropertyName = "parentId", NullValueHandling = NullValueHandling.Include)]
        public string ParentId { get; set; }

        [JsonProperty(PropertyName = "productNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductNumber { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "stock", NullValueHandling = NullValueHandling.Ignore)]
        public long? Stock { get; set; }

        [JsonProperty(PropertyName = "restockTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? RestockTime { get; set; }

        [JsonProperty(PropertyName = "deliveryTimeId", NullValueHandling = NullValueHandling.Include)]
        public string DeliveryTimeId { get; set; }

        [JsonProperty(PropertyName = "ean", NullValueHandling = NullValueHandling.Ignore)]
        public string Ean { get; set; }

        [JsonProperty(PropertyName = "price", NullValueHandling = NullValueHandling.Ignore)]
        public List<TPrice> Price { get; set; }

        [JsonProperty(PropertyName = "visibilities", NullValueHandling = NullValueHandling.Ignore)]
        public List<TProductVisibility> Visibilities { get; set; }

        [JsonProperty(PropertyName = "purchasePrices", NullValueHandling = NullValueHandling.Ignore)]
        public List<TPrice> PurchasePrices { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty(PropertyName = "taxId", NullValueHandling = NullValueHandling.Ignore)]
        public string TaxId { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "categoryIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> CategoryIds { get; set; }

        [JsonProperty(PropertyName = "propertyIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> PropertyIds { get; set; }

        [JsonProperty(PropertyName = "optionIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> OptionIds { get; set; }

        [JsonProperty(PropertyName = "coverId", NullValueHandling = NullValueHandling.Ignore)]
        public string CoverId { get; set; }

        [JsonProperty(PropertyName = "unitId", NullValueHandling = NullValueHandling.Include)]
        public string UnitId { get; set; }

        [JsonProperty(PropertyName = "unit", NullValueHandling = NullValueHandling.Ignore)]
        public TUnit Unit { get; set; }

        [JsonProperty(PropertyName = "purchaseUnit", NullValueHandling = NullValueHandling.Include)]
        public double? PurchaseUnit { get; set; }

        [JsonProperty(PropertyName = "referenceUnit", NullValueHandling = NullValueHandling.Include)]
        public double? ReferenceUnit { get; set; }

        [JsonProperty(PropertyName = "manufacturerId", NullValueHandling = NullValueHandling.Include)]
        public string ManufacturerId { get; set; }

        [JsonProperty(PropertyName = "manufacturer", NullValueHandling = NullValueHandling.Ignore)]
        public TProductManufacturer Manufacturer { get; set; }

        [JsonProperty(PropertyName = "customSearchKeywords", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> CustomSearchKeywords = new List<string>();

        [JsonProperty(PropertyName = "packUnit", NullValueHandling = NullValueHandling.Ignore)]
        public string PackUnit { get; set; }

        [JsonProperty(PropertyName = "metaDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string MetaDescription { get; set; }

        [JsonProperty(PropertyName = "metaTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string MetaTitle { get; set; }

        [JsonProperty(PropertyName = "keywords", NullValueHandling = NullValueHandling.Ignore)]
        public string Keywords { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrWhiteSpace(this.ProductNumber))
            {
                result = this.ProductNumber;
                if (!string.IsNullOrWhiteSpace(this.Name))
                {
                    result += " " + this.Name;
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
