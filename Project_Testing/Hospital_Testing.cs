using Project_OOP_WPF;
namespace Project_Testing
{
    public static class TestUtilities
    {
            // Correct hospital names
            public static IEnumerable<object[]> CorrectHospitalNames => new List<object[]>
            {
                new object[] { "Cityscape Medical Center" },
                new object[] { "Comfort Haven Clinic" },
                new object[] { "Beyondlimit Wellness" },
                new object[] { "      Cityscape Medical Center" },
                new object[] { "99th Medical Group Nellis Air Force Base" }
            };

            // Incorrect hospital names
            public static IEnumerable<object[]> IncorrectHospitalNames => new List<object[]>
            {
                new object[] { null },
                new object[] { " " },
                new object[] { "" },
                new object[] { "1" }
            };

            // Correct department names
            public static IEnumerable<object[]> CorrectDepartmentNames => new List<object[]>
            {
                new object[] { "Finance" },
                new object[] { "Surgery" },
                new object[] { "ER" },
                new object[] { "Radiology" },
                new object[] { "Laboratory" },
                new object[] { "Psychiatry" },
                new object[] { "      Finance" }
            };

            // Incorrect department names
            public static IEnumerable<object[]> IncorrectDepartmentNames => new List<object[]>
            {
                new object[] { null },
                new object[] { " " },
                new object[] { "" },
                new object[] { "1" },
                new object[] { "abcde12345" }
            };

            // Correct person names
            public static IEnumerable<object[]> CorrectPersonNames => new List<object[]>
            {
                new object[] { "Joe" },
                new object[] { "Jill" },
                new object[] { "Andrey" },
                new object[] { "John" },
                new object[] { "Vasiliy" },
                new object[] { "Robert" },
                new object[] { "Bob" },
                new object[] { "        Joe" }
            };

            // Incorrect person names
            public static IEnumerable<object[]> IncorrectPersonNames => new List<object[]>
            {
                new object[] { null },
                new object[] { " " },
                new object[] { "" },
                new object[] { "1" },
                new object[] { "abcde12345" }
            };

            // Incorrect medical record information
            public static IEnumerable<object[]> IncorrectMedicalRecordInfo => new List<object[]>
            {
                new object[] { null },
                new object[] { " " },
                new object[] { "" }
            };
        


        public static void TestCleanup()
        {
            Staff.IDManager = new IDManagement();
            Person.IDManager = new IDManagement();
        }


        public static Hospital DefaultHospital_Testing()
        {
            return new Hospital(
                "TestName", // hospital name
                "TestLocation", // hospital location
                new SortedSet<int> { 1, 2, 3 } // rooms
            );
        }

        //public static Department DefaultDepartment_Testing(Hospital parent)
        //{
        //    return new Department(
        //        parent, // tie to a hospital
        //        "DefaultDepartmentName", // name of the department
        //        new List<int>() { 1, 2, 3 } // rooms of the department
        //    );
        //}

        public static Patient DefaultPatient_Testing(Hospital parent)
        {
            return new Patient(
                "DefaultName", // first
                "DefaultName", // middle
                "DefaultName", // last names
                DateTime.Now, // DOB
                parent // tie to a hospital
            );
        }

        public static Staff DefaultStaff_Testing(Hospital parent)
        {
            return new Staff(
                "DefaultName", // first
                "DefaultName", // middle
                "DefaultName", // last names
                DateTime.Now.AddYears(-19), // DOB
                new List<StaffRole>() { StaffRole.Administrator }, // roles
                parent // hospital tie
                // { DefaultDepartment_Testing(parent) } // department tie
            );
        }
    }

