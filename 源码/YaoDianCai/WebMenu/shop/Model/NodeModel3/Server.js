var localPort="@0@";
var express = require('express')
    ,http = require('http'),
    ejs = require('ejs');
var app = module.exports = express();

app.set('port', process.env.PORT || parseInt(localPort));
app.set('views', __dirname + '/views');
app.engine('.html', ejs.__express);
app.set('view engine', 'html');
app.use(express.static(__dirname + '/public'));

app.get('/index', function(req, res){
    res.render('index');
});
app.get('/main', function(req, res){
    res.render('main');
});
app.get('/table', function(req, res){
    res.render('table');
});
app.get('/tuan', function(req, res){
    res.render('tuan');
});
app.get('/order', function(req, res){
    res.render('order');
});
app.get('/call', function(req, res){
    res.render('call');
});
app.get('/impressTab', function(req, res){
    res.render('impressTab');
});
app.get('/cart', function(req, res){
    res.render('cart');
});
app.get('/dishinfo', function(req, res){
    res.render('dishinfo');
});
app.get('/flippage', function(req, res){
    res.render('flippage');
});
app.get('/video', function(req, res){
    res.render('video');
});
app.get('/music', function(req, res){
    res.render('music');
});
http.createServer(app).listen(app.get('port'), function(){
    console.log("Express server listening on port " + app.get('port'));
});