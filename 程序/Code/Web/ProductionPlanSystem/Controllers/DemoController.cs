using Newtonsoft.Json;
using ProductionPlanSystem.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        #region 下拉框获取产品数据
        /// <summary>
        /// 下拉框获取产品数据
        /// </summary>
        /// <param name="search"></param>
        /// <param name="curPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult GetInfo(string search, string curPage, string pageSize)
        {
            DataTable pjinfo = SqlDbHelper.ExecuteDataTable($@"SELECT ProjectId,ProjectName FROM dbo.tb_Project WHERE   ProjectName LIKE '%{search}%'");
            string info = JsonConvert.SerializeObject(pjinfo);
            info = info.Replace("ProjectId", "id").Replace("ProjectName", "text");
            return Content(info);
        }
        #endregion

        #region 下拉框获取规格数据
        //下拉框获取规格数据
        public ActionResult GetSizeInfo(string search, string product)
        {
            //现获取产品对应的规格有哪些@ProductId='{product}',@ProductSizeName='{search}
            DataTable SizeInfo = SqlDbHelper.ExecuteDataTable($@"SELECT ProjectId, AboutProduct.ProductId,ProductName,AboutProduct.ProductSizeNo,ProductSizeName FROM dbo.AboutProduct  
INNER JOIN dbo.tb_Product ON AboutProduct.ProductId=tb_Product.ProductId 
INNER JOIN dbo.tb_ProductSize ON AboutProduct.ProductSizeNo=dbo.tb_ProductSize.ProductSizeNo 
WHERE ProjectId={product}");
            if (SizeInfo?.Rows.Count > 0)
            {
                string info = JsonConvert.SerializeObject(SizeInfo);
                //info = info.Replace("ProductSizeNo", "id").Replace("ProductSizeName", "text");
                return Content(info);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}