using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class DocumentRepository<TEntity> : EntityRepository<TEntity> where TEntity : Document
    {
        public DocumentRepository(Client client) : base(client)
        {
        }
    }

    public class DocumentRepository : DocumentRepository<Document>
    {
        public DocumentRepository(Client client) : base(client)
        {
        }
    }
}