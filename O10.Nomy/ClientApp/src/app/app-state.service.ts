import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {

  public StateChanged: EventEmitter<AppState> = new EventEmitter<AppState>();
  public state: AppState = {
    isMobile: false
  }

  constructor() {
  }

  public setIsMobile(isMobile: boolean) {
    var emit = this.state.isMobile !== isMobile
    this.state.isMobile = isMobile

    if (emit) {
      this.StateChanged.emit(this.state)
    }
  }
}

class AppState {
  isMobile: boolean
}
