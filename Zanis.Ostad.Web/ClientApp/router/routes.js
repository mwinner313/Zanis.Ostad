import MainLayout from '../components/layouts/main/index'
import HomePage from '../components/home/index'
import Lesson from '../components/home/lesson'
import PaymentHandler from '../components/home/payment-handler'
import CourseDetails from '../components/home/course-details'
import UserLessons from '../components/user/user-lessons'

export const routes = [
  {
    name: 'main', path: '/', component: MainLayout, display: 'Home', icon: 'home',
    children: [
      {
        name: 'home',
        path: "",
        component: HomePage
      },
      {
        name: 'lesson',
        path: "/lesson/:lessonId",
        component: Lesson
      },
      {
        name : 'payment-handle',
        path : '/PaymentComplete',
        component :PaymentHandler
      },
      {
        name : 'my-lessons',
        path : '/my-lessons',
        component :UserLessons
      },
      {
        name : 'course',
        path : '/course/:id',
        component :CourseDetails
      }
    ]
  },
];
