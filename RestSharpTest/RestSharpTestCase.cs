using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace RestSharpTest
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Salary { get; set; }
    }


    [TestClass]
    public class RestSharpTestCase
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }

        [TestMethod]
        public void OnCallingList_ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(7, dataResponse.Count);

            foreach (Employee e in dataResponse)
            {
                System.Console.WriteLine("id: "+e.id+",Name: "+e.name+",Salary: "+e.Salary);
            }
        }

        private IRestResponse getEmployeeList()
        {
            // arrange
            RestRequest request = new RestRequest("/Employee", Method.GET);

            // act
            IRestResponse response = client.Execute(request);
            return response;
        }


        [TestMethod]
        public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            // arrange
            RestRequest request = new RestRequest("/Employee", Method.POST);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Chopper");
            jObjectbody.Add("Salary", "5000");


            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Chopper", dataResponse.name);
            Assert.AreEqual("5000", dataResponse.Salary);
            System.Console.WriteLine(response.Content);
        }


        [TestMethod]
        public void GivenEmployee_OnUpdate_ShouldReturnUpdatedEmployee()
        {
            // arrange
            RestRequest request = new RestRequest("/Employee/10", Method.PUT);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Nami");
            jObjectbody.Add("Salary", "16000");


            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            // act
            var response = client.Execute(request);

            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Nami", dataResponse.name);
            Assert.AreEqual("16000", dataResponse.Salary);
            System.Console.WriteLine(response.Content);
        }

        [TestMethod]
        public void GivenEmployeeId_OnDate_ShouldReturnSuccessStatus()
        {
            // arrange
            RestRequest request = new RestRequest("/Employee/11", Method.DELETE);

            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            System.Console.WriteLine(response.Content);
        }

    }
}
