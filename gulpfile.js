var gulp = require('gulp');
var inject = require('gulp-inject');

gulp.task('codemirror', function () {
  var target = gulp.src('./resources/codemirror-editor.html');
  var sources = gulp.src(['./resources/**/codemirror*.js', './resources/**/codemirror*.css'], {read: true});

  return target.pipe(inject(sources))
    .pipe(gulp.dest('./resources'));
});

