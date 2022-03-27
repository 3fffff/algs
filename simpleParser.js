var request = require("request")
var fs = require("fs");
var pages = 5
var find = "eustoma"
function Request(page) {
    return new Promise((resolve, reject) => {
        request.get({
            uri: "https://stock.adobe.com/Ajax/Search?filters[content_type:photo]=1&filters[content_type:illustration]=1&filters[content_type:zip_vector]=1&filters[content_type:video]=0&filters[content_type:template]=0&filters[content_type:3d]=0&filters[include_stock_enterprise]=0&filters[is_editorial]=0&filters[content_type:image]=1&k=" + find + "&order=relevance&safe_search=1&search_page=" + page + "&get_facets=0&limit=all",
            method: 'GET',
        },
            function (err, res, body) {
                try {
                    const json = JSON.parse(body)
                    resolve(json);
                } catch (e) {
                    reject(e);
                }
            }
        );
    });

}
async function makeRequest(page) {
    try {
        let response_body = await Request(page);
        const items = response_body["items"]
        Object.entries(items).forEach(([key, value]) => {
            console.log(`${key}: ${value["thumbnail_url"]}`)
            doRequest(value["thumbnail_url"])
        });
    }
    catch (error) {
        console.log(error);
    }
}

function doRequest(url) {
    request.get({
        uri: url,
        method: 'GET',
        encoding: 'binary'
    },
        function (err, res, body) {
            fs.writeFile("images/" + 100 * Math.random() + ".jpg", body, 'binary', function (err) { })
        }
    );
}

var asyncPages = {
    [Symbol.asyncIterator]() {
        return {
            i: 0,
            next() {
                if (this.i < pages) {
                    return Promise.resolve({ value: makeRequest(this.i++), done: false });
                }
                return Promise.resolve({ done: true });
            }
        };
    }
};

(async function () {
    for await (let num of asyncPages) {
        console.log(num);
    }
})();
