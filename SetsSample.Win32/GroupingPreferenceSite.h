#pragma once

#include"stdafx.h"
#include"GroupingPreferenceProvider.h"

namespace SetsSample
{
	class GroupingPreferenceSite : public IServiceProvider
	{
	private:
		int Ref = 1;
		GroupingPreferenceProvider* provider;

		GroupingPreferenceSite(const GroupingPreferenceProvider&) {}

	public:

		GroupingPreferenceSite(int preference, HWND associatedWindow)
		{
			provider = new GroupingPreferenceProvider(preference, associatedWindow);
		}

		virtual ~GroupingPreferenceSite()
		{
			provider->Release();
		}

		virtual HRESULT STDMETHODCALLTYPE QueryInterface(REFIID riid, void **ppvObject)
		{
			if (riid == __uuidof(IUnknown))
			{
				*ppvObject = this;
				Ref++;
				return S_OK;
			}
			if (riid == __uuidof(IServiceProvider))
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

		virtual HRESULT STDMETHODCALLTYPE QueryService(REFGUID guidService, REFIID riid, void **ppvObject)
		{
			if (guidService == __uuidof(ILaunchUIContextProvider))
			{
				*ppvObject = provider;
				provider->AddRef();
				return S_OK;
			}
			*ppvObject = nullptr;
			return E_FAIL;
		}
	};

}