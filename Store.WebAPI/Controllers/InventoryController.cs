using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Store.RepositoryLayer;



namespace Store.WebAPI.Controllers
{
    public class InventoryController : ApiController
    {
        // GET: Inventory
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Inventory/Index")]
        public IHttpActionResult Index()
        {

            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            InventoryDBRepository dBRepository = new InventoryDBRepository(connectionString);
            List<Inventory> inventoryList = dBRepository.GetAllInventories();
            if (inventoryList.Count == 0)
            {
                ResponseMessage(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                    Content = new StringContent("No data")
                });
            }
            return Ok();
            
        }
        // GET: Inventory/id
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Inventory/SearchById")]
        public IHttpActionResult SearchById(Guid inventoryId)
        {

            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            InventoryDBRepository dBRepository = new InventoryDBRepository(connectionString);
            Inventory inventory = dBRepository.GetInventoriesById(inventoryId);
            if(inventory == null)
            {
                ResponseMessage(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                    Content = new StringContent("No data found")
                });
            }
            return Ok();
        }
        // GET: Inventory/Name
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Inventory/SearchByName")]
        public IHttpActionResult SearchByName(String name)
        {

            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            InventoryDBRepository dBRepository = new InventoryDBRepository(connectionString);
            List<Inventory> inventoryList = dBRepository.GetByName(name);
            if (inventoryList.Count == 0)
            {
                ResponseMessage(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                    Content = new StringContent("No data found")
                });
            }
            return Ok();
        }

        [System.Web.Http.Route("api/Inventory/AddInventory")]
        public IHttpActionResult AddInventory(Inventory inventory)
        {

            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            InventoryDBRepository dBRepository = new InventoryDBRepository(connectionString);
            DbActionResult actionResult = dBRepository.AddInventory(inventory);
            if (actionResult.Success)
            {
                return Ok();
            }
            else
            {
                return ResponseMessage(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(actionResult.Message)
                });
            }
        }
        [System.Web.Http.Route("api/Inventory/UpdateInventory")]
        public IHttpActionResult UpdateInventory(Inventory inventory)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            InventoryDBRepository dBRepository = new InventoryDBRepository(connectionString);
            DbActionResult actionResult = dBRepository.UpdateInventory(inventory);
            if (actionResult.Success)
            {
                return Ok();
            }
            else
            {
                return ResponseMessage(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(actionResult.Message)
                });
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Inventory/DeleteInventory")]
        public IHttpActionResult DeleteInventory(Inventory inventory)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            InventoryDBRepository dBRepository = new InventoryDBRepository(connectionString);
            DbActionResult actionResult = dBRepository.DeleteInventory(inventory.InventoryId);
            if (actionResult.Success)
            {
                return Ok();
            }
            else
            {
                return ResponseMessage(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(actionResult.Message)
                });
            }
        }
        public IEnumerable<Inventory> AddMultipleInventory()
        {
            List<string> brandName = new List<string>() {"A. T. Cross Company",
"Active Pen",
"Alfred Dunhill",
"Anoto",
"C. Howard Hunt",
"Calligraphy pen",
"Four Treasures of the Study",
"Camlin",
"Caran d'Ache",
"Carter's Ink Company",
"Cello",
"Cerruti",
"Classmate Stationery",
"Compact disc pen",
"Conway Stewart",
"Copic",
"Counterfeit banknote detection pen",
"Counter pen",
"Crayola",
"Cretacolor",
"Curtis Australia",
"D. Leonardt & Co.",
"Decoder pen",
"Invisible ink",
"Demonstrator pen",
"Derwent Cumberland Pencil Company",
"Digital pen",
"Dip pen",
"Displays2Go",
"edding AG",
"Erasermate",
"Esselte",
"Esterbrook",
"Expo dry erase markers",
"F. Weber & Company, Inc.",
"Faber-Castell",
"Flex nibs",
"Flo-Master",
"Flux pen",
"Fountain pen",
"George Safford Parker",
"Lewis Waterman",
"Noodler's Ink",
"Walter A. Sheaffer",
"Frost & Adams",
"Fudepen",
"Macniven and Cameron",
"Marker pen – also known as a felt-tip pen",
"Connector pen",
"Mean Streak",
"Wet wipe marker",
"Meisterstück",
"Melody",
"Monami",
"Montblanc",
"Montegrappa",
"Muji",
"Paint marker",
"Paper Mate",
"Paper Mate PhD Multi",
"Parker Pen Company",
"Parker Jointless",
"Parker Duofold",
"Parker 100",
"Parker 51",
"Parker Jotter",
"Parker Vacumatic",
"Parker Vector",
"Quink",
"Pelikan",
"Pen Room",
"Esselte",
"Esterbrook",
"Expo dry erase markers",
"F. Weber & Company, Inc.",
"Faber-Castell",
"Flex nibs",
"PenAgain",
"Pentel",
"Permanent marker",
"Perry & Co.",
"Pilot",
"Porous point pen",
"Portok",
"Prismacolor",
"Project Eden",
"Rastrum",
"Reed pen",
"Kalamos",
"Qalam",
"Reeves and Sons",
"Retipping",
"Reynolds International Pen Company",
"Rollerball pen",
"Rotring",
"Ruling pen",
"S. T. Dupont",
"Sakura",
"Sanford L.P.",
"Sharpie",
"Schwan-Stabilo",
"The Shanghai Hero Pen Company",
"Sheaffer",
"Sheaffer Prelude",
"Project Eden",
"Rastrum",
"Reed pen",
"Kalamos",
"Qalam",
"Reeves and Sons",
"Retipping",
"Reynolds International Pen Company",
"Rollerball pen",
"Skin pens",
"Société Bic",
"Space Pen",
"Paul C. Fisher",
"Staedtler",
"Stipula",
"Stylus",
"Technical pen",
"Tibaldi",
"Tombow",
"Wacom",
"Waterman pens",
"Waterman Hémisphère",
"Waterman Philéas",
"Winsor & Newton",
"Skin pens",
"Société Bic",
"Space Pen",
"Paul C. Fisher",
"Staedtler",
"Stipula",
"Stylus",
"Technical pen",
"Tibaldi",
"Tombow",
"Wacom",
"Waterman pens"
 };
            List<string> itemName = new List<string>() {
            "Binder clip ",
"Black n' Red",
"Brass fastener",
"Bulldog clip",
"Business card",
"Black Astrum",
"HCard",
"Internet business card",
"Carbon paper ",
"Cartridge paper",
"Chalkboard eraser",
"Clipboard",
"Colour pencil",
"Compliments slip",
"Continuous stationery",
"Correction fluid",
"Correction paper",
"Correction tape",
"Crane & Co.",
"Derwent Cumberland Pencil Company",
"Drawing pin",
"Dymotape",
"E-card",
"Embossing",
"Embossing tape",
"Engraving",
"Envelope",
"Eraser",
"Esselte",
"File folder",
"Foolscap folio",
"Greeting card",
"American Greetings",
"Archies Ltd",
"Baby announcement",
"Cardmaking",
"Cards ",
"Carlton Cards",
"Celebrations Group",
"Christmas card",
"Cookie bouquet",
"CSS Industries",
"David R. Beittel",
"Forever Friends",
"Get-well card",
"The Greeting Card Association ",
"Hallmark Cards",
"Hallmark Business Connections",
"Hallmark holiday",
"Holiday greetings",
"Hoops&Yoyo",
"Irving I. Stone",
"Marcus Ward & Co",
"Moonpig",
"Naughty cards",
"New Year Card",
"Nobleworks",
"Recycled Paper Greetings",
"Studio cards",
"Touchnote",
"Twisted Whiskers",
"Uncooked",
"Video ecard",
"Highlighter ",
"Hipster PDA",
"Index card",
"ISO 216",
"ISO 217",
"Copic",
"Genkō yōshi",
"Hobonichi Techo",
"Kuretake ",
"Namiki",
"Pentel",
"Pilot ",
"Shitajiki",
"Tombow",
"Triart Design Marker",
"Tsujiura",
"Uni-ball",
"Knife ",
"Needle card",
"New Zealand standard for school stationery",
"Notebook",
"Big Chief tablet",
"CERF Collaborative Framework",
"Classmate Stationery",
"Composition book",
"Diary",
"Electronic lab notebook",
"Inventor's notebook",
"Lab notebook",
"Open notebook science",
"Personal organizer",
"Police notebook",
"Ring binder",
"Sketchbook",
"Paper",
"Paper clip",
"Paper cutter",
"Paper Mate",
"Paper size",
"Pee Chee folder",
"Pen",
"Pencil",
"Pencil Case",
"Post-it note",
"Some Royal Mail rubber bands",
"Postal stationery",
"Aerogram",
"Corner card",
"Cut square",
"Cut to shape",
"Formular stationery",
"Higgins & Gage World Postal Stationery Catalog",
"Imprinted stamp",
"International reply coupon",
"Letter sheet",
"Lettercard",
"Mulready stationery",
"Postal card",
"Postal Order",
"Postal Stationery Society",
"Postcard",
"Sherborn Collection",
"Stamped envelope",
"United Postal Stationery Society",
"Wrapper",
"Presentation folder",
"Pressure-sensitive adhesive",
"Pressure-sensitive tape",
"Seal",
"Smythson",
"Spindle",
"Springback binder",
"Staple",
"Stapler",
"Sticker",
"teNeues",
"Thermographic printing",
"Tickler file",
"Tipp-Ex",
"Trade card",
"Trapper Keeper",
"Treasury tag",
"Visiting card",
"Watermark",
"Wite-Out",
"Worksheet",
"Water colour"
};
            List<string> productVolume = new List<string>() { "1 each", "Dozen", "20 pack", "50 pack", "100 pack", "Bundle", "Carton", "Box", "500 pack", "1000 pack" };
            List<string> products = new List<string>();
            List<int> quanities = new List<int>();
            List<Inventory> inventories = new List<Inventory>();
            List<Guid> inventoryId = new List<Guid>();
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();

             
            foreach (String brand in brandName.Distinct().Take(100))
            {
                foreach (String item in itemName.Distinct().Take(100))
                {
                    foreach (String volume in productVolume)
                    {
                        String productName = $"{brand} {item} {volume}";
                        products.Add(productName);
                    }
                }

            }
            int counter = 0;
            while (counter < products.Count)
            {
                quanities.Add((new Random().Next(0,100)*10));
                inventoryId.Add(Guid.NewGuid());
                Inventory newItem = new Inventory()
                {
                    InventoryId = inventoryId[counter],
                    Name = products[counter],
                    Quantity = quanities[counter]
                    
                };
                yield return newItem;
                counter++;
            }

         
            stopwatch.Stop();

            System.Diagnostics.Debug.Write("--------------------------------------------------");
            System.Diagnostics.Debug.Write($"String generation took - {stopwatch.Elapsed.TotalMilliseconds}");
            System.Diagnostics.Debug.Write("--------------------------------------------------");

        }
