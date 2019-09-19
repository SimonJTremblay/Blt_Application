import Vue from 'vue'
import Router from 'vue-router'
import Dashboard from './views/Dashboard.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Dashboard',
      component: Dashboard
    },
    {
      path: '/ProjectView',
      name: 'ProjectView',
      // route level code-splitting
      // this generates a separate chunk (ProjectView.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "ProjectView" */ './views/ProjectView.vue'),
      props: {name: 'projectId'}
    },
    {
      path: '/ProjectView/:projectId',
      name: 'ProjectViewWithId',
      component: () => import(/* webpackChunkName: "ProjectView" */ './views/ProjectView.vue'),
    },
    {
      path: '/DeliverableView',
      name: 'DeliverableView',
      // route level code-splitting
      // this generates a separate chunk (DeliverableView.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "DeliverableView" */ './views/DeliverableView.vue')
    }
  ]
})
