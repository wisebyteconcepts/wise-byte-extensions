# WiseByteExtensions.WinUI - Converters Documentation

This document provides detailed documentation for each converter found in the `Converters` folder of the project. Converters are used in WinUI applications to transform data between the source and the UI, typically in data binding scenarios.

---

## `BooleanToVisibilityConverter`

### Purpose  
Converts a `bool` value to a `Visibility` enumeration value (`Visible` or `Collapsed`). Useful for showing or hiding UI elements based on a boolean property.

### Usage Scenario
Ideal for binding the `Visibility` property of UI elements to boolean properties in your view model.

### Behavior
**Default Behavior**
  - `true` → `Visibility.Visible`
  - `false` → `Visibility.Collapsed`

**Inverted Behavior** (with `ConverterParameter='true'`)  
`ConverterParameter`: If set to `'true'`, the conversion logic is inverted
  - `true` → `Visibility.Collapsed`
  - `false` → `Visibility.Visible`

### Example
```xaml
<Page.Resources>
	<converters:BooleanToVisibilityConverter x:Key="BoolToVisibility" />
</Page.Resources>

<!-- Normal conversion: true = Visible, false = Collapsed -->
<TextBlock Text="Active"
	Visibility="{Binding IsActive, Converter={StaticResource BoolToVisibility}}" />

<!-- Inverted conversion: true = Collapsed, false = Visible -->
<TextBlock Text="Inactive"
	Visibility="{Binding IsActive, Converter={StaticResource BoolToVisibility}, ConverterParameter='true'}" />
```

### Explanation
- The first `TextBlock` will be visible when `IsActive` is `true`.
- The second `TextBlock` will be visible when `IsActive` is `false`.

### Notes
- Must be used with boolean (`bool`) properties.
- Ensure that the `ConverterParameter` is explicitly set to `'true'` for inversion; any other value will maintain the default behavior.

---

## `BooleanToResourceBrushConverter`

### Purpose
Converts a `bool` value to a `Brush` retrieved from the application’s resource dictionary.

### Usage Scenario
Ideal for dynamically changing colors (such as `Foreground` or `Background`) in XAML elements based on a boolean property—using predefined brushes from your app resources.

### Behavior
- The converter expects the **`ConverterParameter`** to be a string containing **two resource keys**, separated by the `'|'` character:
  - The **first key** is used when the value is `true`.
  - The **second key** is used when the value is `false`.
- If the parameter is invalid or the specified resource cannot be found, the converter returns a **transparent brush**.

### Example
```xml
<Page.Resources>
	<converters:BooleanToResourceBrushConverter x:Key="BoolToResourceBrush" />
</Page.Resources>

<!-- Usage -->
<TextBlock Text="Status"
           Foreground="{Binding IsActive,
                                Converter={StaticResource BoolToResourceBrush},
                                ConverterParameter='ActiveBrush|InactiveBrush'}" />
```

### Explanation
- When `IsActive` is `true`, the brush named `ActiveBrush` from the app resources is applied.
- When `IsActive` is `false`, the brush named `InactiveBrush` is used instead.

