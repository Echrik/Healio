<<<<<<< Updated upstream
using Microsoft.AspNetCore.Authorization;
=======
﻿using Healio.Models;
using Healio.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Healio.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserService _userService;
        private User user { get; set; }
        private PatientProfile patient { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }

        // Doctor-specific properties
        [BindProperty]
        public string specialization { get; set; }
        [BindProperty]
        public string clinicAddress { get; set; }
        [BindProperty]
        public string name { get; set; }

        // Patient-specific properties
        [BindProperty]
        public string PatName { get; set; }
        [BindProperty]
        public DateTime date_of_birth { get; set; }
        [BindProperty]
        public string medical_history { get; set; }

        public int counter { get; set; } = 0;

        public ProfileModel(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
<<<<<<< Updated upstream
            UserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            UserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value == "patient" ? "P�ciens" : "Orvos";
=======
            user = _userService.GetUserByEmail(User.FindFirst(ClaimTypes.Email)?.Value);
            patient = _userService.GetPatientById(user.Id);

            UserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            UserRole = User.FindFirst(ClaimTypes.Role)?.Value == "doctor" ? "Orvos" : "Páciens";
        }

        public void OnGetFillFormDoctor(string userEmail)
        {
            user = _userService.GetUserByEmail(userEmail);
            DoctorProfile doc = _userService.GetDoctorById(user.Id);
            if (doc.Name != null)
            {
                name = doc.Name;
                specialization = doc.Specialization;
                clinicAddress = doc.ClinicAddress;
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            user = _userService.GetUserByEmail(User.FindFirst(ClaimTypes.Email)?.Value);
            if (User.FindFirst(ClaimTypes.Role)?.Value == "doctor")
            {
                if (_userService.GetDoctorById(user.Id) == null)
                {
                    _userService.RegisterDoctor(new DoctorProfile { Name = name, ClinicAddress = clinicAddress, Specialization = specialization, UserId = user.Id, User = user });
                }
            }
            else
            {
                if (_userService.GetPatientById(user.Id) == null)
                {
                    _userService.RegisterPatient(new PatientProfile { Name = name, DateOfBirth = date_of_birth, MedicalHistory = medical_history, UserId = user.Id, User = user });
                }
            }
            

            return RedirectToPage("/Index");
>>>>>>> Stashed changes
        }
    }
}