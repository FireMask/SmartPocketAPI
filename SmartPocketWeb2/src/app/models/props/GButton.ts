export enum GButtonSize {
  Small = 'px-3',
  Medium = 'px-6',
  Large = 'px-9',
  ExtraLarge = 'px-12'
}

export interface GButton {
  label?: string;
  icon?: string;
  buttonSize?: GButtonSize;
  style: string;
  action: () => void;
}