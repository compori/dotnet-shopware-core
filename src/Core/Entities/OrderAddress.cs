using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware order address entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/4a8bbf61f94cc-order-address">Order Address Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "order_address", api: "order-address")]
    public class OrderAddress : Entity
    {
        [JsonProperty(PropertyName = "countryId", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryId { get; set; }
        
        [JsonProperty(PropertyName = "countryStateId", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryStateId { get; set; }

        [JsonProperty(PropertyName = "salutationId", NullValueHandling = NullValueHandling.Ignore)]
        public string SalutationId { get; set; }

        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstname { get; set; }

        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string Lastname { get; set; }

        [JsonProperty(PropertyName = "street", NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "zipcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Zipcode { get; set; }

        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty(PropertyName = "company", NullValueHandling = NullValueHandling.Ignore)]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "department", NullValueHandling = NullValueHandling.Ignore)]
        public string Department { get; set; }

        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "vatId", NullValueHandling = NullValueHandling.Ignore)]
        public string VatId { get; set; }

        [JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "additionalAddressLine1", NullValueHandling = NullValueHandling.Ignore)]
        public string AdditionalAddressLine1 { get; set; }

        [JsonProperty(PropertyName = "additionalAddressLine2", NullValueHandling = NullValueHandling.Ignore)]
        public string AdditionalAddressLine2 { get; set; }
        
        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public Country Country { get; set; }

        [JsonProperty(PropertyName = "countryState", NullValueHandling = NullValueHandling.Ignore)]
        public CountryState CountryState { get; set; }

        [JsonProperty(PropertyName = "salutation", NullValueHandling = NullValueHandling.Ignore)]
        public Salutation Salutation { get; set; }
    }
}
