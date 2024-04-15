const { src, dest, series } = require('gulp');
const inject = require('gulp-inject');

function codemirrorBuild() {
  var target = src('./resources/codemirror/codemirror-editor.html');
  var sources = src(['./resources/**/codemirror*.js', './resources/**/codemirror*.css'], {read: true});

  return target.pipe(inject(sources))
    .pipe(dest('./resources/codemirror'));
}

exports.default = series(codemirrorBuild);
