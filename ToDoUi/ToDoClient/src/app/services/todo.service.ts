import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ToDoItem, CreateToDoDto, UpdateToDoDto } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private apiUrl = 'http://localhost:5007/api/todos';

  constructor(private http: HttpClient) { }

  getAllToDos(): Observable<ToDoItem[]> {
    return this.http.get<ToDoItem[]>(this.apiUrl);
  }

  getToDoById(id: number): Observable<ToDoItem> {
    return this.http.get<ToDoItem>(`${this.apiUrl}/${id}`);
  }

  getToDosByTaskId(taskId: number): Observable<ToDoItem[]> {
    const params = new HttpParams().set('taskId', taskId.toString());
    return this.http.get<ToDoItem[]>(this.apiUrl, { params });
  }

  createToDo(todo: CreateToDoDto): Observable<ToDoItem> {
    return this.http.post<ToDoItem>(this.apiUrl, todo);
  }

  updateToDo(id: number, todo: UpdateToDoDto): Observable<ToDoItem> {
    return this.http.put<ToDoItem>(`${this.apiUrl}/${id}`, todo);
  }

  deleteToDo(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
