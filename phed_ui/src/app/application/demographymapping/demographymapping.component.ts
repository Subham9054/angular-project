import { Component } from '@angular/core';

@Component({
  selector: 'app-demographymapping',
  templateUrl: './demographymapping.component.html',
  styleUrls: ['./demographymapping.component.scss']
})
export class DemographymappingComponent {
  // Original list of blocks
  blocks = [
    { value: '1', name: 'ARARIA' },
    { value: '2', name: 'ARWAL' },
    { value: '3', name: 'AURANGABAD' }
  ];
  
  // Bind selected items from the first select
  selectedBlocks: string[] = [];

  // Bind items moved to the second select
  selectedInSecondList: { value: string, name: string }[] = [];

  moveSelectedToSecond() {
    // Move selected items from the first select to the second
    this.selectedBlocks.forEach(selectedValue => {
      const index = this.blocks.findIndex(block => block.value === selectedValue);
      if (index !== -1) {
        this.selectedInSecondList.push(this.blocks[index]);
        this.blocks.splice(index, 1);
      }
    });
    // Clear selected items after moving
    this.selectedBlocks = [];
  }

  moveAllToSecond() {
    // Move all items from the first select to the second
    this.selectedInSecondList.push(...this.blocks);
    this.blocks = [];
  }
}
