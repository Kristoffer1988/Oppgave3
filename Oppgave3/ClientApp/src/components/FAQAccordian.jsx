import React, { Component } from 'react';
import Accordion from 'react-bootstrap/Accordion';
import Card from 'react-bootstrap/Card';
import Like from './Like';

class FAQAccordian extends Component {
    constructor(props) {
        super(props);
        this.state = {
            faq: this.props.faq
        };
    }
  

    render() {
        return (
            <Accordion key={this.state.faq.id} className="accordianBakgrunn">
                <Card key={this.state.faq.id} >
                        <Accordion.Toggle eventKey={this.state.faq.id} as={Card.Header} className="toggleUtseende">
                            <div>{this.state.faq.spørsmål}</div>
                        </Accordion.Toggle>
                    <div className="likerKnapper">{<Like likes={this.state.faq.like} dislikes={this.state.faq.dislike} id={this.state.faq.id} oppdater={this.props.oppdater}/>}</div>
                        <Accordion.Collapse eventKey={this.state.faq.id}>
                            <Card.Body className="cardInnhold">
                                <div>
                                    <div className="svar">{this.state.faq.svar}</div>
                                </div>
                            </Card.Body>
                        </Accordion.Collapse>
                    </Card>
                </Accordion>
        );
    }
}

export default FAQAccordian;