import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'orderby'
})

export class OrderByPipe implements PipeTransform {
  transform(data: Array<any>, orderByKey: any, ascending: boolean = true) {
    return data.sort((prev: any, next: any): any => {
      if (prev[orderByKey] > next[orderByKey]) {
        return ascending ? true : false
      } else if (prev[orderByKey] < next[orderByKey]) {
        return ascending ? false : true
      } else {
        return 0;
      }
    });
  }
};
