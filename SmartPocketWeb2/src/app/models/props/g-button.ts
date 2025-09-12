export enum GButtonSize {
  Small = 'px-3',
  Medium = 'px-6',
  Large = 'px-9',
  ExtraLarge = 'px-12'
}

export interface GButton {
  id: number;
  label?: string;
  icon?: string;
  buttonSize?: GButtonSize;
  selected?: boolean;
  selectedStyle?: string;
  style: string;
  action: () => void;
}