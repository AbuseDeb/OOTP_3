using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace OOTP_2
{
    // You must apply a DataContractAttribute or SerializableAttribute
    // to a class to have it serialized by the DataContractSerializer.
    [DataContract(Name = "Person")]
    [KnownType(typeof(ServicePersonal))]
    [KnownType(typeof(Waiter))]
    [KnownType(typeof(Administrator))]
    [KnownType(typeof(SportsMan))]
    [KnownType(typeof(HockeyPlayer))]
    [KnownType(typeof(FootballPlayer))]
    public class Person //: IExtensibleDataObject
    {

        

        [DataMember(Name = "Name")]
        public string m_cName { get; set; }
        [DataMember(Name = "LastName")]
        public string m_cLastName { get; set; }

        public Person(string Name, string LastName)
        {
            m_cName = Name;
            m_cLastName = LastName;
        }

        //private ExtensionDataObject extensionDataObject_value;
        //public ExtensionDataObject ExtensionData
        //{
        //    get
        //    {
        //        return extensionDataObject_value;
        //    }
        //    set
        //    {
        //        extensionDataObject_value = value;
        //    }
        //}
    }

    [DataContract(Name = "ServicePersonal")]
    public class ServicePersonal : Person
    {
        [DataMember(Name = "NameOfOrganization")]
        string m_cNameOfOrganization { get; set; }

        public ServicePersonal(string Name, string LastName, string Organization) : base(Name, LastName)
        {
            m_cNameOfOrganization = Organization;
        }
    }

    [DataContract(Name = "Waiter")]
    public class Waiter : ServicePersonal
    {
        [DataMember(Name = "Salary")]
        string m_cSalary { get; set; }

        public Waiter(string Name, string LastName, string Organization, string Salary) : base(Name, LastName, Organization)
        {
            m_cSalary = Salary;
        }
    }


    [DataContract(Name = "Administrator")]
    public class Administrator : ServicePersonal
    {
        [DataMember(Name = "Salary")]
        string m_cSalary;

        public Administrator(string Name, string LastName, string Organization, string Salary) : base(Name, LastName, Organization)
        {
            m_cSalary = Salary;
        }
    }

    [DataContract(Name = "SportsMan")]
    public class SportsMan : Person
    {
        [DataMember(Name = "Team")]
        string m_cTeam;
        public SportsMan(string Name, string LastName, string Team) : base(Name, LastName)
        {
            m_cTeam = Team;
        }
    }

    [DataContract(Name = "HockeyPlayer")]
    public class HockeyPlayer : SportsMan
    {
        [DataMember(Name = "Position")]
        string m_cPosition;
        public HockeyPlayer(string Name, string LastName, string Team, string Position) : base(Name, LastName, Team)
        {
            m_cPosition = Position;
        }
    }

    [DataContract(Name = "FootballPlayer")]
    public class FootballPlayer : SportsMan
    {
        [DataMember(Name = "Position")]
        string m_cPosition;
        public FootballPlayer(string Name, string LastName, string Team, string Position) : base(Name, LastName, Team)
        {
            m_cPosition = Position;
        }
    }


    public class Test
    {
        //private Test() { }

        //public static void Main()
        //{
        //    try
        //    {
        //        WriteObject("DataContractSerializerExample.xml");
        //        ReadObject("DataContractSerializerExample.xml");
        //    }

        //    catch (SerializationException serExc)
        //    {
        //        Console.WriteLine("Serialization Failed");
        //        Console.WriteLine(serExc.Message);
        //    }
        //    catch (Exception exc)
        //    {
        //        Console.WriteLine(
        //        "The serialization operation failed: {0} StackTrace: {1}",
        //        exc.Message, exc.StackTrace);
        //    }

        //    finally
        //    {
        //        Console.WriteLine("Press <Enter> to exit....");
        //        Console.ReadLine();
        //    }
        //}

        public static void WriteObject(string fileName)
        {
            Console.WriteLine(
                "Creating a Person object and serializing it.");
            Person p1 = new Person("Дмитрий", "Лафа");

            Person p2 = new ServicePersonal("Дмитрий", "Лафа", "PizzaTral");

            List<Person> people = new List<Person>();

            people.Add(new Waiter("Дмитрий","Лафа","PizzaTral","1000$"));
            people.Add(new Administrator("Олег", "Гроф", "LocalZ", "2000$"));
            people.Add(new SportsMan("Влера", "Слыш", "Onix"));
            people.Add(new HockeyPlayer("Слава", "Лан", "Dinamo", "Нападающий"));
            people.Add(new FootballPlayer("Лев", "Лом", "Днепр", "Полу-защитник"));

            FileStream writer = new FileStream(fileName, FileMode.Create);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(List<Person>));
            ser.WriteObject(writer, people);
            writer.Close();
        }

        public static void ReadObject(string fileName)
        {
            Console.WriteLine("Deserializing an instance of the object.");
            FileStream fs = new FileStream(fileName,
            FileMode.Open);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(Person));

            // Deserialize the data and read it from the instance.
            Person deserializedPerson =
                (Person)ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();
            //Console.WriteLine(String.Format("{0} {1}",
            //deserializedPerson.FirstName, deserializedPerson.LastName));
        }
    }
}
