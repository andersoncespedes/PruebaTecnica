using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class JourneyController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    public JourneyController(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
    }
}
