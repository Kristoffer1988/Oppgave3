import React, { Component } from 'react';
import Accordion from 'react-bootstrap/Accordion'
import Card from 'react-bootstrap/Card';
import CardMainHeader from './CardMainHeader';
import FAQAccordian from './FAQAccordian';
import 'bootstrap/dist/css/bootstrap.css';

class KategoriAccordian extends Component {
    constructor(props) {
        super(props);
        this.state = {
            kategori: this.props.kategori
        };
    }

  

    oppdaterBilde = () => {
        this.refs.oppdater.roter();
    }

    render() {
        return (
            <Accordion className="accordianBakgrunn">
                <Card >
                    <Accordion.Toggle eventKey={this.state.kategori.id} as={Card.Header} onClick={this.oppdaterBilde} className="toggleUtseende">
                        <div>
                            {this.state.kategori.kategori}
                        </div>
                    </Accordion.Toggle>
                    <div className="pil"><CardMainHeader ref="oppdater" /></div>
                    <Accordion.Collapse eventKey={this.state.kategori.id}>
                        <Card.Body className="cardInnhold">
                            <div>
                                {
                                    this.state.kategori.faqs.map(faq =>
                                        <FAQAccordian key={faq.id} faq={faq}  oppdater={this.props.oppdater} />
                                    )}
                            </div>
                        </Card.Body>
                    </Accordion.Collapse>
                </Card>
            </Accordion>
        );
    }
}

export default KategoriAccordian;