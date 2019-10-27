using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


public class BloodGroup
{
    public string hospital;
    public string bloodGroup;
    public int unitAvailable = 0;
   public  BloodGroup(string _h, string _bg, int _ua)
    {
        hospital = _h;
        bloodGroup = _bg;
        unitAvailable = _ua;
    }
}
namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<BloodGroup> Get()
        
        {
            List<BloodGroup> list = new List<BloodGroup>();
            list.Add(new BloodGroup("ABC", "A+", 20));
            list.Add(new BloodGroup("ABC", "A-", 23));
            list.Add(new BloodGroup("ABC", "B+", 60));
            list.Add(new BloodGroup("ABC", "B-", 10));
            list.Add(new BloodGroup("ABC", "AB+", 12));
            list.Add(new BloodGroup("ABC", "AB-", 20));

            list.Add(new BloodGroup("MV", "A+", 120));
            list.Add(new BloodGroup("MV", "A-", 123));
            list.Add(new BloodGroup("MV", "B+", 160));
            list.Add(new BloodGroup("MV", "B-", 110));
            list.Add(new BloodGroup("MV", "AB+", 112));
            list.Add(new BloodGroup("MV", "AB-", 120));


            return list;
            //return new string[] { "value1", "value2" };
        }

        

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
