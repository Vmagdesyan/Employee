using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeList.Models;
using System.Data.Entity;
using System.Data;
namespace EmployeeList.Controllers
{
    public class HomeController : Controller
    {
        EmployeeListContext db = new EmployeeListContext();

        public ActionResult Index()
        {
            var employees = db.Employees.Include(p => p.Position);
            return View(employees.ToList());
        }
        [HttpGet]
        public ActionResult Search()
        {

            var employees = db.Employees.Include(p => p.Position);
            return View(employees.ToList());

        }
        [HttpPost]
        public ActionResult Search(string searchFIO, string searchPosition)
        {
            //определим новые переменные, чтобы убрать один раз лишние пробелы
            string srchFIO = searchFIO.Trim(' ');
            string srchPosition = searchPosition.Trim(' ');
            //заведем переменную для вывода данных
            IEnumerable<Employee> employees = db.Employees.Include(p => p.Position);
            //в зависимости от того какие поля заполнены выполняем поиск
            if (srchFIO != "" & srchPosition != "")
                employees = db.Employees.Include(p => p.Position).Where(c => c.FIO == srchFIO).ToList().Where(c => c.Position.NamePost == srchPosition);
            else if (srchFIO != "" & srchPosition == "")
                employees = db.Employees.Where(c => c.FIO == srchFIO);
            else if (srchFIO == "" & srchPosition != "")
                employees = db.Employees.Where(c => c.Position.NamePost == srchPosition);
            return View(employees.ToList());

        }
        public ActionResult PositionDetails()
        {

            var position = db.Positions;
            return View(position);
        }
        [HttpGet]
        public ActionResult Create()
        {
            // Формируем список сотрдуников для передачи в представление
            SelectList positions = new SelectList(db.Positions, "Id", "NamePost");
            ViewBag.Positions = positions;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            //Добавляем сотрудника в таблицу
            db.Employees.Add(employee);
            db.SaveChanges();
            // перенаправляем на главную страницу
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд сотрудника
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                // Создаем список команд для передачи в представление
                SelectList positions = new SelectList(db.Positions, "Id", "NamePost", employee.PositionId);
                ViewBag.Positions = positions;
                return View(employee);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //То же самое для Positions 
        [HttpGet]
        public ActionResult CreatePosition()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePosition(Position position)
        {
            //Добавляем должность в таблицу
            db.Positions.Add(position);
            db.SaveChanges();
            // перенаправляем на страницу с должностями
            return RedirectToAction("PositionDetails");
        }
        [HttpGet]
        public ActionResult EditPosition(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд должность
            Position position = db.Positions.Find(id);
            if (position != null)
            {
                return View(position);
            }
            return RedirectToAction("PositionDetails");
        }
        [HttpPost]
        public ActionResult EditPosition(Position position)
        {
            db.Entry(position).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("PositionDetails");
        }

        public ActionResult DeletePosition(int id)
        {
            Position position = db.Positions.Find(id);
            if (position != null)
            {
                db.Positions.Remove(position);
                db.SaveChanges();
            }
            return RedirectToAction("PositionDetails");
        }
        
        public ActionResult getStatistics()
        {
            //количество сотрудников
            int count = db.Employees.Count();
            ViewData["countEmployee"] = count;
            ////получим вакансии и запишем
            var queryPos = db.Database.SqlQuery<Position>("SELECT * FROM Positions p WHERE p.id not in (select positionId from Employees)").ToList();
            //строка, которую будем выводить
            String vacancy = "";
            foreach (var position in queryPos)
                vacancy += position.NamePost + ", ";
            //удаляем лишнюю запятую
            if (vacancy != "")
                vacancy = vacancy.Substring(0, vacancy.Length - 2);
            else
                vacancy = "нет";
            ViewData["vacancyEmployee"] = vacancy;

            //средняя зарплата
            var querySalary = db.Database.SqlQuery<Position>("SELECT * from Employees e join Positions p on p.Id = e.PositionId");
            decimal AVGsalary = 0;
            //обходим все записи в таблице
            foreach (var position in querySalary)
                AVGsalary += position.Salary;
            if (count != 0)
            {
                AVGsalary = AVGsalary / count;
                ViewData["salaryEmployee"] = Math.Round(AVGsalary, 2).ToString();
            }
            return View();

        }
        
        
    }
}
