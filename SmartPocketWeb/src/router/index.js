import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import MovementsLayout from '../views/movements/MovementsLayout.vue'
import AuthAPI from '../api/AuthAPI'
import CardsLayout from '../views/cards/CardsLayout.vue'
import DashboardLayout from '../views/reports/Dashboard.vue'
import NewMovementLayout from '../views/movements/NewMovementLayout.vue'
import NewRecurringPayment from '../views/movements/NewRecurringPayment.vue'
import NewCardLayout from '../views/cards/NewCardLayout.vue'
import MyMovementsLayout from '../views/movements/MyMovementsLayout.vue'

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
          children: [
            {
              path: '',
              name: 'my-movements',
              component: MyMovementsLayout
            },
          ]
        },
        {
          path: '/movement/new',
          name: 'new-movement',
          component: NewMovementLayout
        },
        {
          path: '/recurring/new',
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
