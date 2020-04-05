import { Pipe, PipeTransform, InjectionToken } from '@angular/core';
import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { By } from '@angular/platform-browser';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from '@angular/platform-browser-dynamic/testing';
import { of } from 'rxjs';
import { PetComponent } from './pet.component';
import { PetService } from './pet.service';

@Pipe({ name: 'orderby' })
class MockOrderByPipe implements PipeTransform {
  public transform(data: Array<any>, orderByKey: any, ascending: boolean = true){
    return data;
  }
}
export const BASE_URL = new InjectionToken<string>('BASE_URL');

describe('PetComponent', () => {
  let component: PetComponent;
  let fixture: ComponentFixture<PetComponent>;

  const mockPetService = {
    getPets(petType: string) {
      return of({ "petsByOwnerGender": { "Male": [{ "name": "Jim", "type": 1 }], "Female": [{ "name": "Garfield", "type": 1 }, { "name": "Tabby", "type": 1 }] } });
    }
  };

  beforeEach(async(() => {
    TestBed.resetTestEnvironment();
    TestBed.initTestEnvironment(
      BrowserDynamicTestingModule,
      platformBrowserDynamicTesting()
    );
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        HttpModule
      ],
      declarations: [
        MockOrderByPipe,
        PetComponent
      ],
      providers: [
        {
          provide: PetService,
          useValue: mockPetService
        },
        {
          provide: BASE_URL,
          useValue: 'http://localhost'
        }
      ],
    }).compileComponents().then(() => {
      fixture = TestBed.createComponent(PetComponent);
      component = fixture.debugElement.componentInstance;
    });
  }));

  it('should create the Pet Component', () => {
    expect(component).toBeTruthy();
  });

  it('should render correct title', () => {
    let titleDiv = fixture.debugElement.query(By.css('#title'));
    fixture.detectChanges();

    expect(titleDiv.nativeElement.innerHTML).toContain('List of cats in alphabetical order under a heading of the gender of their owner');
  });

  it('should render owner gender in H5', () => {
    const petsSpyObj = jasmine.createSpyObj({
      afterClosed: of({ "petsByOwnerGender": { "Male": [{ "name": "Jim", "type": 1 }], "Female": [{ "name": "Garfield", "type": 1 }, { "name": "Tabby", "type": 1 }] } })
    });
    const petService: PetService = fixture.debugElement.injector.get(PetService);
    spyOn(petService, 'getPets').and.returnValue(petsSpyObj);

    fixture.whenStable().then(() => {
      const compiled = fixture.debugElement.nativeElement;
      let h5Elements = compiled.querySelectorAll('H5');
      expect(h5Elements[0].textContent).toContain('Male');
      expect(h5Elements[1].textContent).toContain('Female');
    });
  });

  it('should render 1 cat with Male owner', () => {
    const petsSpyObj = jasmine.createSpyObj({
      afterClosed: of({ "petsByOwnerGender": { "Male": [{ "name": "Jim", "type": 1 }], "Female": [{ "name": "Garfield", "type": 1 }, { "name": "Tabby", "type": 1 }] } })
    });
    const petService: PetService = fixture.debugElement.injector.get(PetService);
    spyOn(petService, 'getPets').and.returnValue(petsSpyObj);
    
    fixture.whenStable().then(() => {
      const compiled = fixture.debugElement.nativeElement;
      let h5Elements = compiled.querySelectorAll('H5');
      expect(h5Elements[0].getElementsByTagName("li").length).toEqual(1);
    });
  });

  it('should render 2 cats with Female owners', () => {
    const petsSpyObj = jasmine.createSpyObj({
      afterClosed: of({ "petsByOwnerGender": { "Male": [{ "name": "Jim", "type": 1 }], "Female": [{ "name": "Garfield", "type": 1 }, { "name": "Tabby", "type": 1 }] } })
    });
    const petService: PetService = fixture.debugElement.injector.get(PetService);
    spyOn(petService, 'getPets').and.returnValue(petsSpyObj);
    
    fixture.whenStable().then(() => {
      const compiled = fixture.debugElement.nativeElement;
      let h5Elements = compiled.querySelectorAll('H5');
      expect(h5Elements[1].getElementsByTagName("li").length).toEqual(2);
    });
  });

});
