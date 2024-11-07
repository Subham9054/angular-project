import { Component } from '@angular/core';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';

@Component({
  selector: 'app-mnggallery',
  templateUrl: './mnggallery.component.html',
  styleUrls: ['./mnggallery.component.scss']
})
export class MnggalleryComponent {
  // select option
  selectedType: string | null = null;
  
  editor = ClassicEditor;
  data: any = `<p class="text-grey">Enter here...</p>`;
}