### Notes
- Must be used with boolean (`bool`) properties.
- Must be provided with exactly two resource keys in the `ConverterParameter`.
- Ensure that the resource keys provided in the parameter exist in your resource dictionaries.
- Returns a `SolidColorBrush` with `Colors.Transparent` if the converter cannot resolve a resource.
- Use [`ThreeStateBooleanToResourceBrushConverter`](#threestatebooleantoresourcebrushconverter) if your property is nullable (`bool?`).

---

## `ThreeStateBooleanToResourceBrushConverter`

### Purpose  
Converts a three-state boolean value (`true`, `false`, or `null`) to a corresponding `Brush` resource retrieved from the application's resource dictionary.  
This converter is useful when you want to visually represent the state of a nullable boolean property using predefined brushes in your application resources.

### Usage Scenario  
Ideal for dynamically changing colors (such as `Background`, `Foreground`, or `BorderBrush`) based on a three-state boolean property, where each state (`true`, `false`, `null`) maps to a specific brush resource.

### Behavior
- The converter expects the **`ConverterParameter`** to be a string containing **three resource keys**, separated by the `'|'` character:  
  - The **first key** is used when the value is `true`.  
  - The **second key** is used when the value is `false`.  
  - The **third key** is used when the value is `null`.  
- If the parameter is invalid or a resource cannot be resolved, the converter returns a **transparent brush**.


## Example
```xml
<Page.Resources>
	<converters:ThreeStateBooleanToResourceBrushConverter x:Key="ThreeStateBoolToBrush" />
</Page.Resources>

<!-- Usage -->
<Border Background="{Binding IsEnabled,
                             Converter={StaticResource ThreeStateBoolToBrush},
                             ConverterParameter='EnabledBrush|DisabledBrush|UnknownBrush'}" />
```

### Explanation
- When `IsEnabled` is `true`, the `brush` named `EnabledBrush` from the app resources is applied.
- When `IsEnabled` is `false`, the `brush` named `DisabledBrush` is used.
- When `IsEnabled` is `null`, the `brush` named `UnknownBrush` is used.
- If the converter cannot find the specified resource key, it falls back to a **transparent brush**.

### Notes
- Must be used with nullable boolean (`bool?`) properties.
- Must be provided with exactly three resource keys in the `ConverterParameter`.
- Ensure that all three brush resource keys provided in the parameter exist in your resource dictionaries.
- The converter returns a `SolidColorBrush` with `Colors.Transparent` if the resource cannot be resolved.
- Suitable for nullable boolean (bool?) bindings where each state should have a distinct visual representation.
- Use [`BooleanToResourceBrushConverter`](#booleantoresourcebrushconverter) if your property only represents true or false states.


---


## `BooleanToSelectionConverter`

### Purpose  
Converts a `bool` value to a selectable state (`IsChecked`) and vice versa.  
This converter is ideal for scenarios where a boolean property is bound to UI selection controls such as `RadioButton` or `ToggleButton`.

### Usage Scenario  
Useful when you need two-way binding between a boolean property and a selection control, ensuring that user selections directly update the bound boolean value.

### Behavior
- The converter uses the **`ConverterParameter`** to determine which boolean value the control represents.  
  - If `ConverterParameter` is `"true"`, the control is checked when the bound value is `true`.  
  - If `ConverterParameter` is `"false"`, the control is checked when the bound value is `false`.
- The converter supports **two-way binding**, allowing changes in either the UI or the data model to stay synchronized.
- Unlike `ThreeStateBooleanToSelectionConverter`, this converter does **not** support `null` or undefined states.


## Example
```xml
<Page.Resources>
	<converters:BooleanToSelectionConverter x:Key="BoolToSelection" />
</Page.Resources>

<!-- Usage -->
<StackPanel>
	<RadioButton Content="True"
	             IsChecked="{Binding BooleanValue, 
	                         Mode=TwoWay,
	                         Converter={StaticResource BoolToSelection},
	                         ConverterParameter='true'}" />

	<RadioButton Content="False"
	             IsChecked="{Binding BooleanValue, 
	                         Mode=TwoWay,
	                         Converter={StaticResource BoolToSelection},
	                         ConverterParameter='false'}" />
</StackPanel>
```

### Explanation
- When `BooleanValue` is `true`, the `RadioButton` with `ConverterParameter='true'` will be checked.
- When `BooleanValue` is `false`, the `RadioButton` with `ConverterParameter='false'` will be checked.

Selecting a different `RadioButton` updates the bound boolean property accordingly.

### Notes
- Ensure that the `ConverterParameter` is explicitly set to either `'true'` or `'false'`.
- This converter is intended for non-nullable boolean properties.
- Use [`ThreeStateBooleanToSelectionConverter`](#threestatebooleantoselectionconverter) if you need to handle null or indeterminate states.


---


## `ThreeStateBooleanToSelectionConverter`

### Purpose  
Converts a three-state boolean (`true`, `false`, or `null`) to a selection state and vice versa.  
This converter is particularly useful for controls such as `RadioButton` or any scenario where a **nullable boolean** property is represented as a selectable option.

### Usage Scenario  
Ideal for cases where a `bool?` property (nullable boolean) must be bound to multiple selectable options in the UI, allowing users to choose between `True`, `False`, or `Undefined` states.

### Behavior
- The converter uses the **`ConverterParameter`** to determine which selection option corresponds to which boolean state.  
  - If `ConverterParameter` is `"true"`, the control is checked when the value is `true`.  
  - If `ConverterParameter` is `"false"`, the control is checked when the value is `false`.  
  - If `ConverterParameter` is `"null"`, the control is checked when the value is `null`.  
- The converter supports **two-way binding**, ensuring changes in the UI reflect in the data model and vice versa.  
- When a different option is selected, the underlying `bool?` property updates accordingly.


### Example
```xml
<Page.Resources>
	<converters:ThreeStateBooleanToSelectionConverter x:Key="ThreeStateBoolToSelection" />
</Page.Resources>

<!-- Usage -->
<StackPanel>
	<RadioButton Content="True"
	             IsChecked="{Binding ThreeStateBoolean,
	                         Mode=TwoWay,
	                         Converter={StaticResource ThreeStateBoolToSelection},
	                         ConverterParameter='true'}" />

	<RadioButton Content="False"
	             IsChecked="{Binding ThreeStateBoolean,
	                         Mode=TwoWay,
	                         Converter={StaticResource ThreeStateBoolToSelection},
	                         ConverterParameter='false'}" />

	<RadioButton Content="Undefined"
	             IsChecked="{Binding ThreeStateBoolean,
	                         Mode=TwoWay,
	                         Converter={StaticResource ThreeStateBoolToSelection},
	                         ConverterParameter='null'}" />
</StackPanel>
```

### Explanation
- When `ThreeStateBoolean` is `true`, the `RadioButton` with `ConverterParameter='true'` will be checked.
- When `ThreeStateBoolean` is `false`, the `RadioButton` with `ConverterParameter='false'` will be checked.
- When `ThreeStateBoolean` is `null`, the `RadioButton` with `ConverterParameter='null'` will be checked.

### Notes
- Ensure that the `ConverterParameter` matches one of the supported values: `'true'`, `'false'`, or `'null'`.
- This converter supports nullable boolean (`bool?`) properties.
- Use [`BooleanToSelectionConverter`](#booleantoselectionconverter) if you only need to handle true and false values without a null state.

---

## `BooleanToTextConverter`

### Purpose  
Converts a `bool` value (`true` or `false`) to one of two strings specified in the converter parameter.  
This converter is useful for displaying different text values in controls like `TextBlock` or any content control based on a `boolean` property.

### Usage Scenario  
Ideal for toggling text display in the UI based on a boolean property—for example, showing `"Active"` when `true` and `"Inactive"` when `false`.

### Behavior
- The converter expects the **`ConverterParameter`** to be a string containing **two or more values**, separated by the `'|'` character.  
  - The **first value** is returned when the bound value is `true`.  
  - The **second value** is returned when the bound value is `false`.  
  - If more than two values are specified, only the first two are used.  
- If the parameter is `null`, does not contain at least two parts, or the input value is not a `bool`, the converter returns the string `"null"`.


### Example
```xml
<Page.Resources>
	<converters:BooleanToTextConverter x:Key="BoolToText" />
</Page.Resources>

<!-- Usage -->
<TextBlock Text="{Binding IsActive,
                         Mode=OneWay,
                         Converter={StaticResource BoolToText},
                         ConverterParameter='Active|Inactive'}" />
```
### Explanation
- When `IsActive` is `true`, the `TextBlock` displays "Active".
- When `IsActive` is `false`, the `TextBlock` displays "Inactive".
- If the parameter is invalid or the value is not a `boolean`, displays "null".

### Notes
- Ensure that the `ConverterParameter` contains at least two values separated by `|`.
- Only the first two values are used by the converter.
- The converter returns "null" for invalid input or missing parameters.
- Suitable for any control that binds to textual content based on a boolean property.
- Use [`ThreeStateBooleanToTextConverter`](#threestatebooleantotextconverter) for handling nullable boolean (`bool?`) values.

---

## `ThreeStateBooleanToTextConverter`

### Purpose
Converts a three-state boolean (`true`, `false`, or `null`) to one of three strings specified in the converter parameter.
This converter is useful for displaying different text values in controls like `TextBlock` or any content control based on a nullable boolean property.

### Usage Scenario
Ideal for toggling text display in the UI based on a nullable boolean property—for example, showing `"Yes"` when `true`, `"No"` when `false`, and `"Unknown"` when `null`.

### Behavior
- The converter expects the **`ConverterParameter`** to be a string containing **three or more values**, separated by the `'|'` character.  
  - The **first value** is returned when the bound value is `true`.  
  - The **second value** is returned when the bound value is `false`.  
  - The **third value** is returned when the bound value is `null`.  
  - If more than three values are specified, only the first three are used.
	- If the parameter is `null`, does not contain at least three parts, or the input value is not a `bool?`, the converter returns the string `"null"`.
	- If the input value is `null`, the converter returns the third value from the parameter.

### Example
```xml
<Page.Resources>
	<converters:ThreeStateBooleanToTextConverter x:Key="ThreeStateBoolToText" />
</Page.Resources>

<!-- Usage -->
<TextBlock Text="{Binding IsActive,
                         Mode=OneWay,
                         Converter={StaticResource ThreeStateBoolToText},
                         ConverterParameter='Yes|No|Unknown'}" />
```
### Explanation
- When `IsActive` is `true`, the `TextBlock` displays "Yes".
- When `IsActive` is `false`, the `TextBlock` displays "No".
- When `IsActive` is `null`, the `TextBlock` displays "Unknown".

### Notes
- Ensure that the `ConverterParameter` contains at least three values separated by `|`.
- Only the first three values are used by the converter.
- The converter returns "null" for invalid input or missing parameters.
- Suitable for any control that binds to textual content based on a nullable boolean (`bool?`) property.

---
