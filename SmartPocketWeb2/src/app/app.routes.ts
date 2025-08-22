import { Routes } from '@angular/router';
import { NotFound } from './components/not-found/not-found';
import { Dashboard } from './components/dashboard/dashboard';

export const routes: Routes = [
    { path: '', component: Dashboard },
    { path: '**', component: NotFound },
];
