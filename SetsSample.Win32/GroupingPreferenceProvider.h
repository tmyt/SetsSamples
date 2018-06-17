#pragma once

#include"stdafx.h"

namespace SetsSample
{
	class GroupingPreferenceProvider : public ILaunchUIContextProvider
	{
	private:
		int Ref = 1;
		int GroupingPreference;
		HWND AssociatedWindow;

		GroupingPreferenceProvider(const GroupingPreferenceProvider&){}

	public:
		GroupingPreferenceProvider(int preference, HWND associatedWindow)
		{
			GroupingPreference = preference;
			AssociatedWindow = associatedWindow;
		}

		virtual ~GroupingPreferenceProvider() {}

		virtual HRESULT STDMETHODCALLTYPE QueryInterface(REFIID riid, void **ppvObject)
		{
			if (riid == __uuidof(IUnknown))
			{
				*ppvObject = this;
				Ref++;
				return S_OK;
			}
			if (riid == __uuidof(ILaunchUIContextProvider))
			{
				*ppvObject = this;
				Ref++;
				return S_OK;
			}
			return E_NOINTERFACE;
		}

		virtual ULONG STDMETHODCALLTYPE AddRef()
		{
			return ++Ref;
		}

		virtual ULONG STDMETHODCALLTYPE Release()
		{
			auto newRef = --Ref;
			if (Ref == 0) delete this;
			return newRef;
		}

		virtual HRESULT STDMETHODCALLTYPE UpdateContext(ILaunchUIContext *context)
		{
			context->SetTabGroupingPreference(GroupingPreference);
			context->SetAssociatedWindow(AssociatedWindow);
			return S_OK;
		}
	};
}