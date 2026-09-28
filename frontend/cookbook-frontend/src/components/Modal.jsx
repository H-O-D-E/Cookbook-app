import { X } from "lucide-react";

function Modal({ isOpen, onClose, children }) {
  if (!isOpen) return null;

  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 backdrop-blur-sm p-4"
      onClick={onClose}
    >
      <div
        className="relative w-full max-w-lg rounded-2xl bg-surface text-foreground shadow-2xl p-8"
        onClick={(e) => e.stopPropagation()}
      >
        <button
          type="button"
          onClick={onClose}
          className="absolute right-4 top-4 btn btn-ghost btn-circle bg-accent-color"
          aria-label="Close modal"
        >
          <X size={22} />
        </button>

        {children}
      </div>
    </div>
  );
}

export default Modal;
