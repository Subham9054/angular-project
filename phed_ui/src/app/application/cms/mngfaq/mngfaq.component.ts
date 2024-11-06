import { Component } from '@angular/core';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
@Component({
  selector: 'app-mngfaq',
  templateUrl: './mngfaq.component.html',
  styleUrls: ['./mngfaq.component.scss']
})
export class MngfaqComponent {
  editor = ClassicEditor;
  data: any = `<p class="text-grey">Enter here...</p>`;
}
