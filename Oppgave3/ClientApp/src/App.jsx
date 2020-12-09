import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { HovedSide } from './components/HovedSide';
import { SkjemaSide } from './components/SkjemaSide';

export default class App extends Component {
    render() {
        return (
            <Layout>
                <Route exact path='/' component={HovedSide} />
                <Route path='/SkjemaSide' component={SkjemaSide} />
            </Layout>
        );
    }
}