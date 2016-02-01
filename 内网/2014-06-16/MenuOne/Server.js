/**
 * Created by Administrator on 2014/5/9.
 */
var express = require('express')
    ,http = require('http'),
    ejs = require('ejs');
var app = module.exports = express();
//定义共享环境
//app.configure(function(){
//    app.use(express.methodOverride());
//    app.use(express.bodyParser);
//    app.use(app.router);
//});
//定义开发环境
//app.configure('development',function(){
//    app.use(express.static(__dirname+'/public'));
//    app.use(express.errorHandler({dumpExceptions:true,showStack:true}));
//});
//定义生产环境
//app.configure('production',function(){
//    var oneYear = 31557600000;
//    app.use(express.static(__dirname + '/public',{maxAge:oneYear}));
//    app.use(express.errorHandler());
//});
app.set('port', process.env.PORT || 3000);
app.set('views', __dirname + '/views');
app.engine('.html', ejs.__express);
app.set('view engine', 'html');//引用模版引擎
//    app.use(express.favicon());
//    app.use(express.logger('dev'));
//    app.use(express.bodyParser());
//    app.use(express.methodOverride());
//    app.use(app.router);
app.use(express.static(__dirname + '/public'));
//app.get('/', function(req, res){
//    res.render('indexs.jade', { title: 'CSSer, 关注Web前端技术！' });
//});
app.get('/index', function(req, res){
    res.render('index');
});
app.get('/flippage', function(req, res){
    res.render('flippage');
});
app.get('/turnpage', function(req, res){
    res.render('turnpage');
});
http.createServer(app).listen(app.get('port'), function(){
    console.log("Express server listening on port " + app.get('port'));
});