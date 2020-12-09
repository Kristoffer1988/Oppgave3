import React, { Component } from 'react';
import KategoriAccordian from './KategoriAccordian';

class HentKategorier extends Component {
    state = {
        søk: "",
        kategoriListe: [],
        kategoriFilter: [],
        innlasting: true
    };

    oppdater = () => {
        this.hentKategorier();
    }

    handleSøk = event => {
        const søk = event.target.value;

        this.setState(forrigeTilstand => {
            const kategoriFilter = forrigeTilstand.kategoriListe.filter(enKategori => {
                return enKategori.kategori.toLowerCase().includes(søk.toLowerCase());
            });

            return {
                søk,
                kategoriFilter
            };
        });
    };

    hentKategorier = () => {
        fetch('http://localhost:52711/FAQ/HentKategorier')
            .then(response => response.json())
            .then(kategoriListe => {
                const { søk } = this.state;
                const kategoriFilter = kategoriListe.filter(enKategori => {
                    return enKategori.kategori.toLowerCase().includes(søk.toLowerCase());
                });

                this.setState({
                    kategoriListe,
                    kategoriFilter,
                    innlasting: false
                });
            });
    };

    componentDidMount() {
        this.hentKategorier();
    }

    render() {
        let tabellInnhold = this.state.innlasting
            ? <p><em>Laster inn...</em></p>
            : this.renderKategorier(this.state.kategorier);
        return (
            <div>
                {tabellInnhold}
            </div >
        );
    }

    renderKategorier() {
        return (
            <div key="forOpdatering">
                <form>
                    <input className="filter" placeholder="Søk kategorier" value={this.state.søk} onChange={this.handleSøk} />
                </form>
                <div>{this.state.kategoriFilter.map(kategori => <KategoriAccordian kategori={kategori} key={kategori.id} oppdater={this.oppdater} />)}</div>
            </div>
        );
    }
}
export default HentKategorier;