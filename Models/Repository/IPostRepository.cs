using CoreServices.Models;
using CoreServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Repository 
{ public interface IPostRepository
    {
        /*Defines our methods for a different purpose. GetCategories will get the list of available category, GetPosts will get the list of available posts, GetPost will get the individual post for specific Post Id, AddPost will add new post detail, DeletePost will delete the individual post based of Post Id and last UpdatePost will update the existing post. As we are returning Task-specific data, it means, data will return asynchronously.*/
        Task<List<Category>> GetCategories();
        Task<List<PostViewModel>> GetPosts();
        Task<PostViewModel> GetPost(int? postId);
        Task<int> AddPost(Post post);
        Task<int> DeletePost(int? postId);
        Task UpdatePost(Post post);
    }
}
