# Pangeas Helping Hand

Pangeas Helping Hand is a VR game framework with several mini-games and interaction prototypes. This repository contains the C# source and Unity project files used while building the project for a client. The project is structured as a Unity VR experience and targets common VR runtimes (OpenXR, SteamVR, Oculus).  

Important: this repository is under a custom view-only license (see LICENSE). If you are not listed as a project collaborator you are only permitted to read and inspect the source code on GitHub. Cloning, copying, redistributing, modifying, or using the code in other projects is prohibited unless you have explicit collaborator permission.

Repository snapshot
- Owner: Crimsofall-Technologies
- Name: Pangeas-Helping-Hand
- Primary language: C#
- Default branch: main
- URL: https://github.com/Crimsofall-Technologies/Pangeas-Helping-Hand

Contents
- Assets/                - Unity Assets (scenes, prefabs, scripts)
- Packages/              - Unity package manifest and dependencies
- ProjectSettings/       - Unity project settings
- README.md              - This file
- LICENSE                - Repository license (view-only rules + collaborator exceptions)

Quick start (for collaborators)
1. Ensure you have a compatible Unity Editor version. Recommended: Unity 2020.3 LTS or later (confirm exact version with project maintainers).
2. Install a VR runtime/provider (OpenXR, SteamVR, or Oculus Integration) according to the target headset.
3. Clone the repository (collaborators only):
   git clone https://github.com/Crimsofall-Technologies/Pangeas-Helping-Hand.git
4. Open the project in the Unity Editor and allow the editor to resolve packages.
5. Open the main scene in Assets/Scenes/ (ask a maintainer for the exact scene path if it’s not obvious).
6. Configure Player Settings → XR Plug-in Management to match the target runtime.
7. Enter Play mode in the Editor or create a build (File → Build Settings).

Non-collaborator access
- You may view the code on GitHub for review, learning, or evaluation purposes.
- You may not clone, copy, fork, or use the code outside of the GitHub web interface without written permission from the repository owner.
- For access beyond viewing (cloning, contributing, running locally, or redistribution), request collaborator access as described in “Requesting access”.

Project structure (typical Unity layout)
- Assets/ — Scenes, prefabs, scripts (.cs), art assets
- Packages/ — manifest.json and package configurations
- ProjectSettings/ — project-wide Unity settings
- .gitignore — recommended Unity-specific ignores (if present)

Contributing
This is a client project. Contributions are restricted to approved collaborators only.
- If you are a maintained contributor or a client-assigned developer, request collaborator access from the repository owner or open an issue to request access (if issues are enabled).
- Once you are granted collaborator status, follow the repository’s branch and PR workflow (create feature branches off main, open pull requests, request reviews).

Requesting collaborator access
- Open an issue in this repository requesting collaborator access and include:
  - Your GitHub username
  - Role (developer, tester, artist)
  - Reason for access and expected changes
- Or contact the owner/organization directly: https://github.com/Crimsofall-Technologies

License summary
This repository is distributed under a custom "View-Only License (Collaborator-Restricted)". The LICENSE file in this repository contains the full terms. In short:
- Non-collaborators: allowed to view source on GitHub only. Not allowed to copy, clone, fork, redistribute, or create derivative works.
- Collaborators: will be granted explicit rights (read/write/modify/distribute) as part of their collaborator agreement.

Security & sensitive data
- Do NOT hardcode secrets (API keys, passwords, tokens) in the repository. If you find secrets, contact a maintainer immediately.
- If you discover a security issue, report it privately to the repository owner.

Acknowledgements
- Project created for a client of Crimsofall-Technologies.
- VR runtimes: OpenXR / SteamVR / Oculus (as applicable)
- Unity engine and community packages

Contact
- Repository owner: Crimsofall-Technologies — https://github.com/Crimsofall-Technologies
