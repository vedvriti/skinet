import { HttpInterceptorFn } from '@angular/common/http';
import { delay, finalize } from 'rxjs/operators';
import { BusyService } from '../services/busy.service';
import { inject } from '@angular/core';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService)
  //loading
  busyService.busy();
  //after the request is received we are delaying the loading by seconds and thn finalize means the content is loaded
  return next(req).pipe(
    delay(500),
    finalize(() => busyService.idle() )
  );
};