import React, { Component } from 'react';
import likeBilde from "./bilder/like.png";
import dislikeBilde from "./bilder/dislike.png";
import axios from "axios";

class Like extends Component {
    state = {
        likes: this.props.likes,
        dislikes: this.props.dislikes,
        id: this.props.id,
    };

    handleLikes = () => {
        var harDisliket = window.localStorage.getItem("disliket" + this.state.id);
        var harLiket = window.localStorage.getItem("liket" + this.state.id);
        if (!harDisliket && !harLiket) {
            this.setState({ likes: this.state.likes + 1 });
            const data = {
                id: this.state.id,
                dislikes: this.state.dislikes,
                likes: this.state.likes + 1
            };

            window.localStorage.setItem("liket" + this.state.id, true);
            window.localStorage.removeItem("disliket" + this.state.id);

            axios.post(`http://localhost:52711/FAQ/EndreLikesFAQ`, data);

            setTimeout(() => this.props.oppdater(), 0);
        }
        else if (!harLiket && harDisliket) {
            this.setState({ dislikes: this.state.dislikes - 1 });
            this.setState({ likes: this.state.likes + 1 });
            const data = {
                id: this.state.id,
                dislikes: this.state.dislikes - 1,
                likes: this.state.likes + 1
            };

            window.localStorage.removeItem("disliket" + this.state.id);
            window.localStorage.setItem("liket" + this.state.id, true);

            axios.post(`http://localhost:52711/FAQ/EndreLikesFAQ`, data);

            setTimeout(() => this.props.oppdater(), 0);
        }
    }

    handleDislikes = () => {
        var harLiket = window.localStorage.getItem("liket" + this.state.id);
        var harDisliket = window.localStorage.getItem("disliket" + this.state.id);
        if (!harDisliket && !harLiket) {
            this.setState({ dislikes: this.state.dislikes + 1 });
            const data = {
                id: this.state.id,
                dislikes: this.state.dislikes + 1,
                likes: this.state.likes
            };

            window.localStorage.setItem("disliket" + this.state.id, true);
            window.localStorage.removeItem("liket" + this.state.id);

            axios.post(`http://localhost:52711/FAQ/EndreLikesFAQ`, data);

            setTimeout(() => this.props.oppdater(), 0);
        }
        else if (harLiket && !harDisliket) {
            this.setState({ dislikes: this.state.dislikes + 1 });
            this.setState({ likes: this.state.likes - 1 });
            const data = {
                id: this.state.id,
                dislikes: this.state.dislikes + 1,
                likes: this.state.likes - 1
            };

            window.localStorage.setItem("disliket" + this.state.id, true);
            window.localStorage.removeItem("liket" + this.state.id);

            axios.post(`http://localhost:52711/FAQ/EndreLikesFAQ`, data);

            setTimeout(() => this.props.oppdater(), 0);
        }
    }

    render() {
        return (
            <React.Fragment>

                <span className={this.getLikeBadgeClasses()}>{this.formatLikeCount()}</span>

                <img src={likeBilde} alt="like" width="40" height="40" onClick={this.handleLikes} className="btb btn-secondary likeBilde" />

                <span className={this.getDislikeBadgeClasses()}>{this.formatDislikeCount()}</span>

                <img src={dislikeBilde} alt="dislike" width="40" height="40" onClick={this.handleDislikes} className="btb btn-secondary likeBilde" />

            </React.Fragment>
        );
    }

    getLikeBadgeClasses() {
        let classes = "badge m-2 badge-";
        classes += (this.state.likes === 0) ? "warning" : "primary";
        return classes;
    }

    formatLikeCount() {
        const { likes } = this.state;
        return likes === 0 ? 'null' : likes;
    }

    getDislikeBadgeClasses() {
        let classes = "badge m-2 badge-";
        classes += (this.state.dislikes === 0) ? "warning" : "primary";
        return classes;
    }

    formatDislikeCount() {
        const { dislikes } = this.state;
        return dislikes === 0 ? 'null' : dislikes;
    }
}

export default Like;