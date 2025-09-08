import { Routes } from '@angular/router';
import { NotFound } from './components/not-found/not-found';
import { Dashboard } from './components/dashboard/dashboard';
import { Movements } from './components/movements/movements';

export const routes: Routes = [
    { path: '', component: Dashboard },
    { path: 'movements', component: Movements },
    { path: '**', component: NotFound },
];
