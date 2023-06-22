using Compori.Shopware.Attributes;
using Newtonsoft.Json;

namespace Compori.Shopware.Entities
{
    /// <summary>
    /// Implements the shopware app payment method entity.
    /// </summary>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/ZG9jOjE0MzUyOTMz-entity-reference">Entity Reference on shopware.stoplight.io</seealso>
    /// <seealso href="https://shopware.stoplight.io/docs/admin-api/7b99aa4b721da-app-payment-method">App Payment Method Entity on shopware.stoplight.io</seealso>
    /// <seealso cref="Compori.Shopware.Entities.Entity" />
    [ShopwareEntity(name: "app_payment_method", api: "app-payment-method")]
    public class AppPaymentMethod : Entity
    {
        [JsonProperty(PropertyName = "appName", NullValueHandling = NullValueHandling.Ignore)]
        public string AppName { get; set; }

        [JsonProperty(PropertyName = "identifier", NullValueHandling = NullValueHandling.Ignore)]
        public string Identifier { get; set; }

        [JsonProperty(PropertyName = "payUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string PayUrl { get; set; }

        [JsonProperty(PropertyName = "finalizeUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string FinalizeUrl { get; set; }

        [JsonProperty(PropertyName = "validateUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string ValidateUrl { get; set; }

        [JsonProperty(PropertyName = "captureUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string CaptureUrl { get; set; }

        [JsonProperty(PropertyName = "refundUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string RefundUrl { get; set; }

        [JsonProperty(PropertyName = "appId", NullValueHandling = NullValueHandling.Ignore)]
        public string AppId { get; set; }

        [JsonProperty(PropertyName = "originalMediaId", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalMediaId { get; set; }

        [JsonProperty(PropertyName = "paymentMethodId", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentMethodId { get; set; }

    }
}
