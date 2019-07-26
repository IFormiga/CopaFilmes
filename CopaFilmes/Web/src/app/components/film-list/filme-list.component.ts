import { Component, OnInit, OnDestroy } from '@angular/core';
import { Filme } from '../../models/Filme';
import { trigger, transition, style, animate } from '@angular/animations';
import { FilmListService } from './film-list.service';
import { Observable, Subscription } from 'rxjs';
import { ResultadoCampeonato } from 'src/app/models/ResultadoCampeonato';

@Component({
  selector: 'filme-list',
  templateUrl: './filme-list.component.html',
  styleUrls: ['./filme-list.component.scss'],
  animations: [
    trigger('fade', [

      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(30px)' }),
        animate(200, style({ opacity: 1, transform: 'translateY(0px)'}))
      ]),

      transition(':leave', [
        animate(200, style({ opacity: 0, transform: 'translateY(30px)' }))
      ]),

    ])
  ]
})
export class FilmeListComponent implements OnInit, OnDestroy {
  filmes: Filme[] = [];
  tituloFilme: string;
  idForFilme: number;
  beforeEditCache: string;
  filter: string;
  anyRemainingModel: boolean;
  unSubFilmes: Subscription;
  resultadoCampeonato: ResultadoCampeonato;


  constructor(
    private filmeService: FilmListService
  ) {}

  ngOnInit() {

    // var x = this.filmeService.obterTodosFilmes();
    this.anyRemainingModel = true;
    this.filter = 'all';
    this.beforeEditCache = '';
    this.idForFilme = 4;
    this.tituloFilme = '';
    this.unSubFilmes = this.filmeService.obterTodosFilmes().subscribe(res => {
      this.filmes = res;
    });
  }

  QuantosSelecionados(): number {
    return this.filmes.filter(filme => filme.completed).length;
  }

  ExisteResultado(): boolean{
    return this.resultadoCampeonato != null;
  }

  aoMenosUmSelecionado(): boolean {
    return this.filmes.filter(filme => filme.completed).length > 0;
  }

  clearCompleted(): void {
    this.filmes.forEach(filme => filme.completed = false);
  }

  LimiteSelecionado(): boolean {
    return this.QuantosSelecionados() < 8;
  }

  GerarCampeonato(): void {
    const filmesCompleted = this.getChavesFilmesCompleted(this.filmes)
    this.filmeService.gerarCampeonato(filmesCompleted)
    .subscribe(res => {
      this.resultadoCampeonato = res;
      console.log(this.resultadoCampeonato);
    });
  }

  getChavesFilmesCompleted(filmes: Filme[]) {
    return filmes.filter(f => {
      return f.completed
    }).map(filmes => {
      return filmes.filme.chave;
    })
  }

  filmesFiltrados(): Filme[] {
    if (this.filter === 'all') {
      return this.filmes;
    } else if (this.filter === 'active') {
      return this.filmes.filter(filme => !filme.completed);
    } else if (this.filter === 'completed') {
      return this.filmes.filter(filme => filme.completed);
    }

    return this.filmes;
  }

  ngOnDestroy() {
    this.unSubFilmes.unsubscribe();
  }

}

