import React, { Component } from 'react';
import { Modal, Button, Row, Col, Form } from 'react-bootstrap';
import api from './../services/api'

export class InserirAgendamento extends Component {
    constructor(props) {
        super(props);
    }

    async handleSubmit(event) {
        event.preventDefault();

        const response = await api.post("/Agendamento",
        {
            dhInicial: event.target.dtAgendamento.value + "T" + event.target.hrInicial.value,
            dhFinal: event.target.dtAgendamento.value + "T" + event.target.hrFinal.value,
            obs: event.target.obs.value,
            idVeiculo: event.target.idVeiculo.value
        })
        .then(response => alert(response.data))
        .catch(erro => alert("ops! ocorreu o seguinte erro: " + erro.message));
    }

    render() {
        return (
            <Modal
                {...this.props}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered
            >
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        Inserir Agendamento
            </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="container">
                        <Row>
                            <Col sm={3}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="dtAgendamento">
                                        <Form.Label>Data do Agendamento</Form.Label>
                                        <Form.Control
                                            type="date"
                                            name="dtAgendamento"
                                            required
                                            placeholder="Data do Agendamento"
                                        />
                                    </Form.Group>
                                    <Form.Group controlId="hrInicial">
                                        <Form.Label>Hora Inicial</Form.Label>
                                        <Form.Control
                                            type="time"
                                            name="hrInicial"
                                            required
                                            placeholder="Hora Inicial"
                                        />
                                    </Form.Group>
                                    <Form.Group controlId="hrFinal">
                                        <Form.Label>Hora Final</Form.Label>
                                        <Form.Control
                                            type="time"
                                            name="hrFinal"
                                            required
                                            placeholder="Hora Final"
                                        />
                                    </Form.Group>
                                    <Form.Group controlId="idVeiculo">
                                        <Form.Label>Veículo</Form.Label>
                                        <Form.Control
                                            type="number"
                                            name="IdVeiculo"
                                            required
                                            placeholder="Veículo"
                                        />
                                    </Form.Group>
                                    <Form.Group controlId="obs">
                                        <Form.Label>Observações</Form.Label>
                                        <Form.Control
                                            type="text"
                                            name="obs"
                                            placeholder="Observações"
                                        />
                                    </Form.Group>
                                    <Form.Group>
                                        <Button variant="primary" type="submit">Confirmar
                                    </Button>
                                    </Form.Group>
                                </Form>
                            </Col>
                        </Row>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="danger" onClick={this.props.onHide}>Cancelar</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}