    [TestClass]
    public class Hospital_Testing
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectHospitalNames), typeof(TestUtilities))]
        public void Hospital_Constructor_IncorrectNames_Test(string IncorrectHospitalName)
        {
            string location = "this can be anything in any format doesn't really matter";

            SortedSet<int> rooms = new() { 1, 2, 3, 4, 5 };

            if (string.IsNullOrWhiteSpace(IncorrectHospitalName))
                Assert.ThrowsException<ExceptionList>(() => new Hospital(IncorrectHospitalName, location, rooms), "Not checking for empty strings");
            else
                Assert.ThrowsException<ExceptionList>(() => new Hospital(IncorrectHospitalName, location, rooms), "Not checking for name format");
        }

        //[TestMethod]
        //public void idmanagementtest_temp()
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();
        //    testHospital.AddStaff("cohtea", "cohteua", "gjeai", DateTime.Now.AddYears(-20), new List<StaffRole> { StaffRole.Paramedic });
        //    testHospital.AddPatient("cohtea", "cohteua", "gjeai", DateTime.Now.AddYears(-20));

        //    Assert.AreNotEqual(testHospital.People[0], testHospital.People[1]);

        //    Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
        //    testHospital2.AddStaff("cohtea", "cohteua", "gjeai", DateTime.Now.AddYears(-20), new List<StaffRole> { StaffRole.Paramedic });
        //    testHospital2.AddPatient("cohtea", "cohteua", "gjeai", DateTime.Now.AddYears(-20));

        //    Assert.AreNotEqual(testHospital2.People[0], testHospital2.People[1]);
        //}

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectHospitalNames), typeof(TestUtilities))]
        public void Hospital_Constructor_CorrectNames_Test(string CorrectHospitalName)
        {
            string location = "this can be anything in any format doesn't really matter";

            SortedSet<int> rooms = new() { 1, 2, 3, 4, 5 };

            new Hospital(CorrectHospitalName, location, rooms); // will throw an exception if it doesnt go through
            
        }

        [TestMethod]
        public void Hospital_Constructor_Rooms_Test()
        {
            string location = "this can be anything in any format doesn't really matter";

            Assert.ThrowsException<ExceptionList>(() => new Hospital("TestHospital", location, new SortedSet<int>() { -1 }), "Not checking for correct room format");
            Assert.ThrowsException<ExceptionList>(() => new Hospital("TestHospital", location, new SortedSet<int>() { 0 }), "Not checking for correct room format");
            Assert.ThrowsException<ExceptionList>(() => new Hospital("TestHospital", location, new SortedSet<int>()), "Not checking for correct room format");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectHospitalNames), typeof(TestUtilities))]
        public void Hospital_CorrectNameChange_Test(string CorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            testHospital.Name = CorrectName;
            Assert.AreEqual(CorrectName.Trim(), testHospital.Name, "Name assignment is incorrect");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectHospitalNames), typeof(TestUtilities))]
        public void Hospital_IncorrectNameChange_Test(string IncorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            if (string.IsNullOrWhiteSpace(IncorrectName))
                Assert.ThrowsException<NullReferenceException>(() => testHospital.ChangeNameOrLocation(IncorrectName), "Not checking for empty strings");
            else
                Assert.ThrowsException<ArgumentException>(() => testHospital.ChangeNameOrLocation(IncorrectName), "Not checking for name format");
        }

        [TestMethod]
        public void Hospital_AddRoom_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            testHospital.AddRoom(4);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2, 3, 4 }, testHospital.Rooms, "Not adding rooms up correctly?");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRoom(0), "Not checking for incorrect value range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRoom(-1), "Not checking for incorrect value range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRoom(1), "Not checking for overlap");
        }

        [TestMethod]
        public void Hospital_AddRooms_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            testHospital.AddRooms(new SortedSet<int>() { 4, 5 });
            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 4, 5 }, testHospital.Rooms);
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new SortedSet<int>() { -1, 0 }), "Not checking for incorrect value range"); // 0 and negative room id's are not allowed
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new SortedSet<int>() { 1 }), "Not checking for overlap"); // overlap is not allowed
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new SortedSet<int>() { 1, 2 }), "Not checking for overlap");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new SortedSet<int>() { 1, 2, 3 }), "Not checking for overlap");
        }

        //[DataTestMethod]
        //[DynamicData(nameof(TestUtilities.IncorrectDepartmentNames), typeof(TestUtilities))]
        //public void Hospital_AddIncorrectDepartmentName_Test(string IncorrectName)
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();

        //    // incorrect name formats
        //    if (string.IsNullOrWhiteSpace(IncorrectName))
        //        Assert.ThrowsException<NullReferenceException>(() => testHospital.AddDepartment(IncorrectName, new List<int>() { 1, 2, 3 }), "Not checking for empty strings");
        //    else
        //        Assert.ThrowsException<ArgumentException>(() => testHospital.AddDepartment(IncorrectName, new List<int>() { 1, 2, 3 }), "Not checking for incorrect name format");
        //}

        //[DataTestMethod]
        //[DynamicData(nameof(TestUtilities.CorrectDepartmentNames), typeof(TestUtilities))]
        //public void Hospital_AddCorrectDepartmentName_Test(string CorrectName)
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();

        //    // cannot add rooms that do not exist within the hospital
        //    Assert.ThrowsException<ExceptionList>(() => testHospital.AddDepartment(CorrectName, new List<int>() { 4, 5 }), "Allowing non-existent rooms");

        //    testHospital.AddDepartment(CorrectName, new List<int>() { 1, 2, 3 });
        //    Assert.AreEqual(1, testHospital.Departments.Count);

        //    // must avoid duplicate names
        //    Assert.ThrowsException<ExceptionList>(() => testHospital.AddDepartment(CorrectName, new List<int>() { 1, 2, 3 }), "Allowing duplicate names");
        //}

        //[DataTestMethod]
        //[DynamicData(nameof(TestUtilities.CorrectDepartmentNames), typeof(TestUtilities))]
        //public void Hospital_RemoveDepartmentNullInput_Test(string IncorrectName)
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();

        //    if (string.IsNullOrEmpty(IncorrectName))
        //        Assert.ThrowsException<NullReferenceException>(() => testHospital.RemoveDepartment(IncorrectName), "Not checking for empty strings");

        //}

        //[TestMethod]
        //public void Hospital_RemoveDepartmentIncorrectInput_Test()
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();
        //    testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
        //    testHospital.AddDepartment("CorrectName1", new List<int>() { 1, 2, 3 });
        //    testHospital.AddDepartment("CorrectName2", new List<int>() { 1, 2, 3 });

        //    Assert.AreEqual(3, testHospital.Departments.Count, "Not adding correctly?");

        //    testHospital.RemoveDepartment("CorrectName1");

        //    Assert.AreEqual(2, testHospital.Departments.Count, "Not actually deleting the object?");
        //    Assert.ThrowsException<ArgumentException>(() => testHospital.RemoveDepartment("CorrectName1"), "Can delete what is not there anymore");
        //}

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewPatientIncorrectName_Test(string IncorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            if (string.IsNullOrWhiteSpace(IncorrectName))
                Assert.ThrowsException<ExceptionList>(() => testHospital.AddPatient(IncorrectName, IncorrectName, IncorrectName, DateTime.Now), "Not checking for empty strings");
            else
                Assert.ThrowsException<ExceptionList>(() => testHospital.AddPatient(IncorrectName, IncorrectName, IncorrectName, DateTime.Now), "Wrong algorithm for checking over name formats");
        }

        [TestMethod]
        public void Hospital_PatientCounter_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();

            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);
            testHospital2.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);
            Assert.AreNotEqual(((IPerson)testHospital.Patients[0]).ID, ((IPerson)testHospital2.Patients[0]).ID, "Patient ID should always stay unique");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewPatientIncorrectDate_Test(string CorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now);
            Assert.ThrowsException<ExceptionList>(() => testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now.AddYears(-100)), "Not checking for year range");
            Assert.ThrowsException<ExceptionList>(() => testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now.AddYears(100)), "Not checking for year range");
            Assert.ThrowsException<ExceptionList>(() => testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now.AddDays(1)), "Not checking for day range");

            Assert.AreEqual(1, testHospital.Patients.Count, "Either adding is broken, or you are adding objects despite exceptions");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewPatientCorrect_Test(string CorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now);

            Assert.AreEqual(1, testHospital.Patients.Count, "Something wrong with how you add things");

            testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now);
            Assert.AreNotEqual(((IPerson)testHospital.Patients[0]).ID, ((IPerson)testHospital.Patients[1]).ID);
        }

        [TestMethod]
        public void Hospital_AddExistingPatient_Test()
        {
            // Create the first hospital and add a patient
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);

            Assert.AreEqual(1, testHospital.Patients.Count, "Something wrong with how you add things");

            Patient patient = testHospital.Patients[0];

            // Ensure duplicate patients cannot be added to the same hospital
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(patient), "You are allowing duplicate patients");

            // Ensure the patient's ID remains the same across hospitals
            int patientID = ((IPerson)testHospital.Patients[0]).ID;
            Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
            testHospital2.AddPatient(patient);

            Assert.AreEqual(patientID, ((IPerson)testHospital2.Patients[0]).ID, "Patient ID must stay the same");
        }


        [TestMethod]
        public void Hospital_TransferPatient_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);

            // cannot transfer a patient to the same hospital
            Assert.ThrowsException<ArgumentException>(() => testHospital.TransferPatient(testHospital.Patients[0], testHospital), "Can transfer patients to the same hospital");

            // the id should stay the same
            int patientID = ((IPerson)testHospital.Patients[0]).ID;
            testHospital.TransferPatient(testHospital.Patients[0], testHospital2);

            Assert.AreEqual(0, testHospital.Patients.Count, "Not removing patients after transfer");
            Assert.AreEqual(patientID, ((IPerson)testHospital2.Patients[0]).ID, "Patient ID must stay the same");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewStaff_InvalidNames(string incorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            Assert.ThrowsException<ExceptionList>(() => testHospital.AddStaff(incorrectName, incorrectName, incorrectName, DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator }));
        }

        [TestMethod]
        public void Hospital_AddNewStaff_EmptyRoles()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>()));
        }

        [TestMethod]
        public void Hospital_AddNewStaff_ValidStaff()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });

            Assert.AreEqual(1, testHospital.ActiveStaff.Count, "Staff count mismatch");

            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            Assert.AreNotEqual(((IPerson)testHospital.ActiveStaff[0]).ID, ((IPerson)testHospital.ActiveStaff[1]).ID, "Staff IDs should always be unique");
        }

        [TestMethod]
        public void Hospital_TransferStaff_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator});
            

            Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
            
            testHospital.TransferPerson<Staff>(0, testHospital2);

            Assert.ThrowsException<ArgumentException>(() => testHospital.TransferPerson<Staff>(0, testHospital2), "Staff with ID 0 should not exist in this context");
            Assert.ThrowsException<ArgumentException>(() => testHospital2.TransferPerson<Staff>(0, testHospital2), "Can't add staff to the same Hospital");

            
            //testHospital2.TransferStaff(0, testHospital2); // should be able to pass since it transfers staff from one dep to another
        }

        [TestMethod]
        public void Hospital_RemoveStaff_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } );

            Assert.ThrowsException<ArgumentException>(() => testHospital.RemovePerson(-1), "Negative staff ID should throw an exception");
            Assert.ThrowsException<ArgumentException>(() => testHospital.RemovePerson(10), "Non-existent staff ID should throw an exception");

            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator });
            testHospital.RemovePerson(0);

            Assert.AreEqual(1, ((IPerson)testHospital.ActiveStaff[0]).ID, "Incorrect staff removal logic");
        }
    }

    //[TestClass]
    //public class Department_Testing
    //{
    //    [TestInitialize]
    //    public void Initialize()
    //    {
    //        TestUtilities.TestCleanup();
    //    }
    //    //[TestMethod]
    //    //public void Department_Constructor_ParameterTesting()
    //    //{
    //    //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();
    //    //    testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });

    //    //    Assert.AreEqual("DefaultDepartmentName", testHospital.Departments[0].Name, "Incorrect name assignment?");
    //    //    Assert.AreEqual(testHospital.ActiveStaff[0], testHospital.Departments[0].HeadOfDepartment, "Assigning the head incorrectly?");
    //    //    CollectionAssert.AreEquivalent(new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Departments[0].DepartmentStaff, "Incorrectly assigning staff");
    //    //    CollectionAssert.AreEquivalent(new List<int> { 1, 2, 3 }, testHospital.Departments[0].DepartmentRooms, "Assigning rooms incorrectly");
    //    //    Assert.AreEqual(testHospital, testHospital.Departments[0].ParentHospital, "Assigning the link back to the parent hospital incorrectly");
    //    //}

    //    [DataTestMethod]
    //    [DynamicData(nameof(TestUtilities.IncorrectDepartmentNames), typeof(TestUtilities))]
    //    public void Department_Constructor_IncorrectNames(string IncorrectNames)
    //    {
    //        Hospital testHospital = TestUtilities.DefaultHospital_Testing();
    //        if(string.IsNullOrWhiteSpace(IncorrectNames)) Assert.ThrowsException<NullReferenceException>(() => new Department(testHospital, IncorrectNames, new List<int>() { 1, 2, 3 }), "Not checking format");
    //        else Assert.ThrowsException<ArgumentException>(() => new Department(testHospital, IncorrectNames, new List<int>() { 1, 2, 3 }), "Not checking format");
    //    }

    //    [TestMethod]
    //    public void Department_ConstructorIncorrectRooms_Test()
    //    {
    //        Hospital testHospital = TestUtilities.DefaultHospital_Testing();
    //        Assert.ThrowsException<ArgumentException>(() => new Department(testHospital, "CorrectName", new List<int>() { 4, 5 }), "Not checking available rooms");
    //        Assert.ThrowsException<ArgumentException>(() => new Department(testHospital, "CorrectName", new List<int>() { -1 }), "Not checking room value range");
    //        Assert.ThrowsException<ArgumentException>(() => new Department(testHospital, "CorrectName", new List<int>() { 0 }), "Not checking room value range");
    //        Assert.ThrowsException<NullReferenceException>(() => new Department(testHospital, "CorrectName", new List<int>() { }), "Not checking for empty strings");
    //    }

    //    [TestMethod]
    //    public void Department_ChangeHead_InvalidCases()
    //    {
    //        Hospital testHospital = TestUtilities.DefaultHospital_Testing();
    //        testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
    //        testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } { testHospital.Departments[0] });
    //        testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } { testHospital.Departments[0] });
            
    //        testHospital.Departments[0].ChangeHead(((IPerson)testHospital.ActiveStaff[0]).ID);

    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeHead(((IPerson)testHospital.ActiveStaff[0]).ID), "Cannot assign staff to a position they are already occupying");
    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeHead(10), "Not checking for value range");
    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeHead(-1), "Not checking for value range");
    //    }

    //    [DataTestMethod]
    //    [DynamicData(nameof(TestUtilities.IncorrectDepartmentNames), typeof(TestUtilities))]
    //    public void Department_ChangeName_Test(string IncorrectNames)
    //    {
    //        Hospital testHospital = TestUtilities.DefaultHospital_Testing();
    //        testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
    //        testHospital.AddDepartment("CorrectName2", new List<int>() { 1, 2, 3 });

    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeName("CorrectName2"), "Cannot allow duplicate department names in the same hospital");

    //        if (string.IsNullOrWhiteSpace(IncorrectNames))
    //            Assert.ThrowsException<NullReferenceException>(() => testHospital.Departments[0].ChangeName(IncorrectNames), "Not checking for empty strings");
    //        else
    //            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeName(IncorrectNames), "Not checking for format");

    //    }

    //    [TestMethod]
    //    public void Department_AddStaff_Test()
    //    {
    //        Hospital testHospital = TestUtilities.DefaultHospital_Testing();
    //        testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
    //        testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } { testHospital.Departments[0] });

    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].AddStaff(-1), "Not checking for ID range");
    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].AddStaff(10), "Not checking for ID range");
    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].AddStaff(((IPerson)testHospital.ActiveStaff[0]).ID), "Not checking for ID range");
    //    }

    //    [TestMethod]
    //    public void Department_RemoveStaff_Test()
    //    {
    //        Hospital testHospital = TestUtilities.DefaultHospital_Testing();
    //        testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
    //        testHospital.AddDepartment("CorrectName2", new List<int>() { 1, 2, 3 });
    //        testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } { testHospital.Departments[0] });
    //        testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } { testHospital.Departments[0] });
    //        testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } { testHospital.Departments[0] });
    //        testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } { testHospital.Departments[1] });

    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].RemoveStaff(-1), "Not checking for ID range");
    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].RemoveStaff(10), "Not checking for ID range");
    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].RemoveStaff(((IPerson)testHospital.ActiveStaff[0]).ID), "Not checking for ID range");

    //        Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
    //        testHospital2.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
    //        testHospital2.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator } { testHospital2.Departments[0] });

    //        Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].RemoveStaff(((IPerson)testHospital2.ActiveStaff[0]).ID), "Not checking for whether the staff belongs to this hospital");
    //    }

    //    [TestMethod]
    //    public void Department_TransferStaff_DifferentHospital()
    //    {
    //        Hospital testHospital = TestUtilities.DefaultHospital_Testing();
    //        testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
    //        testHospital.AddDepartment("CorrectName2", new List<int>() { 1, 2, 3 });
    //        testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } { testHospital.Departments[0] });
    //        testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole>() { StaffRole.Administrator } { testHospital.Departments[1] });

    //        Assert.AreEqual(2, testHospital.Departments.Count, "Incorrectly adding departments");
    //        Assert.AreEqual(2, testHospital.ActiveStaff.Count, "Incorrectly adding staff");

    //        try { testHospital.Departments[0].TransferStaff(0, testHospital.Departments[1]); }
    //        catch { Assert.Fail("Not allowing transfer of staff between departments"); }

    //        Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
    //        testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
    //    }
    //}

    //[TestClass]
    //public class IDManagement_Testing
    //{

    //}

    [TestClass]
    public class Staff_Testing
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Staff_Constructor_ValidParameters(string CorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Staff testStaff = new Staff(CorrectNames, CorrectNames, CorrectNames, DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator }, testHospital);
            Assert.AreEqual(CorrectNames.Trim().Trim(), ((IPerson)testStaff).FirstName, "Somehow assigning a wrong first name");
            Assert.AreEqual(CorrectNames.Trim(), ((IPerson)testStaff).MiddleName, "Somehow assigning a wrong middle name");
            Assert.AreEqual(CorrectNames.Trim(), ((IPerson)testStaff).LastName, "Somehow assigning a wrong last name");
            Assert.AreEqual(testHospital, ((IPerson)testStaff).CurrentHospital, "Incorrectly assigning a hospital link");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Staff_Constructor_InvalidParameters(string IncorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            if (string.IsNullOrWhiteSpace(IncorrectNames))
            {
                Assert.ThrowsException<ExceptionList>(() => new Staff(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for empty strings in first name");
                Assert.ThrowsException<ExceptionList>(() => new Staff("CorrectName", IncorrectNames, "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for empty strings in middle name");
                Assert.ThrowsException<ExceptionList>(() => new Staff("CorrectName", "CorrectName", IncorrectNames, DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for empty strings in last name");
            }
            else
            {
                Assert.ThrowsException<ExceptionList>(() => new Staff(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Allowing wrong format on first name");
                Assert.ThrowsException<ExceptionList>(() => new Staff("CorrectName", IncorrectNames, "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Allowing wrong format on middle name");
                Assert.ThrowsException<ExceptionList>(() => new Staff("CorrectName", "CorrectName", IncorrectNames, DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Allowing wrong format on last name");
            }

            Assert.ThrowsException<ExceptionList>(() => new Staff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(1), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for DOB range");
            Assert.ThrowsException<ExceptionList>(() => new Staff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-120), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for DOB range");
            Assert.ThrowsException<ExceptionList>(() => new Staff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole> { }, testHospital), "Not checking for empty role assignments");

            Assert.AreEqual(0, Staff.IDManager.GenerateID(), "Adding ID's despite incorrect object creation");
        }

        //[TestMethod]
        //public void Staff_AddAppointment_ValidParameters()
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();
        //    testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
        //    testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19));

        //    DateTime time = DateTime.Now.AddDays(2); // shouldnt be able to make appointments 
        //    testHospital.ActiveStaff[0].AddAppointment(1, time, time.AddMinutes(1), new List<Staff> { testHospital.ActiveStaff[0]  }, testHospital.Patients[0], AppointmentPurpose.Consultation);
        //    CollectionAssert.AreEquivalent<int>(1, testHospital.ActiveStaff[0].Schedule.Appointments.Count, "Not creating appointments correctly");
        //    CollectionAssert.AreEquivalent(time, testHospital.ActiveStaff[0].Schedule.Appointments[0].Time, "Not assigning time correctly");
        //}

        //[TestMethod]
        //public void Staff_AddAppointment_InvalidParameters()
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();
        //    testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
        //    testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19));

        //    Assert.ThrowsException<NullReferenceException>(() => testHospital.ActiveStaff[0].AddAppointment(0, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddMinutes(1), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Room does not exist");
        //    Assert.ThrowsException<NullReferenceException>(() => testHospital.ActiveStaff[0].AddAppointment(-1, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddMinutes(1), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Room does not exist");
        //    Assert.ThrowsException<NullReferenceException>(() => testHospital.ActiveStaff[0].AddAppointment(10, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddMinutes(1), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Room does not exist");
        //    Assert.ThrowsException<ArgumentException>(() => testHospital.ActiveStaff[0].AddAppointment(1, DateTime.Now, DateTime.Now.AddMinutes(1), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Time range is too short");
        //    Assert.ThrowsException<ArgumentException>(() => testHospital.ActiveStaff[0].AddAppointment(1, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1).AddMinutes(1), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Negative time range");
        //    Assert.ThrowsException<ArgumentException>(() => testHospital.ActiveStaff[0].AddAppointment(1, DateTime.Now.AddMonths(3), DateTime.Now.AddMonths(3).AddMinutes(1), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Too high of a time range");
        //    Assert.ThrowsException<ArgumentException>(() => testHospital.ActiveStaff[0].AddAppointment(1, DateTime.Now.AddDays(3), DateTime.Now.AddDays(2).AddMinutes(-1), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "End time cannot be before the start time");
        //}

        [TestMethod]
        public void Staff_GetFullName_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Staff testStaff = TestUtilities.DefaultStaff_Testing(testHospital);

            Assert.AreEqual($"DefaultName DefaultName DefaultName", ((IPerson)testStaff).GetFullName(), "Probably incorrect output format");
        }

        [TestMethod]
        public void Staff_ChangeInfo_ValidParameters()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Staff testStaff = TestUtilities.DefaultStaff_Testing(testHospital);

            DateTime staffDOB = ((IPerson)testStaff).BirthDate;
            DateTime time = DateTime.Now.AddYears(-19);

            ((IPerson)testStaff).ChangeInfo("DefaultName", "DefaultName", "DefaultName", time);
            Assert.AreEqual("DefaultName", ((IPerson)testStaff).FirstName, "Not assigning a new first name");
            Assert.AreEqual("DefaultName", ((IPerson)testStaff).MiddleName, "Not assigning a new middle name");
            Assert.AreEqual("DefaultName", ((IPerson)testStaff).LastName, "Not assigning a new last name");
            Assert.AreNotEqual(staffDOB, ((IPerson)testStaff).BirthDate, "Not assigning a new date of birth");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Staff_ChangeInfo_InvalidParameters(string names)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Staff testStaff = TestUtilities.DefaultStaff_Testing(testHospital);

            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testStaff).ChangeInfo(names, "CorrectName", "CorrectName", DateTime.Now), "Not checking format for first name");
            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testStaff).ChangeInfo("CorrectName", names, "CorrectName", DateTime.Now), "Not checking format for middle name");
            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testStaff).ChangeInfo("CorrectName", "CorrectName", names, DateTime.Now), "Not checking format for last name");
            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testStaff).ChangeInfo("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-120)), "Not checking birthdate range");
            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testStaff).ChangeInfo("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(1)), "Not checking birthdate range");
        }
        // there should also be a ToString() test but im not sure of what the format should be just yet
    }

    [TestClass]
    public class Patient_Testing
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Patient_Constructor_ValidParameters(string CorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            DateTime time = DateTime.Now;
            Patient testPatient = new Patient(CorrectNames, CorrectNames, CorrectNames, time, testHospital);
            Assert.AreEqual(CorrectNames.Trim(), ((IPerson)testPatient).FirstName, "Somehow assigning a wrong first name");
            Assert.AreEqual(CorrectNames.Trim(), ((IPerson)testPatient).MiddleName, "Somehow assigning a wrong middle name");
            Assert.AreEqual(CorrectNames.Trim(), ((IPerson)testPatient).LastName, "Somehow assigning a wrong last name");
            Assert.AreEqual(time, ((IPerson)testPatient).BirthDate, "Assigning the wrong birthdate");
            Assert.AreEqual(testHospital, ((IPerson)testPatient).CurrentHospital, "Assigning the wrong hospital values?");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Patient_Constructor_InvalidParameters(string IncorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            if (string.IsNullOrWhiteSpace(IncorrectNames))
            {
                Assert.ThrowsException<ExceptionList>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Not checking for empty strings in first name");
                Assert.ThrowsException<ExceptionList>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Not checking for empty strings in middle name");
                Assert.ThrowsException<ExceptionList>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Not checking for empty strings in last name");
            }
            else
            {
                Assert.ThrowsException<ExceptionList>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Allowing wrong format on first name");
                Assert.ThrowsException<ExceptionList>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Allowing wrong format on middle name");
                Assert.ThrowsException<ExceptionList>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Allowing wrong format on last name");
            }

            Assert.ThrowsException<ExceptionList>(() => new Patient("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(1), testHospital), "Not checking for DOB range");
            Assert.ThrowsException<ExceptionList>(() => new Patient("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-120), testHospital), "Not checking for DOB range");

            Assert.AreEqual(0, Patient.IDManager.GenerateID(), "Adding ID's despite incorrect object creation");
        }

        [TestMethod]
        public void Patient_GenerateCompositeID_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

            DateTime testDate = DateTime.Today;
            string result = testPatient.GenerateCompositeID(testDate);
            string expected = $"0-{testDate.Day:D2}:{testDate.Month:D2}:{testDate.Year:D2}";
            Assert.AreEqual(expected, result, "Incorrect ID format");

            result = testPatient.GenerateCompositeID(testDate);
            expected = $"1-{testDate.Day:D2}:{testDate.Month:D2}:{testDate.Year:D2}";

            Assert.AreEqual(expected, result, "Incorrect ID numeration");

            testDate = DateTime.Today.AddHours(5);
            result = testPatient.GenerateCompositeID(testDate);
            expected = $"2-{testDate.Day:D2}:{testDate.Month:D2}:{testDate.Year:D2}";

            Assert.AreEqual(expected, result, "Incorrect ID numeration");
        }

        [TestMethod]
        public void Patient_AddMedicalRecord_ValidParameters()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

            DateTime testDate = DateTime.Now;
            testPatient.AddMedicalRecord(
                new List<string> { "diagnoses" },
                new List<string> { "treatments" },
                new List<string> { "medications" },
                testDate,
                new List<Staff> { testHospital.ActiveStaff[0] });
            string expectedCompositeID = $"0-{testDate.Day:D2}:{testDate.Month:D2}:{testDate.Year}";

            Assert.AreEqual(expectedCompositeID, testPatient.MedicalHistory.First().ID, "Incorrect composite key generation");
            CollectionAssert.AreEquivalent(new List<Staff> { testHospital.ActiveStaff[0] }, testPatient.MedicalHistory.First().ParticipatingStaff, "Incorrect staff assignment");
            CollectionAssert.AreEquivalent(new List<string> { "diagnoses" }, testPatient.MedicalHistory.First().Diagnoses, "Incorrect diagnoses assignment");
            CollectionAssert.AreEquivalent(new List<string> { "treatments" }, testPatient.MedicalHistory.First().Treatments, "Incorrect treatments assignment");
            CollectionAssert.AreEquivalent(new List<string> { "medications" }, testPatient.MedicalHistory.First().Medications, "Incorrect medications assignment");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectMedicalRecordInfo), typeof(TestUtilities))]
        public void Patient_AddMedicalRecord_InvalidParameters(string invalidMedRecInfo)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

            DateTime testDate = DateTime.Now;
            Assert.ThrowsException<ArgumentException>(
                () => testPatient.AddMedicalRecord(
                        new List<string> { "diagnoses" },
                        new List<string> { "treatments" },
                        new List<string> { "medications" },
                        DateTime.Now.AddYears(1),
                        new List<Staff> { })
                , "Not checking for date value range");
            Assert.ThrowsException<ArgumentException>(
                () => testPatient.AddMedicalRecord(
                        new List<string> { "diagnoses" },
                        new List<string> { "treatments" },
                        new List<string> { "medications" },
                        DateTime.Now.AddHours(1),
                        new List<Staff> { })
                , "Not checking for date value range");

            if (string.IsNullOrWhiteSpace(invalidMedRecInfo))
            {
                Assert.ThrowsException<NullReferenceException>(
                    () => testPatient.AddMedicalRecord(
                            new List<string> { invalidMedRecInfo },
                            new List<string> { "treatments" },
                            new List<string> { "medications" },
                            testDate,
                            new List<Staff> { testHospital.ActiveStaff[0] })
                    , "Not checking for empty diagnoses list");

                Assert.ThrowsException<NullReferenceException>(
                    () => testPatient.AddMedicalRecord(
                            new List<string> { "diagnoses" },
                            new List<string> { invalidMedRecInfo },
                            new List<string> { "medications" },
                            testDate,
                            new List<Staff> { testHospital.ActiveStaff[0] })
                    , "Not checking for empty treatments list");

                Assert.ThrowsException<NullReferenceException>(
                    () => testPatient.AddMedicalRecord(
                            new List<string> { "diagnoses" },
                            new List<string> { "treatments" },
                            new List<string> { invalidMedRecInfo },
                            testDate,
                            new List<Staff> { testHospital.ActiveStaff[0] })
                    , "Not checking for empty medications list");
            }
            else
            {
                Assert.ThrowsException<ArgumentException>(
                    () => testPatient.AddMedicalRecord(
                            new List<string> { invalidMedRecInfo },
                            new List<string> { "treatments" },
                            new List<string> { "medications" },
                            testDate,
                            new List<Staff> { testHospital.ActiveStaff[0] })
                    , "Not checking for invalid diagnoses format");

                Assert.ThrowsException<ArgumentException>(
                    () => testPatient.AddMedicalRecord(
                            new List<string> { "diagnoses" },
                            new List<string> { invalidMedRecInfo },
                            new List<string> { "medications" },
                            testDate,
                            new List<Staff> { testHospital.ActiveStaff[0] })
                    , "Not checking for invalid treatments format");

                Assert.ThrowsException<ArgumentException>(
                    () => testPatient.AddMedicalRecord(
                            new List<string> { "diagnoses" },
                            new List<string> { "treatments" },
                            new List<string> { invalidMedRecInfo },
                            testDate,
                            new List<Staff> { testHospital.ActiveStaff[0] })
                    , "Not checking for invalid medications format");
            }
        }

        //[TestMethod]
        //public void Patient_AddAppointment_ValidParameters()
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();
        //    testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
        //    Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

        //    DateTime time = DateTime.Now.AddDays(2); // shouldnt be able to make appointments 
        //    testPatient.AddAppointment(1, time, time.AddMinutes(1), testHospital.ActiveStaff, AppointmentPurpose.Consultation);
        //    CollectionAssert.AreEquivalent<int>(1, testPatient.Schedule.Appointments.Count, "Not creating appointments correctly");
        //    CollectionAssert.AreEquivalent(time, testPatient.Schedule.Appointments[0].Time, "Not assigning time correctly");
        //}

        //[TestMethod]
        //public void Patient_AddAppointment_InvalidParameters()
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();
        //    testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
        //    Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

        //    Assert.ThrowsException<NullReferenceException>(() => testPatient.AddAppointment(0, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddMinutes(1), testHospital.ActiveStaff, AppointmentPurpose.Consultation), "Room does not exist");
        //    Assert.ThrowsException<NullReferenceException>(() => testPatient.AddAppointment(-1, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddMinutes(1), testHospital.ActiveStaff, AppointmentPurpose.Consultation), "Room does not exist");
        //    Assert.ThrowsException<NullReferenceException>(() => testPatient.AddAppointment(10, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddMinutes(1), testHospital.ActiveStaff, AppointmentPurpose.Consultation), "Room does not exist");
        //    Assert.ThrowsException<ArgumentException>(() => testPatient.AddAppointment(1, DateTime.Now, DateTime.Now.AddMinutes(1), testHospital.ActiveStaff, AppointmentPurpose.Consultation), "Time range is too short");
        //    Assert.ThrowsException<ArgumentException>(() => testPatient.AddAppointment(1, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1).AddMinutes(1), testHospital.ActiveStaff, AppointmentPurpose.Consultation), "Negative time range");
        //    Assert.ThrowsException<ArgumentException>(() => testPatient.AddAppointment(1, DateTime.Now.AddMonths(3), DateTime.Now.AddMonths(3).AddMinutes(1), testHospital.ActiveStaff, AppointmentPurpose.Consultation), "Too high of a time range");
        //    Assert.ThrowsException<ArgumentException>(() => testPatient.AddAppointment(1, DateTime.Now.AddDays(3), DateTime.Now.AddDays(2).AddMinutes(-1), testHospital.ActiveStaff, AppointmentPurpose.Consultation), "End time cannot be before the start time");
        //}

        [TestMethod]
        public void Patient_GetFullName_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

            string expected = "DefaultName DefaultName DefaultName";
            Assert.AreEqual(expected, ((IPerson)testPatient).GetFullName(), "Must output in format: FirstName MiddleName LastName");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Patient_ChangeInfo_InvalidParameters(string names)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testPatient).ChangeInfo(names, "CorrectName", "CorrectName", DateTime.Now.AddYears(-20)), "Not checking format for first name");
            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testPatient).ChangeInfo("CorrectName", names, "CorrectName", DateTime.Now.AddYears(-20)), "Not checking format for middle name");
            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testPatient).ChangeInfo("CorrectName", "CorrectName", names, DateTime.Now.AddYears(-20)), "Not checking format for last name");
            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testPatient).ChangeInfo("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-120)), "Not checking for birthdate range");
            Assert.ThrowsException<ExceptionList>(() => ((IPerson)testPatient).ChangeInfo("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(1)), "Not checking for birthdate range");
        }

        //[TestMethod]
        //public void Patient_ToString_Test()
        //{

        //}
        // still haven't decided on what the tostring format should be
    }

    [TestClass]
    public class MedicalRecord_Testing 
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        [TestMethod]
        public void MedicalRecord_Constructor_ValidParameters()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);

            DateTime testDate = DateTime.Now;

            testHospital.Patients[0].AddMedicalRecord(
                new List<string> { "no"},
                new List<string> { "limit" },
                new List<string> { "here" },
                testDate);

            testHospital.Patients[0].AddMedicalRecord(
                new List<string> { "no" },
                new List<string> { "limit" },
                new List<string> { "here" },
                testDate);

            testHospital.Patients[0].AddMedicalRecord(
                new List<string> { "no" },
                new List<string> { "limit" },
                new List<string> { "here" },
                testDate.AddDays(-1));


            Assert.AreEqual(3, testHospital.Patients[0].MedicalHistory.Count(), "Expected 3 medical records to be added.");
            Assert.AreNotEqual(testHospital.Patients[0].MedicalHistory[0], testHospital.Patients[0].MedicalHistory[1], "Records created at the same time should have different IDs.");
            Assert.AreNotEqual(testHospital.Patients[0].MedicalHistory[0], testHospital.Patients[0].MedicalHistory[2], "Records created on different days should have different IDs.");
        }

        // generally speaking you just cannot fuck up this constructor
        // most errors would come from assigning it to wrong hospitals and stuff like that,
        // and that's done through different methods in other classes, such as patient.cs

        // is this even needed? probably is but later
        //[TestMethod]
        //public void MedicalRecord_ToString_Test()
        //{

        //}
    }

    [TestClass]
    public class AppointmentSchedule_Testing
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        //[TestMethod]
        //// both the patient and the staff involved in an appointment should have the same appointments in their schedule
        //public void AppointmentSchedule_SyncBetweenStaffAndPatient_Testing() 
        //{
        //    Hospital testHospital = TestUtilities.DefaultHospital_Testing();
        //    testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
        //    testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);
        //    // writing down the time at which the appointment will be created
        //    DateTime time = DateTime.Now.AddDays(2);
        //    testHospital.Patients[0].Schedule.CreateAppointment(
        //        1,
        //        time,
        //        time.AddMinutes(15),
        //        testHospital.ActiveStaff,
        //        testHospital.Patients[0],
        //        AppointmentPurpose.Consultation);

        //    // checking the amount to see whether the appoitnments have been created
        //    Assert.AreEqual(1, testHospital.Patients[0].Schedule.Appointments.Count);
        //    Assert.AreEqual(1, testHospital.ActiveStaff[0].Schedule.Appointments.Count);
        //    Assert.AreEqual(testHospital.Patients[0].Schedule.Appointments.Count, testHospital.ActiveStaff[0].Schedule.Appointments.Count);
            
        //    // cancelling the appointment from the Patient side
        //    testHospital.Patients[0].Schedule.CancelAppointment(0);

        //    // checking whether the change has been mirrored
        //    Assert.AreEqual(AppointmentState.Cancelled, testHospital.Patients[0].Schedule.Appointments[0].State);
        //    Assert.AreEqual(AppointmentState.Cancelled, testHospital.ActiveStaff[0].Schedule.Appointments[0].State);
        //    Assert.AreEqual(testHospital.Patients[0].Schedule.Appointments.Count, testHospital.ActiveStaff[0].Schedule.Appointments.Count);

        //    // doing the same things from the Staff side
        //    testHospital.ActiveStaff[0].Schedule.CreateAppointment(
        //        1,
        //        time.AddMinutes(31),
        //        time.AddMinutes(45),
        //        testHospital.ActiveStaff,
        //        testHospital.Patients[0],
        //        AppointmentPurpose.Consultation);

        //    Assert.AreEqual(2, testHospital.Patients[0].Schedule.Appointments.Count);
        //    Assert.AreEqual(2, testHospital.ActiveStaff[0].Schedule.Appointments.Count);
        //    Assert.AreEqual(testHospital.Patients[0].Schedule.Appointments.Count, testHospital.ActiveStaff[0].Schedule.Appointments.Count);

        //    testHospital.ActiveStaff[0].Schedule.CancelAppointment(0);

        //    Assert.AreEqual(AppointmentState.Cancelled, testHospital.Patients[0].Schedule.Appointments[0].State);
        //    Assert.AreEqual(AppointmentState.Cancelled, testHospital.ActiveStaff[0].Schedule.Appointments[0].State);
        //    Assert.AreEqual(testHospital.Patients[0].Schedule.Appointments.Count, testHospital.ActiveStaff[0].Schedule.Appointments.Count);
            
        //} // rewrite later i cannot be bothered rn

        [TestMethod]
        public void AppointmentSchedule_TimeOverlap_Testing()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);

            DateTime time = DateTime.Now.AddDays(2);
            testHospital.Patients[0].Schedule.CreateAppointment(
                    1,
                    time,
                    time.AddMinutes(15),
                    new List<Staff> { testHospital.ActiveStaff[0] },
                    testHospital.Patients[0],
                    AppointmentPurpose.Consultation);

            // assigning an appointment at the same time with the same participants should throw an argumentexception exception
            Assert.ThrowsException<ExceptionList>(
                () => testHospital.ActiveStaff[0].Schedule.CreateAppointment(
                    1, 
                    time, 
                    time.AddMinutes(15), 
                    new List<Staff> { testHospital.ActiveStaff[0] }, 
                    testHospital.Patients[0], 
                    AppointmentPurpose.Consultation));

            // assigning an appointment at the same time from a different person entirely should also throw an argumentexception exception
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            Assert.ThrowsException<ExceptionList>(
                () => testHospital.ActiveStaff[1].Schedule.CreateAppointment(
                    1,
                    time,
                    time.AddMinutes(15),
                    new List<Staff> { testHospital.ActiveStaff[1] },
                    testHospital.Patients[0],
                    AppointmentPurpose.Consultation));
        }
    }

    [TestClass]
    public class Appointment_Testing
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        [TestMethod]
        public void Appointment_ConstructorValidParameters_Testing()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);

            DateTime time = DateTime.Now.AddDays(2);
            Appointment testAppointment = new Appointment(1, time, time.AddMinutes(15), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation);
            Assert.AreEqual(1, testAppointment.RoomID);
            Assert.AreEqual(time, testAppointment.StartTime);
            Assert.AreEqual(time.AddMinutes(15), testAppointment.EndTime);
            CollectionAssert.AreEquivalent(new List<Staff> { testHospital.ActiveStaff[0] }, testAppointment.Staff);
            Assert.AreEqual(testHospital.Patients[0], testAppointment.Appointee);
            Assert.AreEqual(AppointmentPurpose.Consultation, testAppointment.Purpose);
            Assert.AreEqual(AppointmentState.Scheduled, testAppointment.State);
        }

        [TestMethod]
        public void Appointment_ConstructorInvalidParameters_Testing()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-19), new List<StaffRole> { StaffRole.Administrator });
            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);

            DateTime time = DateTime.Now.AddDays(2);
            // rooms
            Assert.ThrowsException<ExceptionList>(() => new Appointment(0, time, time.AddMinutes(15), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Room does not exist");
            Assert.ThrowsException<ExceptionList>(() => new Appointment(4, time, time.AddMinutes(15), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Room does not exist");
            Assert.ThrowsException<ExceptionList>(() => new Appointment(-1, time, time.AddMinutes(15), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Room cannot exist");
            // time
            Assert.ThrowsException<ExceptionList>(() => new Appointment(1, DateTime.Now, DateTime.Now.AddMinutes(15), new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Invalid time range (trying to make an appointment right now)");
            Assert.ThrowsException<ExceptionList>(() => new Appointment(1, DateTime.Now.AddMinutes(-15), DateTime.Now, new List<Staff> { testHospital.ActiveStaff[0] }, testHospital.Patients[0], AppointmentPurpose.Consultation), "Invalid time range (trying to make an appointment in the past)");
            // missing: staff
            Assert.ThrowsException<ExceptionList>(() => new Appointment(1, time, time.AddMinutes(15), new List<Staff> { }, testHospital.Patients[0], AppointmentPurpose.Consultation), "There must be at least some staff present");
            Assert.ThrowsException<ExceptionList>(() => new Appointment(1, time, time.AddMinutes(15), null, testHospital.Patients[0], AppointmentPurpose.Consultation), "Staff cannot be null");
            // missing: patients
            Assert.ThrowsException<ExceptionList>(() => new Appointment(1, time, time.AddMinutes(15), new List<Staff> { testHospital.ActiveStaff[0] }, null, AppointmentPurpose.Consultation), "There must be an appointee");
        }

        //[TestMethod]
        //public void Appointment_ChangeState_Testing()
        //{

        //}
    }
}