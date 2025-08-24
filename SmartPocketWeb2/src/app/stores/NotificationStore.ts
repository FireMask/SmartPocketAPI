import { Injectable, signal, computed, DestroyRef, inject } from '@angular/core';
import { Toast } from '../models/notifications/notifications';

@Injectable({ providedIn: 'root' })
export class NotificationsStore {
  private destroyRef = inject(DestroyRef);

  // Internal state
  private _queue = signal<Toast[]>([]);
  private _nextId = 1;

  // Public, read-only selectors
  readonly select = {
    toasts: computed(() => this._queue()),
    hasToasts: computed(() => this._queue().length > 0),
  };

  // Commands
  success(message: string, opts: Partial<Omit<Toast, 'id' | 'message' | 'kind'>> = {}) {
    this.push({ kind: 'success', message, ...opts });
  }
  error(message: string, opts: Partial<Omit<Toast, 'id' | 'message' | 'kind'>> = {}) {
    this.push({ kind: 'error', message, ...opts });
  }
  info(message: string, opts: Partial<Omit<Toast, 'id' | 'message' | 'kind'>> = {}) {
    this.push({ kind: 'info', message, ...opts });
  }
  warning(message: string, opts: Partial<Omit<Toast, 'id' | 'message' | 'kind'>> = {}) {
    this.push({ kind: 'warning', message, ...opts });
  }

  dismiss(id: number) {
    this._queue.update(list => list.filter(t => t.id !== id));
  }
  clear() {
    this._queue.set([]);
  }

  // Core add with auto-dismiss
  private push(toast: Omit<Toast, 'id'>) {
    const id = this._nextId++;
    const durationMs = toast.durationMs ?? (toast.kind === 'error' ? 7000 : 4000);

    this._queue.update(list => {
      // Optional dedupe: prevent identical consecutive messages
      if (list[0] && list[0].message === toast.message && list[0].kind === toast.kind) return list;
      // Optional cap: max 5
      const trimmed = list.length >= 5 ? list.slice(1) : list;
      return [{ id, ...toast, durationMs }, ...trimmed];
    });

    // Auto-dismiss timer
    const timer = setTimeout(() => this.dismiss(id), durationMs);
    // Clear timer if app scope destroyed
    this.destroyRef.onDestroy(() => clearTimeout(timer));
  }
}
