import { Routes } from '@angular/router';
import { HomePage } from './home-page/home-page';
import { About } from './about/about';
import { NotFound } from './not-found/not-found';
import { GestaoArtigos } from './gestao-artigos/gestao-artigos';

export const routes: Routes = [
  { path: '', component: HomePage, title: 'Home' },
  { path: 'gestao', component: GestaoArtigos, title: 'Gestao' },
  { path: 'about', component: About, title: 'About Us' },
  { path: '**', component: NotFound, title: ' try again noob' },
];
