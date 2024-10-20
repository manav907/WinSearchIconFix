# WinSearchIconFix

## Overview

**WinSearchIconFix** is a tool designed to fix missing or broken icons in the Windows Start menu caused by deleted/corrupted cache files for the Windows Search UWP app. 

This issue can occur for several reasons, but it should automatically resolve when the icon cache is properly rebuilt. I am not sure about this and this is my theroy I tried to recreate this issue in a VM and the icons do not fix themselves automatically.

Rebuilding the icon cache may fail due to the use of tools that debloat Windows or block telemetry. These tools prevent the recreation of cached icons for UWP apps, leading to missing or incorrect icons in the Start menu.

Another reason for this issue is software interference, with Google Drive being a common culprit. The exact point of failure is unknown to me, but this fix will work even if you have Google Drive installed. 

However, Google Drive continues to break UWP icons upon launch thus, you may need to re-run this solution if you plan to continue using Google Drive. Currently, I do not have a permanent fix for this, nor am I working on one. I hope someone finds my work useful and can build upon my findings to create a permanent solution.


**WinSearchIconFix** does not specifically check for missing or broken icons. Instead, it applies a fix to all UWP packages, restoring icons as needed.

## The Issue

In the folder located at:

`%localappdata%\Packages\Microsoft.Windows.Search_cw5n1h2txyewy\LocalState\AppIconCache\100`

Windows Search caches all the icons for UWP apps. Deleting the contents of this folder can temporarily break icons in Windows Search. Normally, Windows should recreate the icons after a restart or refresh. However, in certain situations, the icons for UWP apps are not recreated properly, leading to issues with missing or broken icons.

## How It Works

This program scans all listed UWP packages and attempts to fix their icons by copying the relevant icon files from the installed packages to the required system location. It automatically processes all UWP packages without checking for individual icon issues.

## How to Use

1. Download the `.zip` file containing the executable from the releases section.
2. Extract the `.zip` file.
3. Run `WinSearchIconFix.exe`.

### Program Options:

When the program starts, you will be prompted with the following options:

- **Create Comparison Files**: Type `Y` if you want to create comparison files (to store the before and after states of the icons). Type `N` to skip this step.
- **Close the Program**: Type `C` to close the program at any time during startup.

Once the program starts running, it will scan all UWP packages and attempt to fix their icons. The results will be logged to a file named `log.txt` for review.

### Post-Processing Options:

After the program has completed processing, you will have the following options:

- Press `O` to open the folder containing the comparison files (if created).
- Press `L` to open the log file.
- Press `C` to close the program.

## Disclaimer

Please note that this tool modifies system files related to UWP applications. While it has been tested in typical scenarios, it is provided "as is" and should be used at your own risk.
