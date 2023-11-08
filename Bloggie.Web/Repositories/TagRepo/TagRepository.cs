using Azure;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories.TagRepo
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext _context;


        public TagRepository(BloggieDbContext bloggieDb)
        {
            _context = bloggieDb;
        }



        public async Task<Tag> AddAsync(Tag tag)
        {
            try
            {
                await _context.Tags.AddAsync(tag);
                await _context.SaveChangesAsync();

                return tag;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw new Exception();
            }

        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var foundTag = await _context.Tags.FindAsync(id);
            if (foundTag != null)
            {
                _context.Tags.Remove(foundTag);
                await _context.SaveChangesAsync();

                return foundTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var foundTag = await _context.Tags.FindAsync(tag.Id);

            if (foundTag != null)
            {
                foundTag.Name = tag.Name;
                foundTag.DisplayName = tag.DisplayName;

                await _context.SaveChangesAsync();

                return foundTag;
            }

            return null;


        }
    }
}
