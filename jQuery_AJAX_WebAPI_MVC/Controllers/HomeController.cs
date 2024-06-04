using jQuery_AJAX_WebAPI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Windows.Forms;

namespace jQuery_AJAX_WebAPI_MVC.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// DisplayDateTime-->return View()
        /// 2024/05/28
        /// </summary>
        /// <returns></returns>
        public ActionResult DisplayDateTime()
        {
            return View();
        }

        /// <summary>
        /// 顯示員工檔
        /// 2024/05/23
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEmps()
        {
            DBDao dbdao = new DBDao();
            List<EMP> emps = dbdao.GetEMPs();
            ViewBag.emps = emps;
            return View();
        }

        /// <summary>
        /// 新增員工基本檔
        /// 2024/05/23
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEMP()
        {
            return View();
        }

        /// <summary>
        /// 新增員工基本檔
        /// 2024/05/23
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEMP(EMP emp)
        {
            DBDao dbdao = new DBDao();
            try
            {
                //新增一筆員工基本檔記錄
                dbdao.InsertEMP(emp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return RedirectToAction("GetEmps");
        }


        /// <summary>
        /// 編輯員工基本檔-顯示
        /// 2024/05/23
        /// </summary>
        /// <param name="emp_id"></param>
        /// <returns></returns>
        public ActionResult EditEMP(string emp_id)
        {
            DBDao dbdao = new DBDao();
            EMP emp = new EMP();
            try
            {
                //編輯一筆員工基本檔記錄
                emp = dbdao.GetEMPbyID(emp_id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return View(emp);
        }

        /// <summary>
        /// 修改一筆員工基本檔記錄
        /// 2024/05/23
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditEMP(EMP emp)
        {
            DBDao dbdao = new DBDao();
            try
            {
                //修改一筆員工基本檔記錄
                dbdao.UpdateEMP(emp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("GetEmps");
        }

        /// <summary>
        /// 以代號刪除一筆員工資料
        /// 2024/05/23
        /// </summary>
        /// <param name="emp_id"></param>
        /// <returns></returns>
        public ActionResult DeleteEMP(string emp_id)
        {
            DialogResult Result = MessageBox.Show("刪除這位員工嗎?", "Confirm Message", MessageBoxButtons.OKCancel);
            if (Result == DialogResult.OK)
            {

                DBDao dbdao = new DBDao();
                try
                {
                    //刪除一筆員工基本檔記錄
                    dbdao.DeleteEMPbyID(emp_id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
            else if (Result == DialogResult.Cancel)
            {
                //
            }

            return RedirectToAction("GetEmps");

        }


    
}
}
