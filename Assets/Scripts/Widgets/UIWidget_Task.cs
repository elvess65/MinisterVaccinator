using CoreFramework;
using FrameworkPackages.MinMax;
using MinisterVaccinator.Models;
using System.Collections.Generic;
using System.Text;

namespace MinisterVaccinator.Widgets
{
    public class UIWidget_Task : UIWidget_Text
    {
        private GameDataModel m_GameDataModel;
        private GameplayModel m_GameplayModel;

        public void Initialize()
        {
            m_GameDataModel = Dispatcher.GetModel<GameDataModel>();
            m_GameplayModel = Dispatcher.GetModel<GameplayModel>();

            Initialize(GetTask());
        }

        private string GetTask()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("Vaccine ");

            //Roles
            EnumsCollection.RetrieveRoles(m_GameplayModel.CurrentTask.RolesToVaccinate, out List<EnumsCollection.Roles> rolesBuffer);
            for (int i = 0; i < rolesBuffer.Count; i++)
            {
                strBuilder.Append($"{rolesBuffer[i]}");

                if (rolesBuffer.Count > 1)
                {
                    if (i == rolesBuffer.Count - 2)
                        strBuilder.Append(" and ");
                    else if (i < rolesBuffer.Count - 2)
                        strBuilder.Append(", ");
                }
            }

            strBuilder.Append(" of age ");

            //Merge age groups
            List<MinMax> agesToApply = new List<MinMax>(m_GameplayModel.CurrentTask.Cure.AgesToApply);
            for (int i = 1; i < agesToApply.Count; i++)
            {
               /* MinMax cur = agesToApply[i];
                MinMax prev = agesToApply[i - 1];

                if (cur.Max < prev.Min)
                    continue;

               /* if (cur.Max >= prev.Min)
                {
                    MinMax newMinMax = prev;
                    newMinMax.Min = cur.Min;

                    if (cur.Max > prev.Max)
                        newMinMax.Max = cur.Max;

                    agesToApply[i - 1] = newMinMax;
                    agesToApply.RemoveAt(i--);

                    continue;
                }*/

                /*if (cur.Min <= prev.Max)
                {
                    MinMax newMinMax = prev;
                    newMinMax.Max = cur.Max;

                    if (cur.Min < prev.Min)
                        newMinMax.Min = cur.Min;

                    agesToApply[i - 1] = newMinMax;
                    agesToApply.RemoveAt(i--);
                }*/
            }

            //Message
            for (int i = 0; i < agesToApply.Count; i++)
            {
                strBuilder.Append($"{agesToApply[i].Min}-{agesToApply[i].Max}");

                if (agesToApply.Count > 1)
                {
                    if (i == agesToApply.Count - 2)
                        strBuilder.Append(" and ");
                    else if (i < agesToApply.Count - 2)
                        strBuilder.Append(", ");
                }
            }

            return strBuilder.ToString();
        }
    }
}
