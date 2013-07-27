using System.Data.Entity;
using System.Linq;
using ETConsole;
using FakeDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingDb
{
    public class FakePersonSet : FakeDbSet<Person>
    {
        public override Person Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.SocialSecurityNumber == (int)keyValues.Single());
        }
    }

    public class FakeContext
    {
        public FakeContext()
        {
            this.Employees = new FakePersonSet();
        }

        public IDbSet<Person> Employees { get; private set; }

        public int SaveChanges()
        {
            return 0;
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var db = new Db();

            var set = db.Set<Person>();

            set.Add(new Person
            {
                FirstName = "GGG",
                LastName = "sss",
                SocialSecurityNumber = 1111111
            });

            Assert.IsTrue(set.Items.Any());

        }



        [TestMethod]
        public void TestMethod2()
        {
            var context = new FakeContext()
                {
                    Employees ={
            new Person { FirstName = "BBB",SocialSecurityNumber = 1},
            new Person { FirstName = "AAA",SocialSecurityNumber = 2},
            new Person { FirstName = "ZZZ",SocialSecurityNumber = 3},}
                };

            var t = context.Employees.Find(1);
            Assert.IsTrue(t.SocialSecurityNumber == 1);
        }
    }
}
