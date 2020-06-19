import React, { Component } from 'react';
import {getAuthToken, connectToServer} from '../tools/connectionHelper';
import Message from './Message.jsx';
import Filter from './Filter.jsx';


class Chat extends Component {
  constructor(props) {
    super(props);

    this.state = {
      QtyCh: [],      
      StateCh: [],
      Categories: [],
      messages: [],
      hubConnection: null,
    };
  }

  componentDidMount = () => {
    var {server} = this.props;

    connectToServer(server)
      .then(connection => {        
        connection.on('product_changes', (productChanges) => {
          const protMes = this.state.messages.slice();
          protMes.push(productChanges)
          this.setState({messages: protMes})
          console.log(productChanges);
        });    
        
        this.setState({ hubConnection: connection}, () => {
          this.state.hubConnection
            .start()
            .then(() => {
              console.log('Connection started!');
            })
            .catch((err) => console.error('Error while establishing connection'));
        });
      });
  };

  componentWillUnmount = () => {
    this.state.hubConnection.stop();
  }



  renderMessages = (messages) => {
    console.log(messages);

    return messages.reverse().map((message, index) => (
      <Message 
        key={`chat-message-${index}`}
        prodNumber={message.Number}
        qty={message.Qty}
        state={message.State} 
        prevQty={message.PrevQty}
        prevState={message.PrevState}/>)
      );
  }

  render() {
    return (
      <div className='chat-container'>
        <div className='chat-buttons'>
          <Filter hubConnection={this.state.hubConnection}/>
        </div>
        <div className='chat-messages'>
          {this.renderMessages(this.state.messages)}
        </div>        
      </div>
    );
  }
}

export default Chat;
