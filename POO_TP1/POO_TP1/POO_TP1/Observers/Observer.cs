﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using POO_TP1;

namespace POO_TP1
{
    public interface Observer
    {
        /// <summary>
        /// Notifies the specified subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        void Notify(ObservedSubject subject);
    }
}
