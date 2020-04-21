using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.POCO.TypePOCO; //Poco Objects, PersonT
using Sample.Aplication.Implementation; //BusinessLogic
using Sample.Infraestructure.Models; //DataBase NetCore Entity
using Newtonsoft.Json;

namespace Sample.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]    
    public class PersonController : ControllerBase
    {
        private readonly PersonaBL _repository;
        private readonly SampleDBContext _context;
        private readonly ILogger<PersonController> _logger;

        public PersonaController(SampleDBContext Context, ILogger<PersonController> Logger)
        {
            _context = Context;
            _logger = Logger;
            _repository = new PersonaBL(_context);
        }

        /// <summary>
        /// List<Person>
        /// </summary>
        /// <returns>ICollection<PersonT></returns>
        [HttpGet]
        public async Task<List<PersonT>> Get_All()
        {            
            var personList = new List<PersonT>();
            try
            {
                personList = await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                personList = null;
                _logger.LogError(ex.Message, ex);     //Serilog           
            }
            return personList;
        }

        /// <summary>
        /// Get PersonT by Id
        /// </summary>
        /// <param name="id">DB Person Id</param>
        /// <returns>PersonT</returns>
        [HttpGet("{id}")]        
        public async Task<PersonT> Get_ById(int id)
        {            
            var person = new PersonT();
            try
            {
                if (id > 0)
                {
                    person = await _repository.Get_ByIdAsync(id);
                }
                else
                {
                    person = null;
                    _logger.LogError("Bad Id parameter", id);  //Serilog                  
                }
            }
            catch (Exception ex)
            {
                person = null; = null;
                _logger.LogError(ex.Message, ex);                
            }
            return person;
        }                
        
		/// <summary>
        /// Trigger an Email and Write Log with Serilog
        /// </summary>        
        /// <returns>Custom Exception</returns>
        [HttpGet("serilog")]
        public Respuesta<PersonaT> testSerilog()
        {
            throw new Exception(String.Format("Error {0}", "Serilog Test on Sample Web API"));
        }
    }
}