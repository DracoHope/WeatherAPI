/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        /*
            The access modifier "private" means this variable can only be use within this <BaseApiController> class

            The access modifier "protected" means this variable can be use with this <BaseApiController> class and any derive classes from this <BaseApiController> class
        */
        private IMediator _mediator;

        /*
            This means will assign the _mediator to Mediator variable if is not NULL.
            If _mediator is NUll then "??=" assign anything toward the right command to the _mediator variable.
            If _mediator is NUll then assign the HTTP context from this Controller and request and get service from the <IMediator> to populate and assign the _mediator 
            The final result will be the "Mediator" will finally been assign with the <MediatoR> object
            All the derive class from this <BaseApiController> class  will inheritage the <Imediator> object which is require to communicate with the <Application>
        */
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}