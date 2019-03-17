using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Zanis.Ostad.Core.Entities.Cart;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Edits;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zanis.Ostad.Core.Entities
{
    public class User:IdentityUser<long>
    {
        public ICollection<StudentCourseMapping>  BoughtCourses{ get; set; }
        public ICollection<StudentExamSampleMapping>  BoughtExamSamples{ get; set; }
        public ICollection<TeacherLessonMapping> ProducedContents { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<TicketItem> TicketItems { get; set; }
        public ICollection<EditAssignment> EditAssignments { get; set; }
        public string TeacherCode { get; set; }
        public string FullName { get; set; }
        public ICollection<CartItem> Cart { get; set; }
        public string AvatarPath { get; set; }
    }
}