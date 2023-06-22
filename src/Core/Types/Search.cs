using Newtonsoft.Json;

namespace Compori.Shopware.Types
{
    public class Search
    {
        /// <summary>
        /// Gets or sets search result page.
        /// </summary>
        /// <value>Search result page.</value>
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per result page.
        /// </summary>
        /// <value>The Number of items per result page.</value>
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; } = 25;
        
        /// <summary>
        /// Gets or sets a list of filters to restrict the search result.
        /// </summary>
        /// <value>A list of filters to restrict the search result.</value>
        [JsonProperty(PropertyName = "filter", NullValueHandling = NullValueHandling.Ignore)]
        public Filter[] Filters { get; set; }

        /// <summary>
        /// Gets or sets the sorting in the search result.
        /// </summary>
        /// <value>The sorting in the search result.</value>
        [JsonProperty(PropertyName = "sort", NullValueHandling = NullValueHandling.Ignore)]
        public Sorting[] Sorting { get; set; }

        /// <summary>
        /// Gets or sets a list of filters that applied without affecting aggregations.
        /// </summary>
        /// <value>A list of filters that applied without affecting aggregations.</value>
        [JsonProperty(PropertyName = "post-filter", NullValueHandling = NullValueHandling.Ignore)]
        public Filter[] PostFilters { get; set; }

        /// <summary>
        /// Gets or sets aggregations on the search result.
        /// </summary>
        /// <value>Used to perform aggregations on the search result.</value>
        [JsonProperty(PropertyName = "aggregations", NullValueHandling = NullValueHandling.Ignore)]
        public Aggregation[] Aggregations { get; set; }

        /// <summary>
        /// Gets or sets groupings over certain fields.
        /// </summary>
        /// <value>The groupings over certain fields.</value>
        [JsonProperty(PropertyName = "grouping", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Grouping { get; set; }

        /// <summary>
        /// Gets or sets a parameter that can be used to define whether the total for the total number of hits should be determined for the search query.
        /// </summary>
        /// <value>A parameter that can be used to define whether the total for the total number of hits should be determined for the search query.</value>
        [JsonProperty(PropertyName = "total-count-mode")]
        public TotalCountMode TotalCountMode { get; set; } = TotalCountMode.Exact;

        /// <summary>
        /// Gets or sets a limit of the search to a list of Ids.
        /// </summary>
        /// <value>Limits the search to a list of Ids.</value>
        [JsonProperty(PropertyName = "ids", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Ids { get; set; }

        /// <summary>
        /// Gets or sets additional data to the standard data of an entity.
        /// </summary>
        /// <value>Allows to load additional data to the standard data of an entity.</value>
        [JsonProperty(PropertyName = "associations", NullValueHandling = NullValueHandling.Ignore)]
        public object Associations { get; set; }

        /// <summary>
        /// Gets or sets a value to restricts the output to the defined fields.
        /// </summary>
        /// <value>Restricts the output to the defined fields.</value>
        [JsonProperty(PropertyName = "includes", NullValueHandling = NullValueHandling.Ignore)]
        public object Includes { get; set; }

        /// <summary>
        /// Gets or sets a ranking for the search result.
        /// </summary>
        /// <value>A ranking for the search result.</value>
        [JsonProperty(PropertyName = "query", NullValueHandling = NullValueHandling.Ignore)]
        public object Query { get; set; }

        /// <summary>
        /// Gets or sets a text search on all records.
        /// </summary>
        /// <value>A text search on all records.</value>
        [JsonProperty(PropertyName = "term", NullValueHandling = NullValueHandling.Ignore)]
        public string Term { get; set; }
    }
}
