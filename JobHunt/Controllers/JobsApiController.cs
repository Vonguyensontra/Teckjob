using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using JobHunt.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace JobHunt.API.Controllers
{
    [RoutePrefix("api/jobs")]
    public class JobsApiController : ApiController
    {
        RecruitJobManage recruitJobManage = new RecruitJobManage();
        CandidateManage candidateManage = new CandidateManage();
        RecruitJobManage recruitjobManage = new RecruitJobManage();

        [HttpGet, Route("")]
        public IHttpActionResult ListJobs(string keyWord = null, int? idcity = null, int? idprofession = null, int page = 1, int pageSize = 100)
        {
            var jobs = recruitJobManage.GetListRecruitJobHaveSearchAndPaging(keyWord, idcity, idprofession, page, pageSize);

            return Ok(jobs);
        }

        [HttpGet, Route("{id}")]
        public IHttpActionResult JobDetail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var jobDetail = recruitJobManage.GetRecruitJobByID((int)id);

            return Ok(jobDetail);
        }

        [HttpPost, Route("submit-cv")]
        public IHttpActionResult SubmitCV([FromBody] ApplyJobRequestModel request)
        {
            var messageResult = "";
            var statusResult = "success";
            var path = "";
            var getIdCandidateByIdUser = candidateManage.GetCandidateInfoByIdAspNetUser(request.UserID);
            var recruitjob = recruitjobManage.GetRecruitJobByID(int.Parse(request.JobID));
            if (!string.IsNullOrEmpty(request.CVOld))
            {
                path = "/Assets/client/upload/cv/" + getIdCandidateByIdUser.AspNetUserDTO.UserName + "/" + request.CVOld;
            }
            else
            {
                path = UploadFile(request.Base64Content, request.FileName, getIdCandidateByIdUser.AspNetUserDTO.UserName);
            }
            var cddpostresumedto = new CandidatePostResumeDTO()
            {
                CddPhone = request.PhoneUser,
                CPRPostDate = DateTime.Now,
                CPRStatus = true,
                CPR_CandidateId = getIdCandidateByIdUser.CandidateId,
                CPR_RecruitJobId = int.Parse(request.JobID),
                CddPathFileCV = path
            };
            var checkPosted = new CandidatePostResumeManage().CheckPostedResume(cddpostresumedto.CPR_CandidateId, cddpostresumedto.CPR_RecruitJobId);
            if (!checkPosted)
            {
                var postResume = new CandidatePostResumeManage().Insert(cddpostresumedto);
                if (postResume)
                {
                    string email = recruitjob.RecruitDTO.RIEmail;
                    string html = "Ứng viên " + getIdCandidateByIdUser.CddFullName + " đã ứng tuyển công việc " + recruitjob.RJTitle;
                    string url = "Link công việc : https://localhost:44315/tuyen-dung/chi-tiet-viec-lam/" + BU.Common.GenerateUrl.GenerateSlug(recruitjob.RJTitle, recruitjob.RecruitJobId);
                    string content = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/html/TemplateEmail.cshtml"));
                    content = content.Replace("{{body}}", html);
                    content = content.Replace("{{url}}", url);
                    SendMail(email, content);
                    messageResult = "Ứng tuyển thành công";
                }
                else
                {
                    messageResult = "Đã xảy ra lỗi trong quá trình ứng tuyển";
                    statusResult = "error";
                }
            }
            else
            {
                messageResult = "Bạn đã ứng tuyển công việc này rồi. Đợi nhà tuyển dụng liên hệ lại nhé.";
                statusResult = "error";
            }


            return Ok(new { message = messageResult, status = statusResult });
        }

        private string UploadFile(string base64Content, string fileName, string nameUser)
        {
            var hashDateTimeNow = DateTime.Now.ToString("yyyyMMddHHmmss");
            string targetpath = HttpContext.Current.Server.MapPath("~/Assets/client/upload/cv");
            string getFilePath = HttpContext.Current.Server.MapPath("~/Assets/");
            string FileNameWithoutExtension = fileName.Split('.')[0];
            string Extension = Path.GetExtension(fileName);
            DirectoryInfo di = Directory.CreateDirectory(targetpath + "/" + nameUser);
            File.WriteAllBytes(Path.Combine(targetpath, nameUser, FileNameWithoutExtension + hashDateTimeNow + Extension), Convert.FromBase64String(base64Content));
            string path = "/Assets/client/upload/cv/" + nameUser + "/" + FileNameWithoutExtension + hashDateTimeNow + Extension;
            return path;
        }

        private void SendMail(string email, string body)
        {
            var formEmailAddress = ConfigurationManager.AppSettings["FormEmailAddress"].ToString();
            var formEmailDisplayName = ConfigurationManager.AppSettings["FormEmailDisplayName"].ToString();
            var formEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPost"].ToString();
            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            MailMessage message = new MailMessage(new MailAddress(formEmailAddress, formEmailDisplayName), new MailAddress(email));
            message.Subject = "Thông báo";
            message.IsBodyHtml = true;
            message.Body = body;
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(formEmailAddress, formEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enableSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
    }
}
