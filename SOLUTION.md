# Solution Documentation

## Commits and Branching
- **Approach:** Used a feature-branching strategy (`Add Unit Tests, Add backup api`).
- **Commit Style:** Atomic commits following Conventional Commits ().


## Task List not loading on start up
- **Issue:** When starting the app, the default tasks were not showing up on the dashboard.
- **Diagnosis:** Discovered that the incorrect route was being used and the network tab was not showing any connection to the API.
- **Resolution:** Added logging and a checker to see the health of the API before making the calls.

## Debugging: The "Invisible Click" Layer
- **Issue:** Buttons near the header required clicking "outside" to work.
- **Diagnosis:** Discovered that the `modal-backdrop` and `modal` divs were being generated inside the `*ngFor` table rows.
- **Resolution:** Refactored the template to place the Modal at the root level of the component, using a single shared instance for all tasks.

## Error Handling
- Implemented **RFC 7807 Problem Details** globally.
- Standardized the mapping of backend `Enum` values to frontend `Bootstrap` badges for visual consistency.
