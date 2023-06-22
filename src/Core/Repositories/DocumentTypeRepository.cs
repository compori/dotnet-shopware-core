using Compori.Shopware.Entities;

namespace Compori.Shopware.Repositories
{
    public class DocumentTypeRepository<TEntity> : EntityRepository<TEntity> where TEntity : DocumentType
    {
        public DocumentTypeRepository(Client client) : base(client)
        {
        }
    }

    public class DocumentTypeRepository : DocumentTypeRepository<DocumentType>
    {
        public DocumentTypeRepository(Client client) : base(client)
        {
        }
    }
}
