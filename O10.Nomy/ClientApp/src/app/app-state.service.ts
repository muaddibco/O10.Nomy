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
    this.state.isMobile = isMobile
    this.StateChanged.emit(this.state)
  }
}

class AppState {
  isMobile: boolean
}
