import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InnerSubscriber } from 'rxjs/internal/InnerSubscriber';
import { Filme } from 'src/app/models/Filme';
import { ResultadoCampeonato } from 'src/app/models/ResultadoCampeonato';

export function criarParametrosHTTP(params: {}): HttpParams {
  let httpParams: HttpParams = new HttpParams();
  Object.keys(params).forEach(param => {
      if (params[param]) {
          httpParams = httpParams.set(param, params[param]);
      }
  });

  return httpParams;
}

@Injectable({
  providedIn: 'root'
})
export class FilmListService {
  constructor(
    private http: HttpClient
  ){ }

  obterTodosFilmes(): Observable<Filme[]> {
    return this.http.get<Filme[]>("https://localhost:44385/api/filme");
  }

  gerarCampeonato(idsFilmes: string[]): Observable<ResultadoCampeonato>{
    return this.http.post<ResultadoCampeonato>("https://localhost:44385/api/campeonato", idsFilmes);
  }
}
