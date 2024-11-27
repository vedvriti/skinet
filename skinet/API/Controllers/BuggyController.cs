using System;
using API.DTO;
using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseAPIController
{
 [HttpGet("unauthorized")]
 public IActionResult GetUnauthorized()
 {
    return Unauthorized();
 }
 [HttpGet("badrequest")]
 public IActionResult GetBadRequest()
 {
    return BadRequest("Not a good request");
 }
    [HttpGet("notfound")]
 public IActionResult GetNotFound()
 {
   return NotFound();
 }
    [HttpGet("internalerror")]
 public IActionResult GetInternalError()
 {
   throw new Exception("This is a test exception");
 }
   [HttpPost("validationerror")]
 public IActionResult GetValidationError(CreateProductDTO product)
 {
   return Ok();
}
}
