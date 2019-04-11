import MainLayout from '../components/layouts/main/index'
import HomePage from '../components/home/index'
import Lesson from '../components/home/lesson'
import PaymentHandler from '../components/home/payment-handler'
import CourseDetails from '../components/home/course-details'
import AdminLayout from '../components/layouts/admin'
import AdminDashboard from '../components/admin/dashboard'
import AdminTickets from '../components/admin/tickets'
import AdminListCourse from '../components/admin/courses'
import EditorList from '../components/admin/editor-list'
import EditAssignment from '../components/admin/edit-assignments'
import ChangePassword from '../components/home/change-password'
import UserTickets from '../components/user/tickets'
import UserExamSamples from '../components/user/exam-samples'
import UserCourses from '../components/user/courses'
import UserDashboard from '../components/user/dashboard'
import TeacherDashboard from '../components/teacher/dashboard'
import EditorDashboard from '../components/editor/dashboard'
import TeacherCourses from '../components/teacher/courses'
import Notifications from '../components/notifications'
import EditorEditAssigns from '../components/editor/edit-assigns'
import UserLayout from '../components/layouts/user'
import TeacherLayout from '../components/layouts/teacher'
import EditorLayout from '../components/layouts/editor'
import UserManagement from '../components/admin/users'
import StartTeaching from '../components/home/start-teaching'
export const routes = [
  {
    name: 'main', path: '', component: MainLayout, display: 'Home', icon: 'home',
    children: [
      {
        name: 'home',
        path: "/",
        component: HomePage
      },
      {
        name: 'start-teaching',
        path: "/start-teaching",
        component: StartTeaching
      },
      {
        name: 'lesson',
        path: "/lesson/:lessonId",
        component: Lesson
      },
      {
        name: 'payment-handle',
        path: '/PaymentComplete',
        component: PaymentHandler
      },
      {
        name: 'course',
        path: '/course/:id',
        component: CourseDetails
      },
      {
        name: 'changepassword',
        path: '/changepassword',
        component: ChangePassword
      },
    ]
  },
  {
    name: 'admin', path: '/admin', component: AdminLayout, display: 'admin', icon: 'admin',
    children: [
      {
        name: "admin-dashboard",
        path: "dashboard",
        component: AdminDashboard
      },
      {
        name: "users",
        path: "users",
        component: UserManagement
      },
      {
        name: "tickets",
        path: "tickets",
        component: AdminTickets
      },
      {
        name:"admin-courses",
        path:"courses",
        component:AdminListCourse
      },
      {
        name:"editor-assignments",
        path:"editor-assignments",
        component:EditAssignment
      },
      {
        name:"editor-list",
        path:"editor-list",
        component:EditorList
      },
      {path: "*", redirect: 'dashboard'}
    ]
  },
   {
    name: 'user', path: '/user', component: UserLayout, display: 'user', icon: 'user',
    children: [
      {
        name: "user-dashboard",
        path: "dashboard",
        component: UserDashboard
      },
      {
        name: "tickets",
        path: "tickets",
        component: UserTickets
      },
      {
        name: "user-courses",
        path: "courses",
        component: UserCourses
      },
      {
        name: "user-exam-samples",
        path: "exam-samples",
        component: UserExamSamples
      },
      {path: "*", redirect: 'dashboard'}
    ]
  },
  {
    name: 'teacher', path: '/teacher', component: TeacherLayout, display: 'teacher', icon: 'teacher',
    children: [
      {
        name: "teacher-dashboard",
        path: "dashboard",
        component: TeacherDashboard
      },
      {
        name: "tickets",
        path: "tickets",
        component: UserTickets
      },
      {
        name: "user-courses",
        path: "courses",
        component: UserCourses
      },
      {
        name: "produced-courses",
        path: "produced-courses",
        component: TeacherCourses
      },
      {
        name: "notifications",
        path: "notifications",
        component: Notifications
      },
      {
        name: "user-exam-samples",
        path: "exam-samples",
        component: UserExamSamples
      },
      { path: "*", redirect: 'dashboard' }
    ]
  },

  {
    name: 'editor', path: '/editor', component: EditorLayout, display: 'teacher', icon: 'teacher',
    children: [
      {
        name: "editor-dashboard",
        path: "dashboard",
        component: EditorDashboard
      },
      {
        name: "tickets",
        path: "tickets",
        component: UserTickets
      },
      {
        name: "user-courses",
        path: "courses",
        component: UserCourses
      },
      {
        name: "notifications",
        path: "notifications",
        component: Notifications
      },
      {
        name: "edit-assigns",
        path: "edit-assigns",
        component: EditorEditAssigns
      },
      {
        name: "user-exam-samples",
        path: "exam-samples",
        component: UserExamSamples
      },
      { path: "*", redirect: 'dashboard' }
    ]
  },
];
