# Contributing to Easy Asset Package Tool

Thank you for your interest in contributing! This document provides guidelines for contributing to this project.

## How to Contribute

### Reporting Issues

- Use [GitHub Issues](https://github.com/Yu-Rin-Chi2/EawsyAssetPackageTool/issues) to report bugs or request features.
- Before creating a new issue, check if a similar issue already exists.
- Include the following in bug reports:
  - Unity version
  - OS (Windows / macOS)
  - Steps to reproduce
  - Expected vs actual behavior

### Pull Requests

1. Fork the repository and create a feature branch from `main`.
2. Follow the [coding conventions](docs/design/conventions.md).
3. Test your changes in Unity Editor.
4. Submit a pull request with a clear description of the changes.

### Coding Conventions

- 4-space indentation
- `PascalCase` for classes, methods, properties, and public fields
- `camelCase` for private fields and local variables
- Normalize paths with `Replace("\\", "/")` for Unity compatibility
- All code must be Editor-only (no Runtime dependencies)
- Use `[PackageBuilder]` prefix for `Debug.Log` messages

For full details, see [docs/design/conventions.md](docs/design/conventions.md).

## Development Setup

1. Clone the repository:
   ```
   git clone https://github.com/Yu-Rin-Chi2/EawsyAssetPackageTool.git
   ```
2. Open the project in Unity 2022.3 LTS or later.
3. Source files are located in `Assets/EasyAssetPackageTool/Editor/`.

## License

By contributing, you agree that your contributions will be licensed under the [MIT License](LICENSE).
