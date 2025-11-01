# WiseByteExtensions.WinUI - Converters Documentation

This document provides detailed documentation for each converter found in the `Converters` folder of the project. Converters are used in WinUI applications to transform data between the source and the UI, typically in data binding scenarios.

---

## 1. `BooleanToVisibilityConverter`

**Purpose:**  
Converts a `bool` value to a `Visibility` enumeration value (`Visible` or `Collapsed`). Useful for showing or hiding UI elements based on a boolean property.

**Usage Example:**
```xaml
<Page.Resources>
	<converters:BooleanToVisibilityConverter x:Key="BoolToVisibility" />
</Page.Resources>

<!-- Normal conversion: true = Visible, false = Collapsed -->
<TextBlock Text="Active"
	Visibility="{Binding IsActive, Converter={StaticResource BoolToVisibility}}" />

<!-- Inverted conversion: true = Collapsed, false = Visible -->
<TextBlock Text="Inactive"
	Visibility="{Binding IsActive, Converter={StaticResource BoolToVisibility}, ConverterParameter=true}" />
```
**Parameters:**
- `ConverterParameter`: If set to `"true"`, the conversion logic is inverted.
- Default Behavior:
  - `true` → `Visibility.Visible`
  - `false` → `Visibility.Collapsed`
- Inverted Behavior:
  - `true` → `Visibility.Collapsed`
  - `false` → `Visibility.Visible`
  ---


