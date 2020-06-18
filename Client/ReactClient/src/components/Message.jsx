import React from 'react';

function Message (props) {
    const {prodNumber, qty, state, prevQty, prevState} = props;
    const states = ["в достаточном количестве", "Заканчивается", "достигли минимального количества"]

    return (
        <div className=''>
            <span className='product-ch'>Product number: {prodNumber} </span>
            <span className='product-ch'>Current quantity: {qty} </span>
            <span className='product-ch'>Current state: {states[state]} </span>
            <span className='product-ch'>Previous quantity: {prevQty} </span>
            <span className='product-ch'>Previous state: {states[prevState]} </span>
        </div>
    );
}

export default Message;