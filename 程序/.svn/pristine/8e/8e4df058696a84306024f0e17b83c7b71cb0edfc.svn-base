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
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            //获取项目的数量
            DataTable pjinfo = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'projectcount'");
            if (pjinfo?.Rows.Count > 0)
            {
                ViewBag.count = pjinfo.Rows[0].Field<int>("Count");
            }
            else
            {
                ViewBag.count = null;
            }
            return View();
        }

        #region 添加项目
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="ProjectName">项目名称</param>
        /// <returns>ok 添加成功 no 添加失败</returns>
        public ActionResult AddProject(string ProjectNo, string ProjectName, string Customer)
        {
            if (string.IsNullOrWhiteSpace(ProjectNo))
            {
                return Content("项目编号为空");
            }
            else if (string.IsNullOrWhiteSpace(ProjectName))
            {
                return Content("项目名称为空");
            }
            else if (string.IsNullOrWhiteSpace(Customer))
            {
                return Content("客户名称为空");
            }
            else
            {
                int add = SqlDbHelper.ExecuteNonQuery($"EXEC dbo.[proc_Project] @strType = 'addpj',@ProjectNo='{ProjectNo}',@ProjectName='{ProjectName}',@Customer='{Customer}'");
                if (add > 0)
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <param name="ProjectNo">项目编号</param>
        /// <param name="Customer">客户名称</param>
        /// <param name="pagecount">当前页码数</param>
        /// <param name="size">每页显示数</param>
        /// <returns>返回查询的DataTable到分部视图_Project</returns>
        public ActionResult SelectAll(string ProjectNo, string Customer, string pagecount, string size)
        {
            DataTable pjinfo = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'allpjinfo',@ProjectNo='{ProjectNo}',@Customer='{Customer}',@pagecount='{pagecount}',@size='{size}'");
            if (pjinfo?.Rows.Count > 0)
            {
                ViewBag.AllPjInfo = pjinfo;
            }
            else
            {
                ViewBag.AllPjInfo = null;
            }
            return PartialView("_Project", pjinfo);
        }
        #endregion

        #region 删除项目
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="ProjectId">项目Id字符串</param>
        /// <returns>ok 添加成功 no 添加失败</returns>
        public ActionResult DelProject(string ProjectId)
        {
            if (string.IsNullOrWhiteSpace(ProjectId))
            {
                return Content("项目Id为空");
            }
            string ProjectIds = ProjectId.Substring(0, ProjectId.Length - 1);
            int del = SqlDbHelper.ExecuteNonQuery($"EXEC dbo.[proc_Project] @strType = 'delpj',@ProjectIds='{ProjectIds}'");
            if (del > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 获取需要编辑的内容
        /// <summary>
        /// 获取需要编辑的内容
        /// </summary>
        /// <param name="PjId">项目Id</param>
        /// <returns>返回字符串</returns>
        public string GetUpdProjectInfo(string PjId)
        {
            if (string.IsNullOrWhiteSpace(PjId))
            {
                return "项目Id为空";
            }
            DataTable info = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'getpj',@ProjectId='{PjId}'");
            if (info?.Rows.Count > 0)
            {
                //int ProjectId = info.Rows[0].Field<int>("ProjectId");
                //string ProjectNo = info.Rows[0].Field<string>("ProjectNo");
                //string ProjectName = info.Rows[0].Field<string>("ProjectName");
                //string Customer = info.Rows[0].Field<string>("Customer");
                //return Content(ProjectId + "," + ProjectNo + "," + ProjectName + "," + Customer);
                string Json = JsonConvert.SerializeObject(info);
                return Json;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 编辑项目
        /// <summary>
        /// 编辑项目
        /// </summary>
        /// <param name="ProjectId">项目ID</param>
        /// <param name="ProjectNo">项目编号</param>
        /// <param name="ProjectName">项目名称</param>
        /// <param name="Customer">客户</param>
        /// <returns>ok代表成功no代表失败</returns>
        public ActionResult EditProjectInfo(string ProjectId, string ProjectNo, string ProjectName, string Customer)
        {
            if (string.IsNullOrWhiteSpace(ProjectNo))
            {
                return Content("项目编号为空");
            }
            else if (string.IsNullOrWhiteSpace(ProjectName))
            {
                return Content("项目名称为空");
            }
            else if (string.IsNullOrWhiteSpace(Customer))
            {
                return Content("客户名称为空");
            }
            else
            {
                int upd = SqlDbHelper.ExecuteNonQuery($"EXEC dbo.[proc_Project] @strType = 'updpj',@ProjectId={ProjectId},@ProjectNo='{ProjectNo}',@ProjectName='{ProjectName}',@Customer='{Customer}'");
                if (upd > 0)
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
        }
        #endregion

        #region 下拉框获取产品数据
        /// <summary>
        /// 下拉框获取产品数据
        /// </summary>
        /// <param name="search">查询的产品名称</param>
        /// <param name="curPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult GetInfo(string search, string curPage, string pageSize)
        {
            DataTable pjinfo = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Size] @strType = 'getproduct',@ProductName='{search}'");
            string info = JsonConvert.SerializeObject(pjinfo);
            info = info.Replace("ProductId", "id").Replace("ProductName", "text");
            return Content(info);
        }
        #endregion

        #region 下拉框获取规格数据
        /// <summary>
        /// 下拉框获取规格数据
        /// </summary>
        /// <param name="search">查询的规格名称</param>
        /// <param name="product"></param>
        /// <returns>返回查询的数据</returns>
        public ActionResult GetSizeInfo(string search, string product)
        {
            //现获取产品对应的规格有哪些
            DataTable SizeInfo = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Size] @strType = 'getSelSizeInfo',@ProductId='{product}',@ProductSizeName='{search}'");
            if (SizeInfo?.Rows.Count > 0)
            {
                string info = JsonConvert.SerializeObject(SizeInfo);
                info = info.Replace("ProductSizeNo", "id").Replace("ProductSizeName", "text");
                return Content(info);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 关联产品
        /// <summary>
        /// 关联产品
        /// </summary>
        /// <param name="ProjectId">项目Id</param>
        /// <param name="ProductId">产品Id</param>
        /// <param name="ProductSizeNo">产品编号</param>
        /// <returns>ok成功 no失败</returns>
        public ActionResult AddAboutProduct(string ProjectId, string ProductId, string ProductSizeNo)
        {
            DataTable isin = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_AboutProduct] @strType = 'isin',@ProjectId='{ProjectId}',@ProductId='{ProductId}',@ProductSizeNo='{ProductSizeNo}'");
            if (isin.Rows.Count > 0)
            {
                return Content("此项目中已存在此产品");
            }
            else
            {
                int add = SqlDbHelper.ExecuteNonQuery($"EXEC dbo.[proc_AboutProduct] @strType = 'addAboutProduct',@ProjectId='{ProjectId}',@ProductId='{ProductId}',@ProductSizeNo='{ProductSizeNo}'");
                if (add > 0)
                {
                 
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
        }
        #endregion

        #region 通过项目Id获取本项目关联的产品信息
        /// <summary>
        /// 通过项目Id获取本项目关联的产品信息
        /// </summary>
        /// <param name="ProjectId">项目Id</param>
        /// <returns>返回获取到的产品及规格数据</returns>
        public ActionResult GetAboutProduct(string ProjectId)
        {
            DataTable aboutinfo = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_AboutProduct] @strType = 'getAboutProduct',@ProjectId='{ProjectId}'");
            if (aboutinfo?.Rows.Count > 0)
            {
                string info = JsonConvert.SerializeObject(aboutinfo);
                return Content(info);
            }
            else
            {
                return null;
            }

        }

        #endregion

        #region 删除关联
        /// <summary>
        /// 删除关联
        /// </summary>
        /// <param name="AboutId">关联Id</param>
        /// <returns>ok成功 no失败</returns>
        public ActionResult DelAbout(string AboutId)
        {
            if (string.IsNullOrWhiteSpace(AboutId))
            {
                return Content("关联Id为空");
            }
            int del = SqlDbHelper.ExecuteNonQuery($"EXEC dbo.[proc_AboutProduct] @strType = 'delabout',@AboutId='{AboutId}'");
            if (del > 0)
            {            
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        } 
        #endregion
    }
}