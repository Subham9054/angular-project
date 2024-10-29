import { Component } from '@angular/core';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';

@Component({
  selector: 'app-pagecontent',
  templateUrl: './pagecontent.component.html',
  styleUrls: ['./pagecontent.component.scss']
})
export class PagecontentComponent {
  editor = ClassicEditor;
  data: any = `<p class="text-grey">Enter here...</p>`;
}
