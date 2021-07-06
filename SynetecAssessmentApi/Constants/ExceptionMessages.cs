namespace SynetecAssessmentApi.Constants
{
    public class ExceptionMessages
    {
        public static string EmployeeIdIsNotValid(int id)
        {
            return $"Employee Id {id} is not valid!";
        }

        public static string EmployeeDoesNotExist(int id)
        {
            return $"Employee {id} does not exist!";
        }
    }
}
