import { X } from "lucide-react";

function Modal({ isOpen, onClose, children }) {
  if (!isOpen) return null;

  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-black/50"
      onClick={onClose}
    >
      <div
        className="
          relative
          w-full h-full
          sm:h-auto sm:max-h-[90vh] sm:max-w-lg
          bg-surface text-foreground
          shadow-2xl
          p-5 sm:p-8
          sm:rounded-2xl
          overflow-y-auto
        "
        onClick={(e) => e.stopPropagation()}
      >
        <button
          type="button"
          onClick={onClose}
          className="absolute right-3 top-3 sm:right-4 sm:top-4 btn btn-ghost btn-circle bg-accent-color"
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
