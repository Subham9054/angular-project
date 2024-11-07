import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VisitorService {
  private apiUrl = 'http://localhost:5097/api/Home';

  constructor(private http: HttpClient) { }

  // Get the current visitor count
  getVisitorCount(): Observable<number> {
    return this.http.get<any>(`${this.apiUrl}/GetVisitorCounts`).pipe(
      // Assuming we want the latest count, access the first item in the data array
      map(response => response.data[0]?.count || 0)
    );
  }

  // Increment the visitor count
  incrementVisitorCount(): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/InsertVisitorsCount`, {});
  }
}
