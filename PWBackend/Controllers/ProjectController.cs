using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWBackend.Models.Entities;
using PWBackend.Models.Helpers;
using PWBackend.Service;

namespace PWBackend.Controllers
{
    struct IconTextId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? IconPath { get; set; }
    }

    [Route("[controller]")]
    [ApiController]
    public class ProjectController(PWDbContext context, IConfiguration configuration) : ControllerBase
    {
        [HttpGet]
        [Route("GetProjectTypes")]
        public IActionResult GetProjectTypes()
        {
            List<IconTextId> types = new List<IconTextId>();
            var typesDB = context.ProjectTypes.ToList();

            foreach (var type in typesDB)
            {
                types.Add(new IconTextId
                {
                    Id = type.Id,
                    Name = type.Name,
                    IconPath = type.IconPath
                });
            }

            return Ok(types);
        }

        [HttpGet]
        [Route("GetProjectStatuses")]
        public IActionResult GetProjectStatuses()
        {
            List<IconTextId> statuses = new List<IconTextId>();
            var statusesDB = context.ProjectStatuses.ToList();
            foreach (var status in statusesDB)
            {
                statuses.Add(new IconTextId
                {
                    Id = status.Id,
                    Name = status.Name,
                    IconPath = status.IconPath
                });
            }
            return Ok(statuses);
        }

        [HttpGet]
        [Route("GetProjectTechnologies")]
        public IActionResult GetProjectTechnologies()
        {
            List<IconTextId> technologies = new List<IconTextId>();
            var technologiesDB = context.ProjectTechnologies.ToList();
            foreach (var technology in technologiesDB)
            {
                technologies.Add(new IconTextId
                {
                    Id = technology.Id,
                    Name = technology.Name,
                    IconPath = technology.IconPath
                });
            }
            return Ok(technologies);
        }

        [HttpGet]
        [Route("GetProjectTags")]
        public IActionResult GetProjectTags()
        {
            List<IconTextId> tags = new List<IconTextId>();
            var tagsDB = context.ProjectTags.ToList();
            foreach (var tag in tagsDB)
            {
                tags.Add(new IconTextId
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }
            return Ok(tags);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateProject")]
        public IActionResult CreateProject(CreateProject project)
        {
            var userService = new UserService(context, configuration);
            var user = userService.FindUserByClaims(HttpContext.User);
            if (user == null)
            {
                return Unauthorized(new { code = "userNotFound" });
            }


            List<ProjectTechnology> projectTechnologies = new List<ProjectTechnology>();
            if (project.Technologies != null)
            {
                foreach (var techId in project.Technologies)
                {
                    ProjectTechnology? projectTech = context.ProjectTechnologies.Find(techId);
                    if (projectTech != null)
                    {
                        projectTechnologies.Add(projectTech);
                    }
                }
            }

            ProjectStatus projectStatus = context.ProjectStatuses.Find(project.StatusId)!;
            ProjectType projectType = context.ProjectTypes.Find(project.TypeId)!;

            // Date is Unix timestamp, we need to convert it to DateTime
            var dateStarted = DateTimeOffset.FromUnixTimeSeconds(project.DateStarted).DateTime;
            var dateEnded = DateTimeOffset.FromUnixTimeSeconds(project.DateEnded).DateTime;

            var newProject = new Project
            {
                Name = project.Name,
                ShortDescription = project.ShortDescription,
                LongDescription = project.LongDescription,
                DateStarted = dateStarted,
                DateEnded = dateEnded,
                Status = projectStatus,
                Type = projectType,
                Technologies = projectTechnologies
            };

            context.Projects.Add(newProject);
            context.SaveChanges();

            return Ok(new { id = newProject.Id });
        }

        [HttpGet]
        [Route("GetProjects")]
        public IActionResult GetProjects()
        {
            var projects = context.Projects.ToList();
            return Ok(projects);
        }

        [HttpGet]
        [Route("GetProject")]
        public IActionResult GetProject(int id)
        {
            var project = context.Projects
                .Include(p => p.Technologies)
                .Include(p => p.Tags)
                .Include(p => p.Media)
                .Include(p => p.Status)
                .Include(p => p.Type)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.ShortDescription,
                    p.LongDescription,
                    p.ImagePath,
                    p.VideoPath,
                    p.DateStarted,
                    p.DateEnded,
                    Status = new { p.Status.Id, p.Status.Name, p.Status.IconPath },
                    Type = new { p.Type.Id, p.Type.Name, p.Type.IconPath },
                    Technologies = p.Technologies!.Select(t => new { t.Id, t.Name, t.IconPath }),
                    Tags = p.Tags!.Select(t => new { t.Id, t.Name }),
                    Media = p.Media!.Select(m => new { m.Id, m.Description, m.IsLink, m.Path }),
                })
                .FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound(new { code = "projectNotFound" });
            }
            return Ok(project);
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteProject")]
        public IActionResult DeleteProject(int id)
        {
            var userService = new UserService(context, configuration);
            var user = userService.FindUserByClaims(HttpContext.User);
            if (user == null)
            {
                return Unauthorized(new { code = "userNotFound" });
            }

            var project = context.Projects.Find(id);
            if (project == null)
            {
                return NotFound(new { code = "projectNotFound" });
            }
            context.Projects.Remove(project);
            context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("GetProjectComments")]
        public IActionResult GetProjectComments(int id)
        {
            var comments = context.ProjectComments
                .Include(p => p.Project)
                .Where(x => x.Project.Id == id)
                .OrderByDescending(c => c.Time)
                .Select(c => new
                {
                    c.Id,
                    c.Username,
                    c.Contents,
                    c.Time
                }).ToList();

            return Ok(comments);
        }

        [HttpPost]
        [Route("SendComment")]
        public IActionResult SendComment(SendComment details)
        {
            var project = context.Projects.Find(details.ProjectId);
            if (project == null)
            {
                return NotFound(new { code = "projectNotFound" });
            }

            if (string.IsNullOrWhiteSpace(details.Name) || string.IsNullOrWhiteSpace(details.Comment))
            {
                return BadRequest(new { code = "invalidInput" });
            }

            var newComment = new ProjectComment
            {
                Project = project,
                Username = details.Name,
                Contents = details.Comment,
                Time = DateTime.UtcNow
            };

            context.ProjectComments.Add(newComment);
            context.SaveChanges();

            return Ok();
        }
    }
}
