export default function delay(v, t) {
    return new Promise(function(resolve) {
        setTimeout(resolve.bind(null, v), t)
    });
}


