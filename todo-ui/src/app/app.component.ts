import { Component, computed, effect, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { TodoService } from './todo.service';
import { TodoItem } from './todo-item.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  template: `
  <div class="container">
    <h1>TODOs</h1>

    <form (ngSubmit)="add()" class="add-form">
      <input [(ngModel)]="newTitle" name="title" placeholder="Add a task..." required maxlength="200" />
      <button type="submit">Add</button>
    </form>
    
    <ul class="list">
      <li *ngFor="let item of todos()">
        <label>
          <input type="checkbox" [checked]="item.isDone" (change)="toggle(item)" />
          <span [class.done]="item.isDone">{{ item.title }}</span>
        </label>
        <button class="delete" (click)="remove(item)">Delete</button>
      </li>
    </ul>

    <p *ngIf="todos().length === 0" class="empty">No items yet — add one!</p>
  </div>
  `,
  styles: [`
    .container { max-width: 640px; margin: 2rem auto; padding: 1rem; }
    h1 { margin-bottom: 1rem; }
    .add-form { display: flex; gap: .5rem; margin-bottom: 1rem; }
    .add-form input { flex: 1; padding: .5rem; }
    .list { list-style: none; padding: 0; display: grid; gap: .5rem; }
    li { display: flex; align-items: center; justify-content: space-between; border: 1px solid #ddd; border-radius: 8px; padding: .5rem .75rem; }
    label { display: flex; align-items: center; gap: .5rem; flex: 1; }
    .done { text-decoration: line-through; opacity: .6; }
    .delete { background: transparent; border: 1px solid #ccc; border-radius: 6px; padding: .25rem .5rem; cursor: pointer; }
    .empty { opacity: .7; }
  `]
})
export class AppComponent {
  private api = inject(TodoService);
  todos = signal<TodoItem[]>([]);
  newTitle = '';

  constructor() {
    effect(() => this.refresh());
  }

  refresh() {
    this.api.getAll().subscribe(items => this.todos.set(items));
  }

  add() {
    const title = this.newTitle.trim();
     console.log("Title check:", title);
    if (!title) return;
    this.api.add(title).subscribe(created => {
      this.todos.update(list => [...list, created]);
      this.newTitle = '';
       console.log("Adding todo:", title);
    });
  }

  toggle(item: TodoItem) {
    this.api.update(item.id, !item.isDone).subscribe(() => {
      this.todos.update(list => list.map(x => x.id === item.id ? { ...x, isDone: !x.isDone } : x));
    });
  }

  remove(item: TodoItem) {
    this.api.delete(item.id).subscribe(() => {
      this.todos.update(list => list.filter(x => x.id !== item.id));
    });
  }
}
