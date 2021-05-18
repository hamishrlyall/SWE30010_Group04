using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using SWE30010_Group04_WebApplication.Models;

namespace SWE30010_Group04_WebApplication.Controllers
{
   [AttributeUsage( AttributeTargets.Method, AllowMultiple = false, Inherited = true )]
   public class MultiButtonAttribute : ActionNameSelectorAttribute
   {
      public string MatchFormKey { get; set; }
      public string MatchFormValue { get; set; }

      public override bool IsValidName( ControllerContext controllerContext, string actionName, MethodInfo methodInfo )
      {
         return controllerContext.HttpContext.Request[ MatchFormKey ] != null &&
            controllerContext.HttpContext.Request[ MatchFormKey ] == MatchFormValue;
      }
   }

   public class SalesController : Controller
   {
      private SWE30010_Group04_DBEntities4 db = new SWE30010_Group04_DBEntities4();
      Sale NewSale { get; set; }

      // GET: Sales
      public ActionResult Index()
      {
         var sales = db.Sales.Include(s => s.Employee);
         return View(sales.ToList());
      }

      // GET: Sales/Details/5
      public ActionResult Details(int? id)
      {
         if (id == null)
         {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Sale sale = db.Sales.Find(id);
         if (sale == null)
         {
               return HttpNotFound();
         }
         return View(sale);
      }

      // GET: Sales/Create
      public ActionResult Create()
      {
         ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Username");
         ViewBag.ProductId = new SelectList( db.Products, "ProductId", "ProductName" );
         //int saleId = 0;
         //if( db.Sales.Any( ) )
         //   saleId = db.Sales.Last( ).SaleId;

         //db.Sale = new Sale( ){
         //   SaleId = saleId + 1,
         //};
         //NewSale = db.Sale;

         //var products = db.Products.ToList( );

         return View( /*db*/ new Sale( ) );
      }

      [HttpPost]
      [MultiButton(MatchFormKey = "action", MatchFormValue = "Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult Delete( int _SaleItemId )
      {
         return Redirect( Request.UrlReferrer.ToString( ) );
      }


      [HttpPost]
      [MultiButton( MatchFormKey = "action", MatchFormValue = "Add" )]
      [ValidateAntiForgeryToken]
      public ActionResult Add( Sale _Sale, Product _Product )
      {
         ViewBag.EmployeeId = new SelectList( db.Employees, "EmployeeId", "Username" );
         ViewBag.ProductId = new SelectList( db.Products, "ProductId", "ProductName" );

         try
         {
            List<Stock> StockList = new List<Stock>( );
            if( _Sale.Stocks.Count > 0 )
            {
               foreach( var stockId in _Sale.Stocks )
               {
                  var stock = db.Stocks.Find( stockId.StockId );
                  stock.SaleId = _Sale.SaleId;
                  _Sale.Total += stock.Product.Price;
                  StockList.Add( stock );
               }
               _Sale.Stocks = StockList;
            }

            var newStockItem = new Stock( );
            var stockItems = db.Stocks.Where( s => s.ProductId == _Product.ProductId );
            foreach( var stockItem in stockItems )
            {
               if( _Sale.Stocks.Any( ) )
               {
                  if( !ListsContainAMatchingValue( stockItem.StockId, _Sale.Stocks.Where( s => s.ProductId == _Product.ProductId ) ) )
                     newStockItem = stockItem;
               }
               else
                  newStockItem = db.Stocks.Where( s => s.ProductId == _Product.ProductId ).FirstOrDefault( );
            }

            if( newStockItem.StockId == 0 )
               throw new DataException( "No Stock available for this Product" );
            else
            {
               newStockItem.SaleId = _Sale.SaleId;
               _Sale.Stocks.Add( newStockItem );
               _Sale.Total += newStockItem.Product.Price;
            }
         }
         catch( DataException _Ex )
         {
            ModelState.AddModelError( "", _Ex.Message );
         }
         return View( _Sale );
      }

      private bool ListsContainAMatchingValue( int StockId,  IEnumerable<Stock> CurrentStockList )
      {
         return CurrentStockList.Any( s => s.StockId == StockId );
      }

      // POST: Sales/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [MultiButton( MatchFormKey = "action", MatchFormValue = "Create" )]
      [ValidateAntiForgeryToken]
      public ActionResult Create( Sale sale )
      {
         if( ModelState.IsValid )
         {
            var newSale = new Sale( );
            newSale.Employee = db.Employees.Find( sale.EmployeeId );
            newSale.EmployeeId = sale.EmployeeId;
            newSale.Total = sale.Total;
            foreach( var stock in sale.Stocks )
            {
               var newSaleItem = db.Stocks.Find( stock.StockId );
               newSale.Stocks.Add( newSaleItem );
               newSale.Total += newSaleItem.Product.Price;
            }

            db.Sale = newSale;
            db.Sales.Add( newSale );
            db.SaveChanges( );
            return RedirectToAction( "Index" );
         }

         ViewBag.EmployeeId = new SelectList( db.Employees, "EmployeeId", "Username", sale.EmployeeId );
         return View( sale );
      }

      // GET: Sales/Edit/5
      //public ActionResult Edit(int? id)
      //{
      //   if (id == null)
      //   {
      //         return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      //   }
      //   Sale sale = db.Sales.Find(id);
      //   if (sale == null)
      //   {
      //         return HttpNotFound();
      //   }
      //   ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Username", sale.EmployeeId);
      //   return View(sale);
      //}

      // POST: Sales/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      //[HttpPost]
      //[ValidateAntiForgeryToken]
      //public ActionResult Edit([Bind(Include = "SaleId,EmployeeId,Total")] Sale sale)
      //{
      //   if (ModelState.IsValid)
      //   {
      //         db.Entry(sale).State = EntityState.Modified;
      //         db.SaveChanges();
      //         return RedirectToAction("Index");
      //   }
      //   ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Username", sale.EmployeeId);
      //   return View(sale);
      //}

      // GET: Sales/Delete/5
      public ActionResult Delete(int? id)
      {
         if (id == null)
         {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Sale sale = db.Sales.Find(id);
         if (sale == null)
         {
               return HttpNotFound();
         }
         return View(sale);
      }

      // POST: Sales/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         Sale sale = db.Sales.Find(id);
         db.Sales.Remove(sale);
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
