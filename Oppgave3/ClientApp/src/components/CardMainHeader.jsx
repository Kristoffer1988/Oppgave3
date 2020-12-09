import React, { Component } from 'react';
import pil from "./bilder/pil.png";

class CardMainHeader extends Component {
    constructor(props) {
        super(props);
        this.state = {
            rotasjon: 0
        }
        this.roter = this.roter.bind(this);
    }

    roter() {
        var nyRotasjon = this.state.rotasjon;
        if (nyRotasjon == 180) {
            nyRotasjon = nyRotasjon - 180;
        }
        else {
            nyRotasjon = nyRotasjon + 180;
        }
        this.setState({
            rotasjon: nyRotasjon,
        });
    }

    render() {
        const { rotasjon } = this.state;
        return (

            <img alt="Ned/opp" style={{ transform: `rotate(${rotasjon}deg)` }} src={pil} width="35" className="pilBilde" />

        );
    }
}
export default CardMainHeader;