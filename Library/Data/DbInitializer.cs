using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

namespace Library.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            /**LibraryBranch LibraryCard Patrons Status LibraryAsset BranchHour */
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (context.Statuses.Count() >= 4) Console.WriteLine("Database already populated.");

            List<Genre> genres = new List<Genre>()
            {
                new Genre("Unknown    "){ },
                new Genre("Action     "){ },
                new Genre("Adventure  ") { },
                new Genre("Animation  ") { },
                new Genre("Children   ") { },
                new Genre("Comedy     ") { },
                new Genre("Crime      ") { },
                new Genre("Documentary") { },
                new Genre("Drama      ") { },
                new Genre("Family     ") { },
                new Genre("Fiction     ") { },
                new Genre("Fantasy    ") { },
                new Genre("FilmNoir   ") { },
                new Genre("Horror     ") { },
                new Genre("Love       ") { },
                new Genre("Musical    ") { },
                new Genre("Mystery    ") { },
                new Genre("NonFiction ") { },
                new Genre("Romance    ") { },
                new Genre("SciFi      ") { },
                new Genre("Thriller   ") { },
                new Genre("War        ") { },
                new Genre("Western    ") { },
            };
            genres.ForEach(g => context.Add(g));
            context.SaveChanges();

            List<DerivedClass> derivedClasses = new List<DerivedClass>()
        {
            new DerivedClass(new DateTime(1998, 03, 25)){Status = StatusId.Available, Description = "Cool calm collected" },
            new DerivedClass(DateTime.Parse("1997-03-30")){Status = StatusId.OnHold, Description = "Testting..." },
        };
            derivedClasses.ForEach(c => context.Add(c));
            context.SaveChanges();

            //if (context.Statuses.Count() >= 4) //Console.WriteLine("Database already exist");return;     //Db has been seeded
            #region Populate Status
            List<Status> status = new List<Status>
        {
            new Status{Description="A library asset that has been checked out.", Name=Status.CheckedOut},
            new Status{Description="A library asset that is available for loan.", Name=Status.Available},
            new Status{Description="A library asset that has been lost.", Name=Status.Lost},
            new Status{Description="A library asset that has been placed on hold for loan.", Name = Status.OnHold},
        };
            status.ForEach(s => context.Statuses.Add(s));
            context.SaveChanges();
            #endregion

            #region Populate LibraryBranch
            List<LibraryBranch> branch = new List<LibraryBranch>()
        {
            new LibraryBranch("88 Lakeshore Drive", "Lake Shore Branch", "+1 (800) 616 1927", new DateTime(1975,03,13),"The oldest branch in Lakeview."){ },
            new LibraryBranch("123 Skyline Lane", "Mountain View Branch", "810 555-1279", new DateTime(1998,06,01),"The Mountain View Branch contains the largest collection of nature books and encyclopedia."){ },
            new LibraryBranch("540 Inventors Circle", "Pleasant Hill Branch", "19004638273", new DateTime(2017,09,20),"The newest Lakeview Library System Branch."){ },
        };
            branch.ForEach(b => context.LibraryBranches.Add(b));
            context.SaveChanges(true);
            #endregion

            #region Populate LibraryCard
            List<LibraryCard> libraryCard = new List<LibraryCard>()
        {
            new LibraryCard(new DateTime(2020,02,20), 12.00){},
            new LibraryCard(new DateTime(2020,03,20), 0.00){},
            new LibraryCard(new DateTime(2020,04,21), 0.50){},
            new LibraryCard(new DateTime(2020,05,21), 4.00){},
            new LibraryCard(new DateTime(2020,06,21), 3.50){},
            new LibraryCard(new DateTime(2020,07,23), 1.50){},
            new LibraryCard(new DateTime(2020,08,23), 0.00){},
            new LibraryCard(new DateTime(2020,09,23), 8.00){},
        };
            libraryCard.ForEach(b => context.LibraryCards.Add(b));
            context.SaveChanges();
            #endregion

            for (int i = 0; i < 5; i++)
            {
                // Use Arrays instead of List<T> to optimize performance.
                #region Populate Patron
                var patrons = new Patron[]
        {
            new Patron{FirstName = "Noble", LastName="Chukwukere", Address="Lagos", DateOfBirth = new DateTime(1998, 03, 25), TelephoneNumber = "+2349025778189", },
            new Patron{FirstName = "Laura", LastName="Norman", Address="San Francisco", DateOfBirth = DateTime.Parse("1997-12-25"), TelephoneNumber = "+18106506762", },
            new Patron("165 Peace Str", new DateTime(1986, 07,10), "Jane", new LibraryBranch(1), "Patterson", new LibraryCard(1)),
            new Patron("324 Shadow Lane", new DateTime(1984-03-12), "Margaret", new LibraryBranch(2), "Smith", new LibraryCard(2)),
            new Patron("18 Stone Close", new DateTime(1997-01-17), "Thomas", new LibraryBranch(1), "Galloway", new LibraryCard(3)),
            new Patron("246 Jennifer Street", new DateTime(1997-01-17), "Mary", new LibraryBranch(2), "Li", new LibraryCard(4)),
            new Patron("785 Park Avenue", new DateTime(1952-09-16), "Daniel", new LibraryBranch(3), "Carter", new LibraryCard(5)),
            new Patron("17 Main Street", new DateTime(1994-03-24), "Aruna", new LibraryBranch(3), "Adhiban", new LibraryCard(6)),
            new Patron("135 St. Peters Street", new DateTime(2001-11-23), "Raj", new LibraryBranch(1), "Prasad", new LibraryCard(7)),
            new Patron("135 Shell Estate", new DateTime(1981,10,16), "Tatyana", new LibraryBranch(3), "Sylvia", new LibraryCard(8)),
        };
                foreach (Patron patron in patrons)
                {
                    context.Patrons.Add(patron);
                }
                context.SaveChanges();
                #endregion


                #region Populate Book

                List<Book> books = new List<Book>()
                {
new Book { Author = "Jane Austen", Cost = 18, DeweyIndex = "823.123", ImageUrl = "", ISBN = "9781519202987", Location = new LibraryBranch(1), NumberOfCopies = 3, Status = new Status((int)StatusId.Available), Title = "Emma", Year = 2020 },
new Book(19.95,new LibraryBranch(2), StatusId.Available, "War and Peace", new DateTime(1865,03, 19).Year,"/images/books/war-and-peace.jpg",17,"4-87311-336-9","Leo Tolstoy","GSI-0011"){ Description="Tolstoy's epic masterpiece intertwines the lives of private and public individuals during the time of the Napoleonic wars and the French invasion of Russia. The fortunes of the Rostovs and the Bolkonskys, of Pierre, Natasha, and Andrei, are intimately connected with the national history that is played out in parallel with their lives. Balls and soirees alternate with councils of war and the machinations of statesmen and generals, scenes of violent battles with everyday human passions in a work whose extraordinary imaginative power has never been surpassed.", Genre=new Genre(GenreId.War) },
new Book(14.95,new LibraryBranch(1), StatusId.OnHold, "The Hobbit", new DateTime(1937, 09, 27).Year,"/images/books/the-hobbit.jpg",37,"0735619670","J.R.R. Tolkien","RBV-0012"){ Description="Written for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937. Now recognized as a timeless classic, this introduction to the hobbit Bilbo Baggins, the wizard Gandalf, Gollum, and the spectacular world of Middle-earth recounts of the adventures of a reluctant hero, a powerful and dangerous ring, and the cruel dragon Smaug the Magnificent.", Genre=new Genre(GenreId.Fantasy) },
new Book(9.95,new LibraryBranch(3), StatusId.CheckedOut, "Needful Things", new DateTime(1991, 10,new Random().Next(1, 28)).Year,"/images/books/needful-things.jpg", 32, "0-672-32546-2","Stephen King", "JAN-0013"){ Description="Leland Gaunt opens a new shop in Castle Rock called Needful Things. Anyone who enters his store finds the object of his or her lifelong dreams and desires: a prized baseball card, a healing amulet. In addition to a token payment, Gaunt requests that each person perform a little 'deed,' usually a seemingly innocent prank played on someone else from town. These practical jokes cascade out of control and soon the entire town is doing battle with itself. Only Sheriff Alan Pangborn suspects that Gaunt is behind the population's increasingly violent behavior.", Genre=new Genre(GenreId.Horror) },
new Book(8.95,new LibraryBranch(3), StatusId.Lost, "To Kill a Mockingbird", new DateTime(1960, 05, 25).Year,"/images/books/to-kill-a-mockingbird.jpg", 14, "978-7-86548-336-4","Harper Lee", "JHI-0014"){ Description="The unforgettable novel of a childhood in a sleepy Southern town and the crisis of conscience that rocked it, To Kill A Mockingbird became both an instant bestseller and a critical success when it was first published in 1960. It went on to win the Pulitzer Prize in 1961 and was later made into an Academy Award-winning film, also a classic.", Genre=new Genre(GenreId.Fiction) },
new Book(11.95,new LibraryBranch(1), StatusId.Available, "The Hot Zone", new DateTime(1994, 06, 25).Year,"/images/books/the-hot-zone.jpg", 55, "0-672-32546-2","Richard Preston", "MIU-0015"){ Description="A highly infectious, deadly virus from the central African rain forest suddenly appears in the suburbs of Washington, D.C. There is no cure. In a few days 90 percent of its victims are dead. A secret military SWAT team of soldiers and scientists is mobilized to stop the outbreak of this exotic 'hot' virus. The Hot Zone tells this dramatic story, giving a hair-raising account of the appearance of rare and lethal viruses and their 'crashes' into the human race.", Genre = new Genre(GenreId.NonFiction) },
new Book(14.95,new LibraryBranch(2), StatusId.Available, "The Universe in a Nutshell", new DateTime(2001, 10, 16).Year,"/images/books/the-universe-in-a-nutshell.jpg", 21, "978-4-87311-149-2","Stephen King", "JAN-0013"){ Description="Stephen Hawking’s phenomenal, multimillion-copy bestseller, A Brief History of Time, introduced the ideas of this brilliant theoretical physicist to readers all over the world.\n\nNow, in a major publishing event, Hawking returns with a lavishly illustrated sequel that unravels the mysteries of the major breakthroughs that have occurred in the years since the release of his acclaimed first book.", Genre = new Genre(GenreId.NonFiction) },
new Book(18, new LibraryBranch(3), StatusId.CheckedOut, "Jane Eyre", 1847, "/images/books/jane-eyre.jpg", 12, "9784127434018", "Charlotte Brontë", "822.133") { },
new Book(18.75, new LibraryBranch(2), StatusId.OnHold, "WhiteFire Guardian of Light", 1815, "/images/books/WhiteFire-Guardian-of-Light.jpg", 7, "9789442790083", "WhiteFire Comics", "822.133") { },
new Book(13.53, new LibraryBranch(1), StatusId.Lost, "Man's Search For Meaning", 2015, "/images/books/man's-search-for-meaning.jpg", byte.MaxValue, "9787734629823", "Victor E. Frankl", "845.221") { },
            };
                books.ForEach(b => context.Add(b));
                context.SaveChanges();
                #endregion

                #region Populate Video
                List<Video> videos = new List<Video>()

                {
                    new Video(){Title="Blue Velvet", Cost=24*i, Director="David Lynch", ImageUrl="", Location=new LibraryBranch(1), NumberOfCopies=3,Status=new Status(2), Year=2020,},
                    new Video(ushort.MaxValue * i, new LibraryBranch(1), StatusId.Available,"The Old Guard",2020, "/images/videos/the-old-guard.jpg",13,"Gina Prince-ByTheWood"){ },

                };
                videos.ForEach(v => context.LibraryAssets.Add(v));
                context.SaveChanges();
                #endregion
            }

            #region Populate BranchHour
            List<BranchHour> branchHours = new List<BranchHour>
                    {
                        new BranchHour(new LibraryBranch(1), 14, 1, 7),
                        new BranchHour(new LibraryBranch(1), 18, 2, 7),
                        new BranchHour(new LibraryBranch(1), 18, 3, 7),
                        new BranchHour(new LibraryBranch(1), 18, 4, 7),
                        new BranchHour(new LibraryBranch(1), 18, 5, 7),
                        new BranchHour(new LibraryBranch(1), 18, 6, 7),
                        new BranchHour(new LibraryBranch(1), 14, 7, 7),

                        new BranchHour(new LibraryBranch(2), 20, 1, 6),
                        new BranchHour(new LibraryBranch(2), 20, 2, 6),
                        new BranchHour(new LibraryBranch(2), 20, 3, 6),
                        new BranchHour(new LibraryBranch(2), 20, 4, 6),
                        new BranchHour(new LibraryBranch(2), 20, 5, 6),
                        new BranchHour(new LibraryBranch(2), 20, 6, 6),
                        new BranchHour(new LibraryBranch(2), 20, 7, 6),

                        new BranchHour(new LibraryBranch(3), 22, 1, 5),
                        new BranchHour(new LibraryBranch(3), 18, 2, 5),
                        new BranchHour(new LibraryBranch(3), 18, 3, 5),
                        new BranchHour(new LibraryBranch(3), 18, 4, 5),
                        new BranchHour(new LibraryBranch(3), 18, 5, 5),
                        new BranchHour(new LibraryBranch(3), 22, 6, 5),
                        new BranchHour(new LibraryBranch(3), 22, 7, 5),
                    };
            branchHours.ForEach(b => context.BranchHours.Add(b));
            context.SaveChanges();
            #endregion

            context.Dispose();
            return;
        }

      
    }
}
