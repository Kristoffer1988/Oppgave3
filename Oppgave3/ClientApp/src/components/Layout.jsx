import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { Toolbar } from './Toolbar';

export class Layout extends Component {
    render() {
        return (
            <div>
                <Toolbar />
                <Container>
                    {this.props.children}
                </Container>
            </div>
        );
    }
}