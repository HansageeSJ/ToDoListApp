// 
import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TodoService } from './todo.service';
import { environment } from '../environments/environments';
import { TodoItem } from './todo-item.model';

describe('TodoService', () => {
  let svc: TodoService;
  let http: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TodoService],
    });
    svc = TestBed.inject(TodoService);
    http = TestBed.inject(HttpTestingController);
  });

  afterEach(() => http.verify());

  it('loads list', () => {
    const mock: TodoItem[] = [];
    svc.getAll().subscribe(res => expect(res).toEqual(mock));
    const req = http.expectOne(`${environment.apiUrl}/todos`);
    req.flush(mock);
  });
});
