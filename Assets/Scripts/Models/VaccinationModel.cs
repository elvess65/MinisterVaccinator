using CoreFramework;

namespace MinisterVaccinator.Models
{
    public class VaccinationModel : BaseModel
    {
        public int CurVaccinatedAmount
        {
            get => m_CurVaccinated;
            set
            {
                m_CurVaccinated = value;

                OnVaccinatedChanged?.Invoke();

                if (m_CurVaccinated >= m_Population)
                {
                    OnAllPopulationVaccinated?.Invoke();
                }
            }
        }

        public int CurInfectedAmount
        {
            get => m_CurInfected;
            set
            {
                m_CurInfected = value;

                OnInfectedChanged?.Invoke();

                if (m_CurInfected >= m_Population)
                {
                    OnAllPopulationInfected?.Invoke();
                }
            }
        }

        public int Population
        {
            set
            {
                m_Population = value;
            }
        }

        private int m_CurVaccinated;
        private int m_CurInfected;
        private int m_Population;

        public System.Action OnAllPopulationVaccinated;
        public System.Action OnAllPopulationInfected;
        public System.Action OnVaccinatedChanged;
        public System.Action OnInfectedChanged;
    }
}
