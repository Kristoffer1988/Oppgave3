import React, { Component } from 'react';
import Skjema from './Skjema';
import SpørsmålTable from './SpørsmålTable';

export class SkjemaSide extends Component {
    oppdaterSpørsmålListe = () => {
        this.refs.oppdater.oppdaterTabell();
    }

    render() {
        return (
            <div className="row">
                <div className="skjema col-md-5">
                    <Skjema oppdaterSpørsmålListe={this.oppdaterSpørsmålListe} />
                </div>

                <div className="innsendteSpørsmål col-md-5 offset-1">

                    <SpørsmålTable ref="oppdater" />
                </div>
            </div>
        );
    }
}