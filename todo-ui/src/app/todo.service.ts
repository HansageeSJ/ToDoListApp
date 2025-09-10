import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environments';
import { Observable } from 'rxjs';
import { TodoItem } from './todo-item.model';

@Injectable({ providedIn: 'root' })
export class TodoService {
  private http = inject(HttpClient);
  private base = `${environment.apiUrl}/todolist`;

  getAll(): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]>(this.base);
  }

  add(title: string): Observable<TodoItem> {
    return this.http.post<TodoItem>(this.base, { title });
  }

  update(id: string, isDone: boolean): Observable<void> {
    return this.http.put<void>(`${this.base}/${id}`, { isDone });
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.base}/${id}`);
  }
}
