using System;
using FubuCore.Util;

namespace FubuMVC.Diagnostics.Visualization
{
    public static class GlyphRegistry
    {
        private readonly static Cache<Type, string> _glyphs = new Cache<Type, string>(type => "icon-cog");

        public static void Use<T>(string name)
        {
            _glyphs[typeof (T)] = name;
        }

        public static string GlyphFor(Type type)
        {
            return _glyphs[type];
        }
    }
}