﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Wjw1.Libarary.Web
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPagedList
    {
        /// <summary>
        /// 记录总计
        /// </summary>
        int TotalCount { get; set; }

        /// <summary>
        /// 页号
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 页数总计
        /// </summary>
        int TotalPage { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>, IPagedList where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        public PagedList(IQueryable<T> source, int index, int pageSize)
        {

            var data = source.Skip((index - 1) * pageSize).Take(pageSize);
            TotalCount = source.Count();
            PageSize = pageSize;
            PageIndex = index;
            if (TotalCount / pageSize < PageIndex - 1)
            {
                index = 1;
                PageIndex = index;
            }

            TotalPage = (int)Math.Ceiling((double)TotalCount / PageSize);

            AddRange(data);
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }


        public int TotalPage { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class APIPagedList<T> :  IPagedList where T : class
    {

        public List<T> Data { get; set; } = new List<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        public APIPagedList(IQueryable<T> source, int index, int pageSize)
        {

            var data = source.Skip((index - 1) * pageSize).Take(pageSize);
            TotalCount = source.Count();
            PageSize = pageSize;
            PageIndex = index;
            if (TotalCount / pageSize < PageIndex - 1)
            {
                index = 1;
                PageIndex = index;
            }

            TotalPage = (int)Math.Ceiling((double)TotalCount / PageSize);

            Data.AddRange(data);
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }


        public int TotalPage { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class Pagination
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index = 1, int pageSize = 20) where T : class
        {
            return new PagedList<T>(source, index, pageSize);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static APIPagedList<T> ToAPIPagedList<T>(this IQueryable<T> source, int index = 1, int pageSize = 20) where T : class
        {
            return new APIPagedList<T>(source, index, pageSize);
        }
    }
}
