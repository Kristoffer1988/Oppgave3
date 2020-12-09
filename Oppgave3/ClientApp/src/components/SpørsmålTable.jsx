import React, { Component } from 'react';

class SpørsmålTable extends Component {
    constructor() {
        super();
        this.state = { skjemaer: [], loading: true };
    }

    componentDidMount() {
        this.populateSkjemaer();
    }

    renderSkjemaer(skjemaer) {
        return (
            <div>
                {
                    skjemaer.map(skjema =>
                        <div className="inputMargin stiltSpørsmål" key={skjema.id}>
                            <div className="stiltSpørsmålNavn">{skjema.navn}</div>
                            <div>{skjema.spørsmål}</div>
                        </div>
                    )}
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Laster inn...</em></p>
            : this.renderSkjemaer(this.state.skjemaer);

        return (
            <div>
                {contents}
            </div>
        );
    }

    async populateSkjemaer() {
        const response = await fetch('http://localhost:52711/FAQ/HentStilteSpørsmål');
        const data = await response.json();
        this.setState({ skjemaer: data, loading: false });
    }

    oppdaterTabell() {
        this.populateSkjemaer();
    }
}

export default SpørsmålTable;