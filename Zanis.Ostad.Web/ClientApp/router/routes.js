import MainLayout from '../components/layouts/main/index'
import HomePage from '../components/home/index'
import Lesson from '../components/home/lesson'
import PaymentHandler from '../components/home/payment-handler'
import CourseDetails from '../components/home/course-details'
import AdminLayout from '../components/layouts/admin'
import AdminDashboard from '../components/admin/dashboard'
import AdminTickets from '../components/admin/tickets'
import AdminListCourse from '../components/admin/courses'
import ChangePassword from '../components/home/change-password'
import UserTickets from '../components/user/tickets'
import UserExamSamples from '../components/user/exam-samples'
import UserCourses from '../components/user/courses'
import UserDashboard from '../components/user/dashboard'
import TeacherDashboard from '../components/teacher/dashboard'
import TeacherCourses from '../components/teacher/courses'
import UserLayout from '../components/layouts/user'
import TeacherLayout from '../components/layouts/teacher'

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
        name: "tickets",
        path: "tickets",
        component: AdminTickets
      },
      {
        name:"admin-courses",
        path:"courses",
        component:AdminListCourse
      },
      {path: "*", redirect: 'dashboard'}
    ]
  }, {
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
        name: "user-exam-samples",
        path: "exam-samples",
        component: UserExamSamples
      },
      { path: "*", redirect: 'dashboard' }
    ]
  },

];
