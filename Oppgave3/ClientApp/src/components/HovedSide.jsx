import React, { Component } from 'react';
import HentKategorier from './HentKategorier';
import './css/index.css';

export class HovedSide extends Component {
    render() {
        return (
            <div>
                <HentKategorier />
            </div>
        );
    }
}