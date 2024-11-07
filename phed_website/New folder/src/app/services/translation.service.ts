import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TranslateLoader, TranslateService, LangChangeEvent } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class TranslationService implements TranslateLoader {

  constructor(private http: HttpClient, private translate: TranslateService) {
    const language = this.getLanguage();
    this.translate.setDefaultLang(language);
    this.translate.use(language);
  }

  // Method to set the language
  setLanguage(lang: string) {
    this.translate.use(lang);
    localStorage.setItem('language', lang); // Store language preference
  }

  // Method to get the current language
  getLanguage(): string {
    return localStorage.getItem('language') || 'en'; // Default to English if no preference is set
  }

  // Method to expose the language change event as an observable
  onLanguageChange(): Observable<LangChangeEvent> {
    return this.translate.onLangChange;  // Return the language change observable
  }

  // Fetch a translation by key
  getTranslationByKey(key: string): Observable<any> {
    return this.translate.get(key);
  }

  // Fetch translation file based on the selected language
  getTranslation(lang: string): Observable<any> {    
    return this.http.get(`/assets/i18n/${lang}.json`);
  }

  // New method to expose the instant translation method
  instant(key: string): string {
    return this.translate.instant(key);
  }
}
