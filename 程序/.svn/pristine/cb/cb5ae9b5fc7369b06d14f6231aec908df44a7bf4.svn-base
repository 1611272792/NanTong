using Newtonsoft.Json;
using ProductionPlanSystem.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ////获取产品信息
            //DataTable info = SqlDbHelper.ExecuteDataTable("SELECT * FROM dbo.tb_Product");
            //if (info?.Rows.Count > 0)
            //{
            //    for (int i = 0; i < info.Rows.Count; i++)
            //    {
            //        info.Rows[i]["ProductSizeNos"] = getFieldsName(info.Rows[i]["ProductSizeNos"].ToString());

            //    }
            //    ViewBag.pinfo = info;
            //}
            //else
            //{
            //    ViewBag.pinfo = null;
            //}
            //获取产品信息
            DataTable info = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'sizecount'");
            if (info?.Rows.Count > 0)
            {
                ViewBag.count = info.Rows[0].Field<int?>("Count");
            }
            else
            {
                ViewBag.count = 0;
            }
            return View();
        }

        /// <summary>
        /// 查询产品信息
        /// </summary>
        /// <param name="ProductName">产品名称</param>
        /// <param name="pagecount">当前页码数</param>
        /// <param name="size">每页显示数</param>
        /// <returns>返回DataTable到_Product分部视图</returns>
        public ActionResult SelectAll(string ProductName,string pagecount,string size)
        {
            DataTable pdinfo = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'selectpdinfo',@ProductName='{ProductName}',@pagecount='{pagecount}',@size='{size}'");
            if (pdinfo?.Rows.Count > 0)
            {
                ViewBag.pinfo = pdinfo;
            }
            else
            {
                ViewBag.pinfo = null;
            }
            return PartialView("_Product", pdinfo);
        }

        #region 添加产品
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="ProductName">产品名称</param>
        /// <returns>返回执行结果 ok代表成功否则返回对应的提示</returns>
        public ActionResult AddProduct(string ProductName)
        {
            #region 验证
            if (string.IsNullOrWhiteSpace(ProductName.Trim()))
            {
                return Content("产品名不能为空！");
            }
            #endregion
            DataTable isin = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'pdisin',@ProductName='{ProductName}'");
            if (isin.Rows.Count > 0)
            {
                return Content("此产品名称已存在");
            }
            else
            {
                int info = SqlDbHelper.ExecuteNonQuery($"EXEC dbo.[proc_Project] @strType = 'addpd',@ProductName='{ProductName}'");
                if (info > 0)
                {
                    return Content("ok");
                }
                else
                {
                    return Content("添加失败");
                }
            }
        }
        #endregion

        #region 删除产品
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="ProductIds">产品id</param>
        /// <returns>ok代表成功 no代表失败</returns>
        public ActionResult DelProduct(string ProductIds)
        {
            if (string.IsNullOrWhiteSpace(ProductIds))
            {
                return Content("产品id为空");
            }
            ProductIds = ProductIds.Substring(0, ProductIds.Length - 1);
            int delcstate = SqlDbHelper.ExecuteNonQuery($"EXEC dbo.[proc_Project] @strType = 'delpd',@ProductIds='{ProductIds}'");
            if (delcstate > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 获取需要编辑的产品信息
        /// <summary>
        ///  获取需要编辑的产品信息
        /// </summary>
        /// <param name="PId">产品Id</param>
        /// <returns>返回由@符号拼接的字符串</returns>
        public string GetUpdProductInfo(int PId)
        {
            if (string.IsNullOrWhiteSpace(PId.ToString()))
            {
                return "Id不能为空";
            }

            DataTable ProductInfo = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'getpd',@ProductId='{PId}'");
            if (ProductInfo?.Rows.Count > 0)
            {
                //string ProductId = ProductInfo.Rows[0]["ProductId"].ToString();
                //string ProductName = ProductInfo.Rows[0]["ProductName"].ToString();
                //return Content(ProductId + "@" + ProductName);
               string Json = JsonConvert.SerializeObject(ProductInfo);
                return Json;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 编辑产品
        /// <summary>
        /// 编辑产品
        /// </summary>
        /// <param name="ProductId">产品Id</param>
        /// <param name="ProductName">产品名称</param>
        /// <returns></returns>
        public ActionResult EditProduct(string ProductId, string ProductName)
        {
            #region 验证
            if (string.IsNullOrWhiteSpace(ProductId.Trim()))
            {
                return Content("产品Id不能为空");
            }
            #endregion
            DataTable isin = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'pdisin2',@ProductName='{ProductName}',@ProductId='{ProductId}'");
            if (isin.Rows.Count > 0)
            {
                return Content("此产品名称已存在");
            }
            else
            {
                int info = SqlDbHelper.ExecuteNonQuery($"EXEC dbo.[proc_Project] @strType = 'updpd',@ProductId='{ProductId}',@ProductName='{ProductName}'");
                if (info > 0)
                {
                    return Content("ok");
                }
                else
                {
                    return Content("编辑失败");
                }
            }
        }
        #endregion
    }
}