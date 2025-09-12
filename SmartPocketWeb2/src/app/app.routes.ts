import { Routes } from '@angular/router';
// import { NotFound } from './components/not-found/not-found';
// import { Dashboard } from './components/dashboard/dashboard';
// import { Movements } from './components/movements/movements';

export const routes: Routes = [
    { path: '', loadComponent: () => import('./components/dashboard/dashboard').then(m => m.Dashboard) },
    { path: 'movements', loadComponent: () => import('./components/movements/movements').then(m => m.Movements) },
    { path: '**', loadComponent: () => import('./components/not-found/not-found').then(m => m.NotFound) },
];
