import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  private loadingSubject = new BehaviorSubject<boolean>(false);
  public isLoading = this.loadingSubject.asObservable();

  constructor() {}

  // Use this method to trigger loading
  startLoading() {
    this.loadingSubject.next(true);
  }

  // Use this method to stop loading
  stopLoading() {
    this.loadingSubject.next(false);
  }
}
