using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XP_PPS_Model;
using XP_PPS_BLL;
using System.Data.SQLite;


namespace ProductionPlanSystem.Controllers
{
    public class LogoSetController : Controller
    {       
        // GET: LogoSet
        public ActionResult Index()
        {
            SQLiteDataReader reader = FirmInfoBLL.Select();
            while (reader.Read())
            {
               Session["CompanyName"] = reader["CompanyName"].ToString();
               Session["LogoUrl"] = reader["LogoUrl"].ToString(); ;
            }
            ViewBag.info = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(string CompanyName)
        {
            HttpPostedFileBase filel = Request.Files["file1"];
            bool fileOk = false;
            if (filel.ContentLength!=0)//验证是否包含文件
            {
                //取得文件的扩展名,并转换成小写
                string fileExtension = Path.GetExtension(filel.FileName).ToLower();
                //验证上传文件是否图片格式
                fileOk = ValidateCode.IsImage(fileExtension);

                if (fileOk)
                {
                    //对上传文件的大小进行检测，限定文件最大不超过8M
                    if (filel.ContentLength < 8192000)
                    {
                        string filepath = "/Content/images/";
                        
                        if (Directory.Exists(Server.MapPath(filepath)) == false)//如果不存在就创建file文件夹
                        {
                            Directory.CreateDirectory(Server.MapPath(filepath));
                        }
                        string virpath = filepath +"Logo" + fileExtension;//这是存到服务器上的虚拟路径
                        string mappath = Server.MapPath(virpath);//转换成服务器上的物理路径
                        filel.SaveAs(mappath);//保存图片
                                              //显示图片
                                              //pic.ImageUrl = virpath;
                                              //清空提示

                        FirmInfo model = new FirmInfo();
                        model.FirmInfoID = 1;
                        model.CompanyName = CompanyName;
                        model.LogoUrl = virpath;
                        int rows= FirmInfoBLL.Update(model);
                        if (rows == 0)
                        {
                            ViewBag.info = "失败";
                        }
                        else
                        {
                            ViewBag.info = "成功";
                        }
                        return View();
                        //return PartialView("/LogoSet/Index");
                    }
                    else
                    {
                        ViewBag.info = "文件大小超出8M！请重新选择";
                        return View();

                    }
                }
                else
                {
                    ViewBag.info = "要上传的文件类型不对！请重新选择！";
                    return View();
                }
            }
            else
            {
                ViewBag.info = "请选择要上传的图片!";
                return View();
            }
        }
    }
}