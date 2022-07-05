import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/IMember';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css'],
})
export class PhotoEditorComponent implements OnInit {
  @Input() memeber: Member;

  constructor() {}

  ngOnInit(): void {}
}
