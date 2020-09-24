using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreServices.Models;
using CoreServices.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CoreServices.Repository
{
    public class PostRepository : IPostRepository
    {
        //first we get the instance of the dbContext using constructor dependancy injection. "Making or getting a connection to our db"//
        BlogDBContext db;
        public PostRepository(BlogDBContext _db)
        {
            db = _db;
        }
        public async Task<List<Category>> GetCategories()
        {
            if (db != null)
            {
                return await db.Category.ToListAsync();
            }
            return null;
        }
        public async Task<List<PostViewModel>> GetPosts()
        {//if db is not empty will return an async list postviewmodel object. This method gets all posts in the //
            if (db != null)
            {
                return await (from p in db.Post
                    from c in db.Category
                    where p.CategoryId == c.Id
                    select new PostViewModel
                    {
                        PostId = p.PostId,
                        Title = p.Title,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        CategoryName = c.Name,
                        CreatedDate = p.CreatedDate
                    }).ToListAsync();
            }
            return null;
        }
        public async Task<PostViewModel> GetPost(int? postId)
        {
            if (db != null)
            {
                return await (from p in db.Post
                    from c in db.Category
                    where p.PostId == postId
                    select new PostViewModel
                    {
                        PostId = p.PostId,
                        Title = p.Title,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        CategoryName = c.Name,
                        CreatedDate = p.CreatedDate
                    }).FirstOrDefaultAsync();
            }//Different then GetPosts this one only gets one post when the condition p.PostId == postId is met//
            return null;
        }
        public async Task<int> AddPost(Post post)
        {//if not null add a post object, save it and if it does not work it will return a 0//
            if (db != null)
            {
                await db.Post.AddAsync(post);
                await db.SaveChangesAsync();
                return post.PostId;
            }
            return 0;
        }
        public async Task<int> DeletePost(int? postId)
        {
            int result = 0;
            if (db != null)
            {
                //Find the post for specific post id
                var post = await db.Post.FirstOrDefaultAsync(x => x.PostId == postId);
                if (post != null)
                {
                    //Delete that post if it finds it
                    db.Post.Remove(post);
                    //Commit the transaction 
                    result = await db.SaveChangesAsync();
                }
                return result; //if post is null this well return in integer as a code error
            }
            return result;//if line 88 was skipped because the post was deleted then it wall return an integer also
        }
        public async Task UpdatePost(Post post)
        {
            if (db != null)
            {
                //Delete that post
                db.Post.Update(post);
                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }
}
