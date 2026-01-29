import { Component, OnInit, } from '@angular/core';
import { ToDoTask } from '../../models/tasks';
import { TaskService } from '../../services/task';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './task-form.html',
  styleUrl: './task-form.css',
})
export class TaskFormComponent implements OnInit {
  task: ToDoTask = { title: '', description: '', status: 0, priority: 1 };
  isEdit = false;
  loading = false;
  error = '';

  constructor(
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.isEdit = true;
      this.taskService.getTask(id).subscribe(data => this.task = data);
    }
  }

  save() {
  if (!this.task.title) {
    this.error = "Title is required.";
    return;
  }

  this.loading = true;
  this.error = '';

  // Use Observable<any> to bridge the gap between Create (Task) and Update (void)
  let action: Observable<any>;

  if (this.isEdit) {
    // Backend expects id and task object
    action = this.taskService.updateTask(this.task.id!, this.task);
  } else {
    // Backend creates new task
    action = this.taskService.createTask(this.task);
  }

  action.subscribe({
    next: () => {
      this.loading = false;
      this.router.navigate(['/']);
    },
    error: (err) => {
      // Accesses the ProblemDetails "detail" field from your API
      this.error = err.error?.detail || 'An error occurred while saving.';
      this.loading = false;
    }
  });
}
}
