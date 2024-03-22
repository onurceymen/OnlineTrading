namespace MainMVC.Contracts.AlertHelper
{
    public class IActionResult
    {
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 403)
            {
                ViewBag.ErrorMessage = "Üzgünüz, bu işlemi yapmaya yetkiniz yok.";
            }
            return View();
        }
    }
}
