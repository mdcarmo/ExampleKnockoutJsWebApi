using MongoDB.Bson;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Contact> AllContacts();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Contact GetById(ObjectId id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Contact Add(Contact entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(Contact entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);
    }
}
