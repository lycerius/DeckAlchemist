﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DeckAlchemist.WebApp.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}