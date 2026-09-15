"use client";

import "./sketch.css";

import {
  type ReactNode,
  useContext,
  useId,
  useLayoutEffect,
  useMemo,
  useState,
} from "react";
import rough from "roughjs";
import {
  createRoundedRectanglePath,
  createSeed,
  createUnderlinePath,
  createVerticalLinePath,
  DEFAULT_SEED,
  DEFAULT_STROKE_WIDTH,
  getBorderRadius,
  getCssSketchOptions,
  getCssSketchSeed,
  getPaddingBoxSize,
  MIN_STROKE_WIDTH,
  SketchContext,
  type SketchOutline,
  type SketchOutlineOptions,
  type SketchScope,
  type SketchShape,
  type SketchTheme,
} from "./utils/sketch";

export type SketchProviderProps = {
  children: ReactNode;
  seed?: number;
};

export { PAPER_VARIANTS } from "./utils/sketch";
export type { PaperVariant, SketchOutline, SketchOutlineOptions, SketchScope, SketchShape } from "./utils/sketch";

export function SketchProvider({ children, seed = DEFAULT_SEED }: SketchProviderProps) {
  const value = useMemo(() => ({ seed }), [seed]);

  return <SketchContext.Provider value={value}>{children}</SketchContext.Provider>;
}


/**
 * Draws a RoughJS outline into an `<svg>` overlaying the parent element.
 *
 * @param options RoughJS options plus `shape`, `opacity`, and `id`.
 * `id` seeds the wobble and defaults to `useId()`. Because `useId()` shifts
 * with tree position, pass a stable `id` when the drawn geometry must not
 * change, such as shared shapes across instances or visual regression
 * snapshots.
 * @returns The `ref` and `style` to spread onto the outline `<svg>`.
 */
export function useSketchOutline(
  options: SketchOutlineOptions = {},
  scope: SketchScope = "outline",
): SketchOutline {
  const { id, opacity, shape = "rectangle", ...roughOptions } = options;
  const theme = useSketch();
  const [svg, setSvg] = useState<SVGSVGElement | null>(null);
  const fallbackId = useId();
  const instanceId = id ?? fallbackId;

  useLayoutEffect(() => {
    const target = svg?.parentElement;

    if (!svg || !target) {
      return;
    }

    let frame = 0;
    const draw = () => {
      const { height, width } = getPaddingBoxSize(target);

      if (width <= 0 || height <= 0) {
        return;
      }

      const drawing = rough.svg(svg);
      const drawingOptions = {
        ...getCssSketchOptions(target, scope),
        ...roughOptions,
        seed: createSeed(getCssSketchSeed(target, scope) ?? theme.seed, instanceId),
      };
      const strokeWidth = Math.max(
        MIN_STROKE_WIDTH,
        drawingOptions.strokeWidth ?? DEFAULT_STROKE_WIDTH,
      );
      const path = getSketchPath(shape, target, width, height, strokeWidth);
      const drawn = drawing.path(path, { ...drawingOptions, strokeWidth });

      svg.setAttribute("viewBox", `0 0 ${width} ${height}`);
      svg.replaceChildren(drawn);
    };

    const scheduleDraw = () => {
      cancelAnimationFrame(frame);
      frame = requestAnimationFrame(draw);
    };

    const observer = new ResizeObserver(scheduleDraw);
    observer.observe(target);
    const attributeObserver = new MutationObserver(scheduleDraw);
    attributeObserver.observe(target, {
      attributeFilter: ["class", "style"],
      attributes: true,
    });
    scheduleDraw();

    return () => {
      cancelAnimationFrame(frame);
      attributeObserver.disconnect();
      observer.disconnect();
    };
  }, [instanceId, scope, shape, svg, theme.seed]);

  return {
    ref: setSvg,
    style: {
      height: "100%",
      left: 0,
      opacity,
      // Rough's wobble swings past the viewBox; the default svg clip shaves it to a hairline.
      overflow: "visible",
      pointerEvents: "none",
      position: "absolute",
      top: 0,
      width: "100%",
    },
  };
}

export function useSketchBg(options: SketchOutlineOptions = {}): SketchOutline {
  return useSketchOutline(options, "bg");
}

function getSketchPath(
  shape: SketchShape,
  target: Element,
  width: number,
  height: number,
  strokeWidth: number,
): string {
  if (shape === "underline") {
    return createUnderlinePath(width, height, strokeWidth);
  }

  if (shape === "vertical-line") {
    return createVerticalLinePath(width, height, strokeWidth);
  }

  return createRoundedRectanglePath(width, height, getBorderRadius(target), strokeWidth);
}

function useSketch(): SketchTheme {
  const theme = useContext(SketchContext);

  if (theme === null) {
    throw new Error("Sketch components must be rendered inside SketchProvider.");
  }

  return theme;
}
