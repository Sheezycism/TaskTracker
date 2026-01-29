import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TaskService } from './task';
import { environment } from '../../environments/environment';

describe('TaskService', () => {
  let service: TaskService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TaskService]
    });
    service = TestBed.inject(TaskService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should surface the ProblemDetails detail message on a 400 error', (done: any) => {
  const mockErrorResponse = {
    type: 'https://tools.ietf.org/html/rfc9110#section-15.5.1',
    title: 'One or more validation errors occurred.',
    status: 400,
    detail: 'Title is required.'
  };

  service.createTask({ title: '', description: '', status: 0, priority: 1 }).subscribe({
    next: () => {
      fail('Should have failed with a 400 error');
      done();
    },
    error: (error) => {
      try {
        expect(error.error.detail).toBe('Title is required.');
        done(); 
      } catch (e) {
        done.fail(e); 
      }
    }
  });

  // Mock the HTTP backend
  const req = httpMock.expectOne(`${environment.apiUrl}/tasks`);
  
  req.flush(mockErrorResponse, { status: 400, statusText: 'Bad Request' });
});
  afterEach(() => {
    httpMock.verify();
  });
});

function fail(arg0: string) {
  throw new Error('Function not implemented.');
}
