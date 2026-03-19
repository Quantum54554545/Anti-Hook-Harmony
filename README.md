# 🛡️ Anti-Hook-Harmony

![C#](https://img.shields.io/badge/Language-C%23-blue.svg)
![Library](https://img.shields.io/badge/Dependency-Harmony-orange.svg)
![Protection](https://img.shields.io/badge/Protection-AntiPatching-red.svg)

**HookShield** is a lightweight, background-running security component designed to protect your .NET application from runtime tampering. It specifically targets **Harmony patches**, ensuring that no unauthorized modifications are injected into your methods while the application is running.

## ✨ Why it's useful
* **Real-time Monitoring:** Constantly scans your codebase for any hooks (Prefixes, Postfixes, Transpilers).
* **Auto-Recovery:** Detects unauthorized patches and instantly rolls them back, restoring your methods to their original state.
* **Invisible Protector:** Runs in a separate background thread, making it harder for simple injectors to spot the detector.
* **Plug & Play:** Easily integrate it into any project using Harmony.

## ⚙️ How it works (the short version)
Most modern game/app cheats use **Harmony** to hook methods, redirect logic, or bypass checks. HookShield fights back:
1. **Cache Building:** Upon initialization, it maps all critical methods and constructors within your assembly.
2. **Background Scanner:** A dedicated background thread iterates through the cached method list every second.
3. **Integrity Check:** Uses `Harmony.GetPatchInfo` to peek at the method's metadata.
4. **Instant Neutralization:** If any external patch is found, the system forcefully unpatches the method, stripping away the intruder's code immediately.

## 🔑 Use

```csharp
unhook.build_method_cache();
unhook.run_scanner();
