using Healio.Models;

namespace Healio.Services
{
    public class HealthDataService
    {
        private readonly HealioDbContext _context;
        public HealthDataService(HealioDbContext context)
        {
            _context = context;
        }
        public bool RecordHealthData(HealthData healthData)
        {
            if (_context.HealthData.Contains(healthData))
                return false;
            _context.HealthData.Add(healthData);
            return _context.SaveChanges() > 0;
        }
        public List<HealthData> GetHealthData(int id)
        {
            return _context.HealthData.Where(h => h.Patient.UserId == id).ToList();
        }
    }
}
