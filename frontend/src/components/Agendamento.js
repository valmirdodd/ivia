import React, { Component } from 'react';
import { Button, ButtonToolbar } from 'react-bootstrap';
import { InserirAgendamento } from './InserirAgendamento';

export class Agendamento extends Component {
    constructor(props) {
        super(props);
        this.state = { addModalShow: false };
    }

    render() {
        let addModalClose = () => this.setState({ addModalShow: false });
        return (
            <div>
                <ButtonToolbar>
                    <Button className="my-4" variant='primary' onClick={() => this.setState({ addModalShow: true })}>
                        Inserir Agendamento
                    </Button>
                    <InserirAgendamento
                        show={this.state.addModalShow}
                        onHide={addModalClose} />
                </ButtonToolbar>
                <InserirAgendamento />
            </div>
        )
    }
}