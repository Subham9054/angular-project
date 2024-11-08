import { Component, OnInit } from '@angular/core';
import { LoadingService } from '../loading.service';  // Adjust path as necessary
import { Observable } from 'rxjs';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent implements OnInit {
  isLoading!: Observable<boolean>;

  constructor(private loadingService: LoadingService) {}

  ngOnInit(): void {
    this.isLoading = this.loadingService.isLoading;
  }
}
