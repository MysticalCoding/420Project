﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Development.Models;

namespace Development.Controllers.Admin
{
    public class ProgramCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProgramCourses
        public ActionResult Index()
        {
            var programCourse = db.ProgramCourse.Include(p => p.Course).Include(p => p.Program);
            return View(programCourse.ToList());
        }

        // GET: ProgramCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramCourse programCourse = db.ProgramCourse.Find(id);
            if (programCourse == null)
            {
                return HttpNotFound();
            }
            return View(programCourse);
        }

        // GET: ProgramCourses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Title");
            ViewBag.ProgramId = new SelectList(db.Program, "ProgramId", "Name");
            return View();
        }

        // POST: ProgramCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramCourseId,ProgramId,CourseId,Semester")] ProgramCourse programCourse)
        {
            if (ModelState.IsValid)
            {
                db.ProgramCourse.Add(programCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Title", programCourse.CourseId);
            ViewBag.ProgramId = new SelectList(db.Program, "ProgramId", "Name", programCourse.ProgramId);
            return View(programCourse);
        }

        // GET: ProgramCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramCourse programCourse = db.ProgramCourse.Find(id);
            if (programCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Title", programCourse.CourseId);
            ViewBag.ProgramId = new SelectList(db.Program, "ProgramId", "Name", programCourse.ProgramId);
            return View(programCourse);
        }

        // POST: ProgramCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgramCourseId,ProgramId,CourseId,Semester")] ProgramCourse programCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Title", programCourse.CourseId);
            ViewBag.ProgramId = new SelectList(db.Program, "ProgramId", "Name", programCourse.ProgramId);
            return View(programCourse);
        }

        // GET: ProgramCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramCourse programCourse = db.ProgramCourse.Find(id);
            if (programCourse == null)
            {
                return HttpNotFound();
            }
            return View(programCourse);
        }

        // POST: ProgramCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramCourse programCourse = db.ProgramCourse.Find(id);
            db.ProgramCourse.Remove(programCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
