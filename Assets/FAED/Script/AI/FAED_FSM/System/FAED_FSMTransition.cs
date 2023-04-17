using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.AI.FSM
{

    public abstract class FAED_FSMTransition : MonoBehaviour
    {

        [HideInInspector] public string nextState;

        protected virtual void Awake()
        {
            
            nextState = gameObject.name.Replace("(GoTo)", "");

        }

        public abstract bool ChackTransition();

    }

}