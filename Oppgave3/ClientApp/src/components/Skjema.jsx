import React, { Component } from 'react';
import axios from "axios";
import './css/index.css';

class Skjema extends Component {
    constructor(props) {
        super(props);

        this.state = {
            navn: '',
            mail: '',
            spørsmål: '',
            navnError: '',
            mailError: '',
            spørsmålError: '',
            submitError: ''
        };

        this.handleNavn = this.handleNavn.bind(this);
        this.handleMail = this.handleMail.bind(this);
        this.handleSpørsmål = this.handleSpørsmål.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

        this.validerNavn = this.validerNavn.bind(this);
        this.validerMail = this.validerMail.bind(this);
        this.validerSpørsmål = this.validerSpørsmål.bind(this);
    }

    handleNavn(event) {
        this.setState({ navn: event.target.value });
        this.validerNavn();
    }

    handleMail(event) {
        this.setState({ mail: event.target.value });
        this.validerMail();
    }

    handleSpørsmål(event) {
        this.setState({ spørsmål: event.target.value });
        this.validerSpørsmål();
    }

    validerNavn() {
        const navnTest = this.state.navn;
        if (navnTest.trim() === '') {
            this.setState({ navnError: 'Navn kan ikke være tom!' });
            return false;
        }
        else if (!this.state.navn.match(/^[a-zA-ZæøåÆØÅ\.\ \-]{2,25}$/)) {
            this.setState({ navnError: 'Du kan bare skrive inn bokstaver!(2-25 bokstaver)' });
            return false;
        }
        else {
            this.setState({ navnError: '' });
            return true;
        }
    }

    validerMail() {
        const mailTest = this.state.mail;
        if (mailTest.trim() === '') {
            this.setState({ mailError: 'Mail kan ikke være tom!' });
            return false;
        }
        else if (!(this.state.mail.match(/^([a-zæøåA-ZÆØÅ0-9_\-\.]+)@([a-zæøåA-ZÆØÅ0-9_\-\.]+)\.([a-zæøåA-ZÆØÅ]{2,5})$/))) {
            this.setState({ mailError: 'Du må skrive en mail!' });
            return false;
        }
        else {
            this.setState({ mailError: '' });
            return true;
        }
    }

    validerSpørsmål() {
        const spørsmålTest = this.state.spørsmål;
        if (spørsmålTest.trim().length < 2 || spørsmålTest.trim().length > 100) {
            this.setState({ spørsmålError: 'Husk å skrive et spørsmål!(2-100 tegn)' });
            return false;
        }
        else {
            this.setState({ spørsmålError: '' });
            return true;
        }
    }

    handleSubmit(event) {
        var navnOK = this.validerNavn();
        var mailOK = this.validerMail();
        var spørsmålOK = this.validerSpørsmål();

        if ((navnOK && mailOK && spørsmålOK)) {
            const data = {
                navn: this.state.navn,
                mail: this.state.mail,
                spørsmål: this.state.spørsmål
            };

            axios.post(`http://localhost:52711/FAQ/LagreSkjema`, data);

            this.setState({ navn: '' });
            this.setState({ mail: '' });
            this.setState({ spørsmål: '' });
            this.setState({ submitError: '' });

            setTimeout(() => this.props.oppdaterSpørsmålListe(), 1000);
        }

        else {
            this.setState({ submitError: 'Du må fylle ut alle feltene riktig!' });
        }

        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h2 className="inputMargin">Send in spørsmål</h2>

                <div>
                    <input id="navn" className="inputMargin" ref="navn" type="text" size="25" placeholder="Navn..." value={this.state.navn} onChange={this.handleNavn} onKeyUp={this.handleNavn} />
                </div>
                <div>
                    <span size="25" className="navnError inputMargin text-danger">{this.state.navnError}</span>
                </div>

                <div>
                    <input className="inputMargin" ref="mail" type="text" size="25" placeholder="Mail..." value={this.state.mail} onChange={this.handleMail} onKeyUp={this.handleMail} />
                </div>
                <div>
                    <span size="25" className="mailError inputMargin text-danger">{this.state.mailError}</span>
                </div>

                <div>
                    <textarea className="inputMargin" refs="Spørsmål" cols="40" rows="5" placeholder="Spørsmål..." value={this.state.spørsmål} onChange={this.handleSpørsmål} onKeyUp={this.handleSpørsmål} />
                </div>
                <div>
                    <span size="25" className="spørsmålError inputMargin text-danger">{this.state.spørsmålError}</span>
                </div>

                <div>
                    <input className="inputMargin btn btn-primary" type="submit" value="Send inn" />
                </div>
                <div>
                    <span size="25" className="submitError inputMargin text-danger">{this.state.submitError}</span>
                </div>
            </form>
        );
    }
}

export default Skjema;