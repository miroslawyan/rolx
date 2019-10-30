// -----------------------------------------------------------------------
// <copyright file="CustomerController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolXServer.Account.DataAccess;
using RolXServer.Common.DataAccess;

namespace RolXServer.Account.WebApi
{
    /// <summary>
    /// Controller for customers.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public sealed class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController" /> class.
        /// </summary>
        /// <param name="customerRepository">The customer repository.</param>
        public CustomerController(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>All customers.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            return await this.customerRepository.Entities.ToListAsync();
        }

        /// <summary>
        /// Gets the customer with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// All customer.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            var customer = await this.customerRepository.Entities.FindAsync(id);
            if (customer is null)
            {
                return this.NotFound();
            }

            return customer;
        }

        /// <summary>
        /// Creates the specified new customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns>The created customer.</returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            this.customerRepository.Entities.Add(customer);
            await this.customerRepository.SaveChanges();

            return customer;
        }

        /// <summary>
        /// Updates the customer with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="customer">The customer.</param>
        /// <returns>
        /// No content.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return this.BadRequest();
            }

            this.customerRepository.Entities.Attach(customer).State = EntityState.Modified;
            await this.customerRepository.SaveChanges();

            return this.NoContent();
        }
    }
}
