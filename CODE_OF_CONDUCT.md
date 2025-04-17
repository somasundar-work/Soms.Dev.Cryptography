# Code of Conduct

## Our Pledge

We, as contributors and maintainers of the URL Shortener project, pledge to create a welcoming, inclusive, and harassment-free environment for all participants. We commit to treating everyone with respect and professionalism, regardless of their background, experience level, or personal characteristics.

## Our Standards

Examples of acceptable behavior include:

- Being respectful and inclusive to all community members.
- Providing constructive feedback and helping others when possible.
- Using welcoming and inclusive language.
- Being open to different perspectives and ideas.

Examples of unacceptable behavior include:

- Harassment, discrimination, or offensive language.
- Personal attacks, trolling, or inflammatory comments.
- Publishing others’ private information without consent.
- Any other conduct that could be considered inappropriate in a professional setting.
  s

## Naming Conventions Best Practices

### 1. General Naming Guidelines

- Use meaningful and descriptive names.
- Follow a consistent casing style (PascalCase, camelCase, snake_case).
- Avoid magic numbers and single-letter variable names.
- Be concise but clear.
- Avoid generic names like `ProcessData()`.
- Use standard prefixes and suffixes (e.g., `Async` for async methods).

### 2. Casing Conventions

Common casing styles:

- **PascalCase**: Classes, Methods, Properties (e.g., `CustomerService`, `GetCustomerOrders()`)
- **camelCase**: Variables, Parameters, Private Fields (e.g., `customerId`, `totalAmount`)
- **snake_case**: Constants, Environment Variables (e.g., `MAX_RETRIES`, `DB_CONNECTION_STRING`)
- **UPPER_SNAKE_CASE**: Constants (e.g., `DEFAULT_TIMEOUT`)
- **kebab-case**: URLs, File Names (e.g., `/user-profile`, `invoice-summary.pdf`)

### 3. Variables and Constants

- Use descriptive names (e.g., `userFullName`, `orderTotal`).
- Use `readonly` for values that don't change after initialization.
- Use constants for fixed values (e.g., `const int MaxRetries = 3;`).
- Avoid single-character names (`x`, `y`) unless in loops.

### 4. Classes, Interfaces, and Enums

- Use **PascalCase** for class names (e.g., `OrderService`).
- Prefix interfaces with `I` (e.g., `ILogger`, `IRepository`).
- Use **singular nouns** for class names (e.g., `Customer`, not `Customers`).
- Use meaningful enum names (e.g., `OrderStatus` with values like `Pending`, `Shipped`).

### 5. Methods and Functions

- Use **verbs or action phrases** (e.g., `GetCustomerById()`, `CalculateTotal()`).
- Indicate return type in the name if necessary (e.g., `IsUserActive()`).
- Append `Async` for asynchronous methods (e.g., `FetchDataAsync()`).
- Avoid generic names like `DoWork()` or `Process()`.

### 6. Properties

- Use **PascalCase** (e.g., `OrderDate`, `CustomerName`).
- Use `Is` or `Has` for booleans (e.g., `IsActive`, `HasDiscount`).
- Avoid prefixing with `_` unless it's a private field.

### 7. Events and Delegates

- Use `On` prefix for event handlers (e.g., `OnOrderCompleted`).
- Use **past tense or passive voice** for event names (e.g., `OrderCompleted`).
- Use `EventHandler<T>` for standard event patterns.

### 8. Database Naming

- Use **snake_case** or **PascalCase** (e.g., `order_items`, `CustomerOrders`).
- Use **singular table names** (e.g., `Customer`, `Invoice`).
- Prefix foreign keys with the related table (e.g., `customer_id`).
- Avoid using reserved SQL keywords like `Order` or `User`.

### 9. API and REST Naming

- Use **nouns** for resource names (e.g., `/users`, `/orders`).
- Use **plural** for collections (e.g., `/products`).
- Use **verbs for actions** (e.g., `/users/{id}/activate`).
- Use **kebab-case** for URLs (e.g., `/user-profile`).
- Avoid using verbs in resource names (`/getUsers`).

### 10. Configuration and Environment Variables

- Use **UPPER_SNAKE_CASE** (e.g., `DATABASE_URL`, `API_KEY`).
- Use clear, specific names (e.g., `JWT_SECRET`, `REDIS_HOST`).
- Avoid overly generic names like `CONFIG_1`.
- Avoid hardcoding sensitive data; use environment variables or secret managers.

## 🚀 Branching Rules

To ensure a well-structured and maintainable codebase, follow these branching guidelines:

### 🔹 Main Branches

- **`main`** → Stable production-ready branch. All releases are merged here.
- **`develop`** → Active development branch. All feature branches should be merged into this before going to `main`.

### 🔹 Feature Development

- Use `feature/{feature-name}` for new features.
- Example: `feature/add-url-tracking`, `feature/custom-alias-support`
- Merge into `develop` after review and approval.

### 🔹 Bug Fixes

- Use `fix/{bug-description}` for bug fixes.
- Example: `fix/redirect-bug`, `fix/database-connection`
- Merge into `develop` (or `main` for critical hotfix).

### 🔹 Hotfix

- Use `hotfix/{urgent-fix}` for critical fixes in production.
- Example: `hotfix/expired-links-issue`
- Merge into both `main` and `develop`.

### 🔹 Release Branch

- Use `release/{version}` to prepare a stable release.
- Example: `release/v1.0.0`
- Once tested, merge into `main` and tag the version.

### 🔹 Naming Conventions

- Use **lowercase**, **hyphens (-) instead of spaces**, and keep branch names **descriptive**.
- ✅ `feature/add-url-analytics`
- ❌ `feature_addurl`
- Keep branch names concise but clear.

### 🔹 Merge Strategy

- Always use **Pull Requests (PRs)** to merge branches.
- Ensure **at least one approval** before merging.
- **No direct commits** to `main` or `develop`.

### 🔹 Additional Rules

- Delete branches after merging to keep the repository clean.
- Keep commits atomic (small, self-contained changes).
- Use meaningful commit messages.

## Enforcement

Project maintainers are responsible for upholding this Code of Conduct. Violations will be reviewed and addressed appropriately, including warnings or removal from the project.

## Reporting Issues

If you experience or witness a violation of this Code of Conduct, please report it by contacting the maintainers via email or an issue in the repository. All reports will be handled confidentially.

## Acknowledgment

This Code of Conduct is inspired by the Contributor Covenant and other open-source community guidelines. By participating in this project, you agree to abide by these standards.
