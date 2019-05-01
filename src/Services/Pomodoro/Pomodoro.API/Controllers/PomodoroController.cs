using System.Collections.Generic;
using Athena.Pomodoro.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Athena.Pomodoro.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PomodoroController : ControllerBase
    {
        private readonly PomodoroContext _pomodoroContext;
        private readonly PomodoroSettings _settings;
        //private readonly I


    }
}
