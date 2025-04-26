using AutoMapper;
using Healio.Models;
using Healio.Models.DTO;
using Healio.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Healio.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public ProfileModel(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            docDTO = new PublicDoctorDTO();
            patDTO = new PublicPatientDTO();
        }
        [BindProperty]
        public PublicUserDTO userDTO { get; set; }
        [BindProperty]
        public PublicDoctorDTO docDTO { get; set; }
        [BindProperty]
        public PublicPatientDTO patDTO { get; set; }
        public User user { get; set; }

        public void OnGet()
        {
            FetchUser();
            FetchDTOs();
        }

        private void FetchUser()
        {
            user = _userService.GetUserByEmail(User.FindFirst(ClaimTypes.Email)?.Value);
            if (user != null)
                userDTO = _mapper.Map<PublicUserDTO>(user);

        }

        public void FetchDTOs() {
            var pat = _userService.GetPatientByUserId(user.Id);
            var doc = _userService.GetDoctorByUserId(user.Id);
            
            if (doc != null)
                docDTO = _mapper.Map<PublicDoctorDTO>(doc);
            if (pat != null)
                patDTO = _mapper.Map<PublicPatientDTO>(pat);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FetchUser();
            if (User.FindFirst(ClaimTypes.Role)?.Value == "doctor")
            {
                if (_userService.GetDoctorByUserId(user.Id) == null)
                {
                    _userService.RegisterDoctor(new DoctorProfile { ClinicAddress = docDTO.ClinicAddress, Specialization = docDTO.Specialization, UserId = user.Id, User = user });
                }
                if(!docDTO.ClinicAddress.IsNullOrEmpty() && !docDTO.Specialization.IsNullOrEmpty())
                {
                    _userService.UpdateDoctorProfile(user.Id, docDTO.ClinicAddress, docDTO.Specialization);
                }
            }
            else
            {
                if (_userService.GetPatientByUserId(user.Id) == null)
                {
                    _userService.RegisterPatient(new PatientProfile { DateOfBirth = patDTO.DateOfBirth, MedicalHistory = patDTO.MedicalHistory, UserId = user.Id, User = user });
                }
                if (!patDTO.MedicalHistory.IsNullOrEmpty()){
                    _userService.UpdatePatientMedicalHistory(user.Id, patDTO.MedicalHistory, patDTO.DateOfBirth);
                }
            }
            return RedirectToPage("/Index");
        }
    }
}