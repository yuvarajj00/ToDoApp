import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { CreateTaskDto, UpdateTaskDto, TaskStatus } from '../../models/task.model';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-form.component.html',
  styleUrl: './task-form.component.css'
})
export class TaskFormComponent implements OnInit {
  isEditMode = false;
  taskId?: number;
  
  title: string = '';
  description: string = '';
  dueDate: string = '';
  status: string = 'New';
  
  taskStatuses = ['New', 'InProgress', 'Completed'];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private taskService: TaskService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.taskId = Number(id);
      this.loadTask(this.taskId);
    }
  }

  loadTask(id: number): void {
    this.taskService.getTaskById(id).subscribe({
      next: (task) => {
        this.title = task.title;
        this.description = task.description;
        this.dueDate = task.dueDate ? task.dueDate.split('T')[0] : '';
        this.status = task.status;
      },
      error: (error) => console.error('Error loading task:', error)
    });
  }

  onSubmit(): void {
    if (!this.title.trim()) {
      alert('Please enter a task title');
      return;
    }

    if (this.isEditMode && this.taskId) {
      this.updateTask();
    } else {
      this.createTask();
    }
  }

  createTask(): void {
    const newTask: CreateTaskDto = {
      title: this.title,
      description: this.description,
      dueDate: this.dueDate || undefined,
      status: this.status as TaskStatus
    };

    this.taskService.createTask(newTask).subscribe({
      next: (task) => {
        this.router.navigate(['/tasks', task.id]);
      },
      error: (error) => console.error('Error creating task:', error)
    });
  }

  updateTask(): void {
    if (!this.taskId) return;

    const updatedTask: UpdateTaskDto = {
      title: this.title,
      description: this.description,
      dueDate: this.dueDate || undefined,
      status: this.status as TaskStatus
    };

    this.taskService.updateTask(this.taskId, updatedTask).subscribe({
      next: () => {
        this.router.navigate(['/tasks', this.taskId]);
      },
      error: (error) => console.error('Error updating task:', error)
    });
  }

  cancel(): void {
    if (this.isEditMode && this.taskId) {
      this.router.navigate(['/tasks', this.taskId]);
    } else {
      this.router.navigate(['/tasks']);
    }
  }
}
