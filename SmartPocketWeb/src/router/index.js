import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import MovementsLayout from '../views/movements/MovementsLayout.vue'
import AuthAPI from '../api/AuthAPI'
import CardsLayout from '../views/cards/CardsLayout.vue'
import DashboardLayout from '../views/reports/DashboardLayout.vue'
import NewMovementLayout from '../views/movements/NewMovementLayout.vue'
import NewRecurringPayment from '../views/movements/NewRecurringPayment.vue'
import NewCardLayout from '../views/cards/NewCardLayout.vue'
import ChartLayout from '@/views/charts/ChartLayout.vue'
import PaymentsLayout from '@/views/payments/PaymentsLayout.vue'
import RecurringPaymentsLayout from '@/views/payments/RecurringPaymentsLayout.vue'
import PendingLayout from '@/views/payments/PendingLayout.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { requiresAuth: true },
      children: [
        {
          path: '/',
          name: 'dashboard',
          component: DashboardLayout,
        },
        {
          path: '/movements',
          name: 'movements',
          component: MovementsLayout,
          meta: { requiresAuth: true },
        },
        {
          path: '/charts',
          name: 'charts',
          component: ChartLayout,
          meta: { requiresAuth: true },
        },
        {
          path: '/movement/new',
          name: 'new-movement',
          component: NewMovementLayout
        },
        {
          path: '/movement/:id?',
          name: 'edit-movement',
          component: NewMovementLayout
        },
        {
          path: '/payments',
          name: 'payments',
          component: PaymentsLayout,
          children: [
            {
              path: 'recurring',
              name: 'recurring-payments',
              component: RecurringPaymentsLayout
            },
            {
              path: 'pending',
              name: 'pending',
              component: PendingLayout
            },
          ]
        },
        {
          path: '/payment/new',
          name: 'new-recurring-payment',
          component: NewRecurringPayment
        },
        {
          path: '/cards',
          name: 'cards',
          component: CardsLayout
        },
        {
          path: 'card/new',
          name: 'new-card',
          component: NewCardLayout
        },
      ]
    },
    {
      path: '/auth',
      name: 'auth',
      component: () => import('../views/auth/AuthLayout.vue'),
      children: [
        {
          path: 'register',
          name: 'register',
          component: () => import('../views/auth/RegisterLayout.vue')
        },
        {
          path: 'login',
          name: 'login',
          component: () => import('../views/auth/LoginLayout.vue')
        },
      ]
    },
  ],
})

router.beforeEach( async (to, from, next ) => {
  if(to.matched.some(url => url.meta.requiresAuth))
  {
    try
    {
      const { data } = await AuthAPI.user();
      if(data.success === true)
        next()
      else
        next({name: 'login'})
    }
    catch (error)
    {
      next({name: 'login'})
    }
  }
  else
  {
    next()
  }
})

export default router
