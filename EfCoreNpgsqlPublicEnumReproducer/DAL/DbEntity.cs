using System;

namespace EnumBugReproducer.Models.DAL
{


    public abstract class DbEntity
    {
        public Guid Id { get; set; }
    }

    public enum MyEnum { ValueOne, Value2 }



    public class DbEntitySubclassTwo : DbEntity
    {

    }


    public class DbEntitySubclassOne : DbEntity
    {
    }
}