//        public IEnumerable<string> AddMultipleProduct()
//        {
//            List<string> brandName = new List<string>() {"A. T. Cross Company",
//"Active Pen",
//"Alfred Dunhill",
//"Anoto",
//"C. Howard Hunt",
//"Calligraphy pen",
//"Four Treasures of the Study",
//"Camlin",
//"Caran d'Ache",
//"Carter's Ink Company",
//"Cello",
//"Cerruti",
//"Classmate Stationery",
//"Compact disc pen",
//"Conway Stewart",
//"Copic",
//"Counterfeit banknote detection pen",
//"Counter pen",
//"Crayola",
//"Cretacolor",
//"Curtis Australia",
//"D. Leonardt & Co.",
//"Decoder pen",
//"Invisible ink",
//"Demonstrator pen",
//"Derwent Cumberland Pencil Company",
//"Digital pen",
//"Dip pen",
//"Displays2Go",
//"edding AG",
//"Erasermate",
//"Esselte",
//"Esterbrook",
//"Expo dry erase markers",
//"F. Weber & Company, Inc.",
//"Faber-Castell",
//"Flex nibs",
//"Flo-Master",
//"Flux pen",
//"Fountain pen",
//"George Safford Parker",
//"Lewis Waterman",
//"Noodler's Ink",
//"Walter A. Sheaffer",
//"Frost & Adams",
//"Fudepen",
//"Macniven and Cameron",
//"Marker pen – also known as a felt-tip pen",
//"Connector pen",
//"Mean Streak",
//"Wet wipe marker",
//"Meisterstück",
//"Melody",
//"Monami",
//"Montblanc",
//"Montegrappa",
//"Muji",
//"Paint marker",
//"Paper Mate",
//"Paper Mate PhD Multi",
//"Parker Pen Company",
//"Parker Jointless",
//"Parker Duofold",
//"Parker 100",
//"Parker 51",
//"Parker Jotter",
//"Parker Vacumatic",
//"Parker Vector",
//"Quink",
//"Pelikan",
//"Pen Room",
//"Esselte",
//"Esterbrook",
//"Expo dry erase markers",
//"F. Weber & Company, Inc.",
//"Faber-Castell",
//"Flex nibs",
//"PenAgain",
//"Pentel",
//"Permanent marker",
//"Perry & Co.",
//"Pilot",
//"Porous point pen",
//"Portok",
//"Prismacolor",
//"Project Eden",
//"Rastrum",
//"Reed pen",
//"Kalamos",
//"Qalam",
//"Reeves and Sons",
//"Retipping",
//"Reynolds International Pen Company",
//"Rollerball pen",
//"Rotring",
//"Ruling pen",
//"S. T. Dupont",
//"Sakura",
//"Sanford L.P.",
//"Sharpie",
//"Schwan-Stabilo",
//"The Shanghai Hero Pen Company",
//"Sheaffer",
//"Sheaffer Prelude",
//"Project Eden",
//"Rastrum",
//"Reed pen",
//"Kalamos",
//"Qalam",
//"Reeves and Sons",
//"Retipping",
//"Reynolds International Pen Company",
//"Rollerball pen",
//"Skin pens",
//"Société Bic",
//"Space Pen",
//"Paul C. Fisher",
//"Staedtler",
//"Stipula",
//"Stylus",
//"Technical pen",
//"Tibaldi",
//"Tombow",
//"Wacom",
//"Waterman pens",
//"Waterman Hémisphère",
//"Waterman Philéas",
//"Winsor & Newton",
//"Skin pens",
//"Société Bic",
//"Space Pen",
//"Paul C. Fisher",
//"Staedtler",
//"Stipula",
//"Stylus",
//"Technical pen",
//"Tibaldi",
//"Tombow",
//"Wacom",
//"Waterman pens"
// };
//            List<string> itemName = new List<string>() {
//            "Binder clip ",
//"Black n' Red",
//"Brass fastener",
//"Bulldog clip",
//"Business card",
//"Black Astrum",
//"HCard",
//"Internet business card",
//"Carbon paper ",
//"Cartridge paper",
//"Chalkboard eraser",
//"Clipboard",
//"Colour pencil",
//"Compliments slip",
//"Continuous stationery",
//"Correction fluid",
//"Correction paper",
//"Correction tape",
//"Crane & Co.",
//"Derwent Cumberland Pencil Company",
//"Drawing pin",
//"Dymotape",
//"E-card",
//"Embossing",
//"Embossing tape",
//"Engraving",
//"Envelope",
//"Eraser",
//"Esselte",
//"File folder",
//"Foolscap folio",
//"Greeting card",
//"American Greetings",
//"Archies Ltd",
//"Baby announcement",
//"Cardmaking",
//"Cards ",
//"Carlton Cards",
//"Celebrations Group",
//"Christmas card",
//"Cookie bouquet",
//"CSS Industries",
//"David R. Beittel",
//"Forever Friends",
//"Get-well card",
//"The Greeting Card Association ",
//"Hallmark Cards",
//"Hallmark Business Connections",
//"Hallmark holiday",
//"Holiday greetings",
//"Hoops&Yoyo",
//"Irving I. Stone",
//"Marcus Ward & Co",
//"Moonpig",
//"Naughty cards",
//"New Year Card",
//"Nobleworks",
//"Recycled Paper Greetings",
//"Studio cards",
//"Touchnote",
//"Twisted Whiskers",
//"Uncooked",
//"Video ecard",
//"Highlighter ",
//"Hipster PDA",
//"Index card",
//"ISO 216",
//"ISO 217",
//"Copic",
//"Genkō yōshi",
//"Hobonichi Techo",
//"Kuretake ",
//"Namiki",
//"Pentel",
//"Pilot ",
//"Shitajiki",
//"Tombow",
//"Triart Design Marker",
//"Tsujiura",
//"Uni-ball",
//"Knife ",
//"Needle card",
//"New Zealand standard for school stationery",
//"Notebook",
//"Big Chief tablet",
//"CERF Collaborative Framework",
//"Classmate Stationery",
//"Composition book",
//"Diary",
//"Electronic lab notebook",
//"Inventor's notebook",
//"Lab notebook",
//"Open notebook science",
//"Personal organizer",
//"Police notebook",
//"Ring binder",
//"Sketchbook",
//"Paper",
//"Paper clip",
//"Paper cutter",
//"Paper Mate",
//"Paper size",
//"Pee Chee folder",
//"Pen",
//"Pencil",
//"Pencil Case",
//"Post-it note",
//"Some Royal Mail rubber bands",
//"Postal stationery",
//"Aerogram",
//"Corner card",
//"Cut square",
//"Cut to shape",
//"Formular stationery",
//"Higgins & Gage World Postal Stationery Catalog",
//"Imprinted stamp",
//"International reply coupon",
//"Letter sheet",
//"Lettercard",
//"Mulready stationery",
//"Postal card",
//"Postal Order",
//"Postal Stationery Society",
//"Postcard",
//"Sherborn Collection",
//"Stamped envelope",
//"United Postal Stationery Society",
//"Wrapper",
//"Presentation folder",
//"Pressure-sensitive adhesive",
//"Pressure-sensitive tape",
//"Seal",
//"Smythson",
//"Spindle",
//"Springback binder",
//"Staple",
//"Stapler",
//"Sticker",
//"teNeues",
//"Thermographic printing",
//"Tickler file",
//"Tipp-Ex",
//"Trade card",
//"Trapper Keeper",
//"Treasury tag",
//"Visiting card",
//"Watermark",
//"Wite-Out",
//"Worksheet",
//"Water colour"
//};
//            List<string> productVolume = new List<string>() { "1 each", "Dozen", "20 pack", "50 pack", "100 pack", "Bundle", "Carton", "Box", "500 pack", "1000 pack" };
//            List<Inventory> inventories = new List<Inventory>();
//            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

//            stopwatch.Start();


//            foreach (String brand in brandName.Distinct().Take(100))
//            {
//                foreach (String item in itemName.Distinct().Take(100))
//                {
//                    foreach (String volume in productVolume)
//                    {
//                        String productName = $"{brand} {item} {volume}";

//                        //  yield return productName;

//                    }
//                }

//            }


//            stopwatch.Stop();

//            System.Diagnostics.Debug.Write("--------------------------------------------------");
//            System.Diagnostics.Debug.Write($"String generation took - {stopwatch.Elapsed.TotalMilliseconds}");
//            System.Diagnostics.Debug.Write("--------------------------------------------------");

//        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Inventory/InsertMultipleInventory")]
        public IHttpActionResult InsertMultipleInventory()
        {
            return Ok(AddMultipleInventory());
        }

    }
}