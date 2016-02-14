/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2015 Microsoft Corporation
 * 
 * -=- Robust Distributed System Nucleus (rDSN) -=- 
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

/*
 * Description:
 *     What is this file about?
 *
 * Revision history:
 *     Feb., 2016, @imzhenyu (Zhenyu Guo), done in Tron project and copied here
 *     xxxx-xx-xx, author, fix bug about xxx
 */
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace rDSN.Tron.Utility
{
    public class IEnumerableConverter<TSource, TResult> : IEnumerable<TResult>, IEnumerable
    {
        public delegate void PostEnumerationCallback(object arg);
        public IEnumerableConverter(IEnumerable<TSource> enums, PostEnumerationCallback cb, object cbArg)
        {
            _enums = enums;
            _cb = cb;
            _cbArg = cbArg;
            _converter = new TypeConverter(typeof(TSource), typeof(TResult));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public IEnumerator<TResult> GetEnumerator()
        {
            if (_enums != null)
            {
                foreach (var o in _enums)
                {
                    yield return (TResult)_converter.Convert(o);
                }

                if (_cb != null)
                {
                    _cb(_cbArg);
                }
            }
        }

        private IEnumerable<TSource> _enums;
        private TypeConverter _converter;
        private PostEnumerationCallback _cb;
        private object _cbArg;
    }
}
