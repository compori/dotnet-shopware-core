namespace Compori.Shopware
{
    public class BaseTest
    {
        /// <summary>
        /// Gets or sets the test context.
        /// </summary>
        /// <value>The test context.</value>
        protected TestContext TestContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        public BaseTest() { }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected virtual void Setup() 
        { 
            this.TestContext = new TestContext();
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        protected virtual void Cleanup() 
        { 
        }
    }
}
