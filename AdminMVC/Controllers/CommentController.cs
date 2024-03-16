
using AdminMVC.Services.CommentServices;
using Microsoft.AspNetCore.Mvc;

namespace AdminMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult List()
        {
            var comments = _commentService.GetComments();
            return View(comments);
        }

        [HttpPost]
        public IActionResult Approve(int commentId)
        {
            _commentService.ApproveComment(commentId);
            return RedirectToAction("List");
        }
    }
}
