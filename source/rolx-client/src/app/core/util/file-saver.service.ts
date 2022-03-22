import { Injectable } from '@angular/core';
import { NamedBlob } from '@app/core/util/named-blob';

@Injectable({
  providedIn: 'root',
})
export class FileSaverService {
  private readonly dummyAnchor = document.createElement('a');

  constructor() {
    document.body.appendChild(this.dummyAnchor);
    this.dummyAnchor.setAttribute('style', 'display: none');
  }

  save(namedBlob: NamedBlob): void {
    this.dummyAnchor.href = window.URL.createObjectURL(namedBlob.blob);
    this.dummyAnchor.download = namedBlob.name;
    this.dummyAnchor.click();
  }
}
