using System.Collections.Generic;
using Books;

namespace DwmGetUnmetTabRequirements
{
    public static class DwmMessages
    {
        public static Dictionary<DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS, string> Messages =
            new Dictionary<DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS, string>
            {
                {
                    DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS.DWMTWR_NONE,
                    "The window meets all requirements requested."
                },
                {
                    DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS.DWMTWR_IMPLEMENTED_BY_SYSTEM,
                    "In some configurations, the admin/user setting or mode of the system means that windows won't be tabbed. This requirement indicates that the system mode must implement tabbing. If the system does not implement tabbing, nothing can be done to change this."
                },
                {
                    DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS.DWMTWR_WINDOW_RELATIONSHIP,
                    "The window has an owner or parent, and is therefore ineligible for tabbing."
                },
                {
                    DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS.DWMTWR_WINDOW_STYLES,
                    "The window has one or more styles that make it ineligible for tabbing.\n"
                    + "To be eligible for tabbing, a window must:\n"
                    + "Have the WS_OVERLAPPEDWINDOW (such as WS_CAPTION, WS_THICKFRAME, etc.) styles set.\n"
                    + "Not have WS_POPUP, WS_CHILD or WS_DLGFRAME set.\n"
                    + "Not have WS_EX_TOPMOST or WS_EX_TOOLWINDOW set."
                },
                {
                    DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS.DWMTWR_WINDOW_REGION,
                    "The window has a region (set using SetWindowRgn) making it ineligible."
                },
                {
                    DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS.DWMTWR_WINDOW_DWM_ATTRIBUTES,
                    "The window is ineligible due to its Dwm configuration.\n"
                    + "To resolve this issue, the window must not extended its client area into the title bar using DwmExtendFrameIntoClientArea. In addition, the window must not have DWMWA_NCRENDERING_POLICY set to DWMNCRP_ENABLED."
                },
                {
                    DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS.DWMTWR_WINDOW_MARGINS,
                    "The window is ineligible due to its margins, most likely due to custom handling in WM_NCCALCSIZE.\n"
                    + "To resolve this issue, the window must use the default window margins for the non-client area."
                },
                {
                    DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS.DWMTWR_TABBING_ENABLED,
                    "The window has been explicitly opted out by setting DWMWA_TABBING_ENABLED to false."
                },
                {
                    DwmInterop.DWM_TAB_WINDOW_REQUIREMENTS.DWMTWR_USER_POLICY,
                    "The user has configured this application to not participate in tabbing."
                }
            };
    }
}
