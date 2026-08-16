import { Routes } from '@angular/router';
import { HomePage } from './home-page/home-page';
import { About } from './about/about';
import { NotFound } from './not-found/not-found';

export const routes: Routes = [
  { path: '', component: HomePage, title: 'Home' },
  { path: 'about', component: About, title: 'About Us' },
  { path: '**', component: NotFound, title: ' try again noob' },
];
