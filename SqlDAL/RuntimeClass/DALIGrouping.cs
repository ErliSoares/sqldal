/*
Copyright 2012 Brian Adams

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.DBAccess.Generic.RuntimeClass
{
    internal class DALIGrouping<TKey, TElement> : IGrouping<TKey, TElement>, IEnumerable<TElement>
    {
        /// <summary>
        /// DO NOT USE.  ONLY FOR INTERNAL USE BUT THE FRAMEWORK REQUIRES THIS TO BE PUBLIC.
        /// 
        /// Constructor for a new Grouping object that is the result of copying a group containing anonymous types to nonanonymous types
        /// </summary>
        public DALIGrouping(TKey key, IEnumerable<TElement> objects)
        {
            this.m_Key = key;
            this.m_Values = objects;
        }

        private TKey m_Key;
        private IEnumerable<TElement> m_Values;

        public TKey Key
        {
            get { return this.m_Key; }
        }

        public IEnumerable<TElement> Values
        {
            get { return this.m_Values; }
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return this.m_Values.GetEnumerator();
        }

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return (this.m_Values as Collections.IEnumerable).GetEnumerator();
        }
    }
}