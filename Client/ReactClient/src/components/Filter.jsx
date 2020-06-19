import React, { Component } from 'react';

class Filter extends Component {
    constructor(props) {
        super(props);

        this.state = {
            qtyCh: '',
            stateCh: '',
            categories: '',
        };
    }

    postFilter = (e) => {
        const separator = ",";
        const QtyCh = this.state.qtyCh.split(separator);
        const StateCh = this.state.stateCh.split(separator);
        const Categories = this.state.categories.trim() ?
            this.state.categories.split(separator).map((i) => +i) :
            [];

        console.log(QtyCh);
        console.log(StateCh);
        console.log(Categories);

        this.props.hubConnection
            .invoke('set_filter', { QtyCh: QtyCh, StateCh: StateCh, Categories: Categories })
            .catch(err => console.error(err));
    };

    onChangeQtyCh = (e) => {
        this.setState({ qtyCh: e.target.value })
    }

    onChangeStateCh = (e) => {
        this.setState({ stateCh: e.target.value })
    }

    onChangeCategories = (e) => {
        this.setState({ categories: e.target.value })
    }

    render() {
        return (
            <div className=''>
                <div className='chat-group'>
                    <div>Filter</div>
                </div>
                <div className=''>
                    <div>Qty changes</div>
                    <input value={this.state.qtyCh} onChange={this.onChangeQtyCh} />
                </div>
                <div className=''>
                    <div>State changes</div>
                    <input value={this.state.stateCh} onChange={this.onChangeStateCh} />
                </div>
                <div className=''>
                    <div>Categories</div>
                    <input value={this.state.Categories} onChange={this.onChangeCategories} />
                </div>
                <button onClick={this.postFilter}>Post filter</button>
            </div>
        );
    }
}

export default Filter;