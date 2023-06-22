using Compori.Shopware.Entities;
using Compori.Shopware.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shopware.Repositories
{
    public class MediaRepositoryTests : BaseTest
    {
        protected MediaRepository Repository { get; set; }

        protected override void Setup()
        {
            base.Setup();
            this.Repository = new MediaRepository(this.TestContext.CreateClient());
        }

        [Fact()]
        public async Task TestReadAsync()
        {
            this.Setup();
            try
            {
                var items = await this.Repository.Read(new Types.Search { Limit = 25 }).ConfigureAwait(false);
                Assert.NotNull(items);
            }
            finally
            {
                this.Cleanup();
            }
        }

        [Fact()]
        public async Task TestUploadImageAsync()
        {
            this.Setup();
            var id = "";
            try
            {
                var fileName = "test-image.jpg";
                var fileInfo = new FileInfo(Path.Combine("data", fileName));
                var mimeType = MimeTypeHelper.GetMimeTypeByFileName(fileName);
                id = await this.Repository.Create(new Media
                {
                    Title = "My Plants",
                    Alt = "Nice Pictures of Plants",
                    MediaFolderId = "bd98aa186730462db38628427835dacb", // Test Folder
                });
                var media = await this.Repository.Read(id);
                Assert.NotNull(media);
                var extension = fileInfo.Extension.TrimStart('.');
                await this.Repository.UploadAsync(id, File.ReadAllBytes(fileInfo.FullName), MimeTypeHelper.GetMimeTypeByExtension(extension), "test picture", extension);
                media = await this.Repository.Read(id);
                Assert.NotNull(media);
                media.FileName = fileName;
                await this.Repository.RenameAsync(id, "test-bild");
                media = await this.Repository.Read(id);
                Assert.NotNull(media);
            }
            //catch (Exception ex)
            //{
            //}
            finally
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    await this.Repository.Delete(id);
                }
                this.Cleanup();
            }
        }

        [Fact()]
        public async Task TestFailureDuplicateFileNames()
        {
            this.Setup();
            var id1 = "";
            var id2 = "";
            try
            {
                var fileName = "test-image.jpg";
                var fileInfo = new FileInfo(Path.Combine("data", fileName));
                var mimeType = MimeTypeHelper.GetMimeTypeByFileName(fileName);
                id1 = await this.Repository.Create(new Media
                {
                    Title = "My Plants",
                    Alt = "Nice Pictures of Plants",
                    MediaFolderId = "bd98aa186730462db38628427835dacb", // Test Folder
                });
                var media1 = await this.Repository.Read(id1);
                Assert.NotNull(media1);
                id2 = await this.Repository.Create(new Media
                {
                    Title = "My Plants",
                    Alt = "Nice Pictures of Plants",
                    MediaFolderId = "bd98aa186730462db38628427835dacb", // Test Folder
                });
                var media2 = await this.Repository.Read(id2);
                Assert.NotNull(media2);

                var extension = fileInfo.Extension.TrimStart('.');
                await this.Repository.UploadAsync(id1, File.ReadAllBytes(fileInfo.FullName), MimeTypeHelper.GetMimeTypeByExtension(extension), "test picture", extension);
                var ex = await Assert.ThrowsAsync<ShopwareException>(() => this.Repository.UploadAsync(id2, File.ReadAllBytes(fileInfo.FullName), MimeTypeHelper.GetMimeTypeByExtension(extension), "test picture", extension));
                Assert.Equal("CONTENT__MEDIA_DUPLICATED_FILE_NAME", ex.Errors.First().Code);
            }
            //catch (Exception ex)
            //{
            //}
            finally
            {
                if (!string.IsNullOrWhiteSpace(id1))
                {
                    await this.Repository.Delete(id1);
                }
                if (!string.IsNullOrWhiteSpace(id2))
                {
                    await this.Repository.Delete(id2);
                }
                this.Cleanup();
            }
        }

        [Fact()]
        public async Task TestUploadPdfAsync()
        {
            this.Setup();
            var id = "";
            try
            {
                var fileName = "lorem-ipsum-dolor-sit-amet.pdf";
                var fileInfo = new FileInfo(Path.Combine("data", fileName));
                var mimeType = MimeTypeHelper.GetMimeTypeByFileName(fileName);
                id = await this.Repository.Create(new Media
                {
                    Title = "Text",
                    Alt = "Lot of non sense Text",
                    MediaFolderId = "bd98aa186730462db38628427835dacb", // Test Folder
                });
                var media = await this.Repository.Read(id);
                Assert.NotNull(media);
                var extension = fileInfo.Extension.TrimStart('.');
                await this.Repository.UploadAsync(id, File.ReadAllBytes(fileInfo.FullName), MimeTypeHelper.GetMimeTypeByExtension(extension), "test image", extension);
                media = await this.Repository.Read(id);
                Assert.NotNull(media);
                media.FileName = fileName;
                await this.Repository.RenameAsync(id, "test-text");
                media = await this.Repository.Read(id);
                Assert.NotNull(media);
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    await this.Repository.Delete(id);
                }
                this.Cleanup();
            }
        }
    }
}
