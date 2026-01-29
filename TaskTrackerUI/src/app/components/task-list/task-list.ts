import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { ToDoTask } from '../../models/tasks';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../services/task';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './task-list.html',
  styleUrl: './task-list.css',
})
export class TaskListComponent implements OnInit {
  tasks: ToDoTask[] = [];
  loading = false;
  error = '';
  searchQuery = '';
  sortOrder = 'duedate:asc';
  private cdr = inject(ChangeDetectorRef);
  constructor(private taskService: TaskService) {}

  ngOnInit(): void { this.loadTasks(); }

  loadTasks() {
  this.loading = true;
  this.taskService.getTasks(this.searchQuery, this.sortOrder).subscribe({
    next: (res) => { 
      this.tasks = res; 
      this.loading = false; 
      this.cdr.detectChanges(); // 3. Manually trigger UI update
    },
    error: (err) => { 
      this.error = err.message;
      this.loading = false; 
      this.cdr.detectChanges(); 
    }
  });
}

  onDelete(id: number) {
    if (confirm('Are you sure?')) {
      this.taskService.deleteTask(id).subscribe(() => this.loadTasks());
    }
  }

  // Add these properties to your class
taskToDeleteId: number | null = null;

// Triggered when clicking 'Delete' in the table
openDeleteModal(id: number) {
  this.taskToDeleteId = id;
}

// Triggered when clicking 'Confirm' in the modal
confirmDelete() {
  if (this.taskToDeleteId) {
    this.taskService.deleteTask(this.taskToDeleteId).subscribe({
      next: () => {
        this.loadTasks(); // Refresh list
        this.taskToDeleteId = null; // Close modal
      },
      error: (err) => {
        this.error = err.error?.detail || 'Failed to delete task.';
        this.taskToDeleteId = null;
      }
    });
  }
}


}
