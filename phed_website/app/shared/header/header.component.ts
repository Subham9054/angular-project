import { AfterViewInit, Component, HostListener } from '@angular/core';
import { ViewportScroller } from '@angular/common';
declare var $: any;
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent  implements AfterViewInit {
	
  isLightTheme: boolean = false;
	isDarkTheme: boolean = false;
	fontSize = 20;
	clickLimit = 3;
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
	
	constructor(private viewportScroller: ViewportScroller) {}
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
		if (this.ClickCount >= this.clickLimit) {

			return false;
		}
		else {
			this.fontSize = (this.fontSize + 1);
			// document.body.style.fontSize = this.fontSize + 'px';
			$("body").css({ "font-size": this.fontSize + 'px' });
			this.ClickCount++;
			return true;
		}

	}
	DecreaseFontSize() {
		if (this.ClickCountminus >= this.clickLimit) {
			return false;
		}
		else {
			this.fontSize = (this.fontSize - 1);
			$("body").css({ "font-size": this.fontSize + 'px' });
			this.ClickCountminus++;
			return true;
		}

	}

	ResetFontSize() {
		$("body").css({ "font-size": '18px' })
		//console.log("sdghesd");
		this.fontSize = 16;
		this.ClickCount = 0;
		this.ClickCountminus = 0;
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

ngAfterViewInit() {
    this.prevScrollPos = window.scrollY;
	 // Set up scroll event listener
    window.addEventListener('scroll', this.onWindowScroll.bind(this));
  }
  @HostListener('window:scroll', [])
  
  onWindowScroll() {
    const currentScroll = window.scrollY;
	this.isAtTop = currentScroll === 0;
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

}
