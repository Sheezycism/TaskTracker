
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ToDoTask } from '../models/tasks';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private apiUrl = `${environment.apiUrl}/tasks`;

  constructor(private http: HttpClient) {}

  getTasks(q?: string, sort: string = 'duedate:asc'): Observable<ToDoTask[]> {
    let params = new HttpParams().set('sort', sort);
    if (q) params = params.set('q', q);
    return this.http.get<ToDoTask[]>(this.apiUrl, { params });
  }

  getTask(id: number): Observable<ToDoTask> {
    return this.http.get<ToDoTask>(`${this.apiUrl}/${id}`);
  }

  createTask(task: ToDoTask): Observable<ToDoTask> {
    return this.http.post<ToDoTask>(this.apiUrl, task);
  }

  updateTask(id: number, task: ToDoTask): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, task);
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}