using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using System.Web.Http;

namespace JobHunt.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiController
    {
        CandidateManage candidateManage = new CandidateManage();

        [HttpGet, Route("profile")]
        public IHttpActionResult Profile(string userId)
        {
            var getIdCandidateByIdUser = candidateManage.GetCandidateInfoByIdAspNetUser(userId);
            if (getIdCandidateByIdUser == null) return NotFound();

            return Ok(getIdCandidateByIdUser);
        }

        [HttpPost, Route("profile")]
        public IHttpActionResult UpdateProfile(CandidateDTO candidateDTO)
        {
            var messageUpdate = "";
            var statusUpdate = "success";
            var updateProfile = candidateManage.UpdateProfileCandidate(candidateDTO);
            if (updateProfile)
                messageUpdate = "Cập nhật thông tin " + candidateDTO.CddFullName + " thành công!";
            else
            {
                statusUpdate = "error";
                messageUpdate = "Cập nhật thông tin " + candidateDTO.CddFullName + " không thành công!";
            }
            return Ok(new { message = messageUpdate, status = statusUpdate });
        }
    }
}
