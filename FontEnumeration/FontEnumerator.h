#pragma once

namespace FontEnumeration
{
    public ref class FontEnumerator sealed
    {
    public:
        Platform::Array<Platform::String^>^ ListSystemFonts();
		Platform::Array<UINT32>^ ListSupportedChars(Platform::String^ fontName);
    };
}