namespace Zanis.Ostad.Core.Entities.Contents
{
    public enum CourseApprovalStatus
    {
        PendingToApproveByAdmin = 0,
        ApprovedByAdminOrActivatedByTeacher = 5,
        RejectedByAdmin = 10,
        DeactivatedByTeacher = 15
    }
}