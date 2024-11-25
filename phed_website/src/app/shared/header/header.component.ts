import { AfterViewInit, Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { ViewportScroller } from '@angular/common';
import { TranslationService } from 'src/app/services/translation.service';
import { HomeService } from 'src/app/services/home.service';

declare var $: any;
declare var bootstrap: any;

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent  implements AfterViewInit {	
	currentLang: 'en' | 'hi' = 'en';
	isLightTheme: boolean = false;
	isDarkTheme: boolean = false;
	fontSize = 20;
	clickLimit: number = 3;
	ClickCount = 0;
	ClickCountminus = 0;
	prevScrollPos: number = 0;
	isOpen: boolean = false;
	scroller: any;
	previousScroll = 0;
	hideTopHeader = false; // Control visibility of .topheader
	isScrollingUp = false; // Track scrolling direction
	isHidden = false; // Initially false
	isAtTop = true; // Track if at the top
	isExpanded = false;
	elementRef: any;
	isMenuOpen = false;
	
	constructor(
		private translationService: TranslationService,
		private homeService: HomeService,
		private viewportScroller: ViewportScroller
	) {}

	ngOnInit(): void {
		// Theme start
		if (localStorage.getItem('theme') === 'theme-dark') {
			this.setTheme('theme-dark');
			this.isDarkTheme = true;
		}
		else {
			this.setTheme('theme-light');
			this.isLightTheme = true;
		}
		// Load the language preference if available, with type checking
		const savedLang = this.translationService.getLanguage();
    	this.currentLang = savedLang === 'en' || savedLang === 'hi' ? savedLang : 'en';
    	this.translationService.setLanguage(this.currentLang);

		// Theme start
		if (localStorage.getItem('theme') === 'theme-dark') {
			this.setTheme('theme-dark');
			this.isDarkTheme = true;
		}
		else {
			this.setTheme('theme-light');
			this.isLightTheme = true;
		}
 	}

	setTheme(themeName: any) {
		localStorage.setItem('theme', themeName);
		document.body.className = themeName;
	}

	lightThemeBtn() {
		if (localStorage.getItem('theme') === 'theme-dark' || 'theme-green') {
			this.setTheme('theme-light');
			setTimeout(() => {
				this.isLightTheme = true;
				this.isDarkTheme = false;
			}, 100);

		}
	}
	
	darkThemeBtn() {
		if (localStorage.getItem('theme') === 'theme-light' || 'theme-green') {
			this.setTheme('theme-dark');
			setTimeout(() => {
				this.isDarkTheme = true;
				this.isLightTheme = false;
			}, 100);
		}
	}

	//Font Size Start
	IncreaseFontSize() {
		if (this.ClickCount < this.clickLimit) {
			this.fontSize++;
			this.updateFontSize();
			this.ClickCount++;
		}
	}

	DecreaseFontSize() {
		if (this.ClickCountminus < this.clickLimit) {
			this.fontSize--;
			this.updateFontSize();
			this.ClickCountminus++;
		}
	}

	ResetFontSize() {
		this.fontSize = 18; // Reset to initial size
		this.ClickCount = 0;
		this.ClickCountminus = 0;
		this.updateFontSize();
	}

	private updateFontSize() {
		// Apply the font size to the body
		document.body.style.fontSize = this.fontSize + 'px';
	}
	//Font Size End

	//search area Start
	toggleSearch() {
		this.isOpen = !this.isOpen;
		this.isExpanded = !this.isExpanded;
	}
	//search area End


	//Skip content End
	goToMainContent() {
		this.scroller.scrollToAnchor("targetMainContent");
	}

	scrollToSection(sectionId: string) {
		this.viewportScroller.scrollToAnchor(sectionId);
	}
	//Skip content End

	// Toggle language between English and Hindi
	toggleLanguage() {
		this.currentLang = this.currentLang === 'en' ? 'hi' : 'en';
		this.translationService.setLanguage(this.currentLang);
		//this.loadHeaderMenus();
		//this.loadMenus();
	}

	ngAfterViewInit() {
		this.prevScrollPos = window.scrollY;
		// Set up scroll event listener
		window.addEventListener('scroll', this.onWindowScroll.bind(this));
	}
	
	@HostListener('window:scroll', [])
	@HostListener('document:click', ['$event'])
	
	onWindowScroll() {
		const currentScroll = window.scrollY;
		const scrollTop = window.pageYOffset || document.documentElement.scrollTop;
		// this.isAtTop = currentScroll === 0;
		this.isAtTop = scrollTop === 0;
		
		if (currentScroll > this.previousScroll) {
		// Scrolling down
		this.hideTopHeader = true;
		this.isScrollingUp = false;
		this.isHidden = true; // Add is-hidden class
		this.isScrollingUp = false; // Reset scrolling up
		
		} else if (currentScroll < this.previousScroll) {
			this.isHidden = false; // Remove is-hidden class
			this.isScrollingUp = true; // Set scrolling up
		// Scrolling up
		this.isScrollingUp = true;
		if (currentScroll === 0) {
			this.hideTopHeader = false; // Show .topheader when at the top
			
		}
		if (this.isAtTop) {
			this.isHidden = false; // Ensure visible
		}
	}
		// Update the previous scroll position
		this.previousScroll = currentScroll;		
	}

	//   Mega Menu Start
	@ViewChild('megaMenu', { static: false }) megaMenu!: ElementRef;
	toggleMenu() {
		this.isMenuOpen = !this.isMenuOpen;
		console.log('Menu toggled:', this.isMenuOpen);
	}
	
	onClick(event: MouseEvent) {
		console.log('Document clicked');
		const clickedInside = this.megaMenu.nativeElement.contains(event.target);
		console.log('Clicked inside menu:', clickedInside);
		if (!clickedInside && this.isMenuOpen) {
		this.isMenuOpen = false;
		console.log('Menu closed');
		}
	}
	//   Mega Menu End

	closeOffcanvas() {
		const offcanvasElement = document.getElementById('offcanvasNavbar2');
		const offcanvas = bootstrap.Offcanvas.getInstance(offcanvasElement);
		
		if (offcanvas) {
		offcanvas.hide();
		}
	}

	
}
