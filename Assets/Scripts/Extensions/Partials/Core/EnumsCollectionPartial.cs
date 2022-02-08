using System.Collections.Generic;

namespace CoreFramework
{
    public static partial class EnumsCollection
    {
        public enum Roles
        {
            Medic = 0,
            Military = 1,
            Student = 2,
            Uneployed = 3,
            Teacher = 4
        }

        [System.Flags]
        public enum RolesFlags
        {
            Medic = (1 << 0),
            Military = (1 << 1),
            Student = (1 << 2),
            Uneployed = (1 << 3),
            Teacher = (1 << 4),
        }

        public enum Cures
        {
            Cure1,
            Cure2
        }

        public enum Statuses
        {
            Infected,
            Healthy,
            Vaccinated
        }

        public static void RetrieveRoles(EnumsCollection.RolesFlags roles, out List<RolesFlags> buffer)
        {
            buffer = new List<RolesFlags>();
            RetrieveRolesToBuffer(roles, ref buffer);
        }

        public static void RetrieveRolesToBuffer(RolesFlags roles, ref List<RolesFlags> buffer)
        {
            buffer.Clear();
             
            ulong flag = 1;
            foreach (RolesFlags role in System.Enum.GetValues(typeof(RolesFlags)))
            {
                ulong bits = System.Convert.ToUInt64(role);

                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits && roles.HasFlag(role))
                {
                    buffer.Add(role);
                }
            }
        }
    }
}