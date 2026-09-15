import { createContext, type CSSProperties, type Ref } from "react";
import type { Options } from "roughjs/bin/core";

export const DEFAULT_SEED = 20_260_828;
export const DEFAULT_STROKE_WIDTH = 1.6;
export const MIN_STROKE_WIDTH = 1;

export const PAPER_VARIANTS = [
	"default",
	"polkadots",
	"hexagons",
	"graph",
	"plus",
	"filled-dots",
] as const;

export type PaperVariant = (typeof PAPER_VARIANTS)[number];

export type SketchTheme = {
	seed: number;
};

export const SketchContext = createContext<SketchTheme | null>(null);

export type SketchShape = "rectangle" | "underline" | "vertical-line";

export type SketchOutlineOptions = Omit<Partial<Options>, "seed"> & {
	borderRadius?: number;
	id?: string;
	opacity?: number;
	shape?: SketchShape;
};

export type SketchOutline = {
	ref: Ref<SVGSVGElement>;
	style: CSSProperties;
};

/**
 * Namespace for the `--sketch-*` custom properties a drawing reads.
 *
 * `"bg"` resolves `--sketch-bg-<name>` first and falls back to `--sketch-<name>`,
 * so a background inherits shared wobble settings while keeping its own fill.
 */
export type SketchScope = "outline" | "bg";

const CSS_NUMBER_OPTIONS = {
	bowing: "bowing",
	fillWeight: "fill-weight",
	hachureAngle: "hachure-angle",
	hachureGap: "hachure-gap",
	roughness: "roughness",
	strokeWidth: "stroke-width",
} as const;

const CSS_STRING_OPTIONS = {
	fill: "fill",
	fillStyle: "fill-style",
	stroke: "stroke",
} as const;

const CSS_BOOLEAN_OPTIONS = {
	disableMultiStroke: "disable-multi-stroke",
	preserveVertices: "preserve-vertices",
} as const;

export function createSeed(seed: number, instanceId: string): number {
	let hash = seed;

	for (let index = 0; index < instanceId.length; index += 1) {
		hash = (hash << 5) - hash + instanceId.charCodeAt(index);
		hash |= 0;
	}

	return (hash >>> 0) % 2_147_483_647 || 1;
}

export function getCssSketchOptions(
	target: Element,
	scope: SketchScope = "outline",
): Partial<Options> {
	const styles = getComputedStyle(target);
	const read = (name: string) => readScopedCssValue(styles, scope, name);
	const options: Record<string, unknown> = {};

	for (const [option, name] of Object.entries(CSS_NUMBER_OPTIONS)) {
		const value = Number.parseFloat(read(name));

		if (!Number.isNaN(value)) {
			options[option] = value;
		}
	}

	for (const [option, name] of Object.entries(CSS_STRING_OPTIONS)) {
		const value = read(name);

		if (value !== "") {
			options[option] = value;
		}
	}

	for (const [option, name] of Object.entries(CSS_BOOLEAN_OPTIONS)) {
		const value = read(name);

		if (value === "true" || value === "false") {
			options[option] = value === "true";
		}
	}

	return options as Partial<Options>;
}

export function getCssSketchSeed(
	target: Element,
	scope: SketchScope = "outline",
): number | undefined {
	/*
		The seed is read as a quoted string because CSS minifiers round bare
		numbers to six significant digits (20260828 ships as 20260800), which
		would change every wobble between dev and production. Bare numbers are
		still accepted for hand-written overrides that stay small.
	*/
	const raw = readScopedCssValue(getComputedStyle(target), scope, "seed").replace(
		/^["']|["']$/g,
		"",
	);
	const value = Number.parseFloat(raw);

	if (Number.isNaN(value)) {
		return undefined;
	}

	return value;
}

export function getBorderRadius(target: Element): number {
	return Number.parseFloat(getComputedStyle(target).borderTopLeftRadius) || 0;
}

export function getPaddingBoxSize(target: HTMLElement): { height: number; width: number } {
	const styles = getComputedStyle(target);

	return {
		height:
			target.offsetHeight -
			readPixels(styles.borderTopWidth) -
			readPixels(styles.borderBottomWidth),
		width:
			target.offsetWidth -
			readPixels(styles.borderLeftWidth) -
			readPixels(styles.borderRightWidth),
	};
}

function readPixels(value: string): number {
	return Number.parseFloat(value) || 0;
}

export function createRoundedRectanglePath(
	width: number,
	height: number,
	radius: number,
	strokeInset: number,
): string {
	const left = strokeInset;
	const top = strokeInset;
	const right = width - strokeInset;
	const bottom = height - strokeInset;
	const cornerRadius = Math.min(radius, (right - left) / 2, (bottom - top) / 2);

	if (cornerRadius === 0) {
		return `M ${left} ${top} H ${right} V ${bottom} H ${left} Z`;
	}

	return [
		`M ${left + cornerRadius} ${top}`,
		`H ${right - cornerRadius}`,
		`Q ${right} ${top} ${right} ${top + cornerRadius}`,
		`V ${bottom - cornerRadius}`,
		`Q ${right} ${bottom} ${right - cornerRadius} ${bottom}`,
		`H ${left + cornerRadius}`,
		`Q ${left} ${bottom} ${left} ${bottom - cornerRadius}`,
		`V ${top + cornerRadius}`,
		`Q ${left} ${top} ${left + cornerRadius} ${top}`,
		"Z",
	].join(" ");
}

export function createUnderlinePath(
	width: number,
	height: number,
	strokeInset: number,
): string {
	const bottom = height - strokeInset;

	return `M ${strokeInset} ${bottom} H ${width - strokeInset}`;
}

export function createVerticalLinePath(
	width: number,
	height: number,
	strokeInset: number,
): string {
	const right = width - strokeInset;

	return `M ${right} ${strokeInset} V ${height - strokeInset}`;
}

function readScopedCssValue(
	styles: CSSStyleDeclaration,
	scope: SketchScope,
	name: string,
): string {
	if (scope === "bg") {
		const scoped = styles.getPropertyValue(`--sketch-bg-${name}`).trim();

		if (scoped !== "") {
			return scoped;
		}
	}

	return styles.getPropertyValue(`--sketch-${name}`).trim();
}
