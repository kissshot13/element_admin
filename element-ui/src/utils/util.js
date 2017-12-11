var SIGN_REGEXP = /([yMdhsm](\1*))/g

var DEFAULT_PATTERN = 'yyyy-MM-dd'

function padding (s, len) {
  len = len - (s + '').length
  for (let i = 0; i < len; i++) {
    s = '0' + s
  }
  return s
}

export default {
  getQueryStringByName: function (name) {
    // (^|&): 匹配空或者&
    // （[^&]*）:非&贪婪
    var reg = new RegExp('(^|&) ' + name + '=([^&]*)(&|$)', 'i')
    var r = window.location.search.substr(1).match(reg)
    var context = ''
    if (r != null) {
      context = r[2]
    }
    reg = null
    r = null
    return context === null || context === '' || context === 'undefined' ? '' : context
  },
  formatDate: {
    // date:string,pattern:输出形式
    // date() => string
    format: function (date, pattern) {
      pattern = pattern || DEFAULT_PATTERN
      return pattern.replace(SIGN_REGEXP, function ($0) {
        switch ($0.charAt(0)) {
          case 'y': return padding(date.getFullYear(), $0.length)
          case 'M': return padding(date.getMonth() + 1, $0.length)
          case 'd': return padding(date.getDate(), $0.length)
          case 'w': return date.getDay() + 1
          case 'h': return padding(date.getHours(), $0.length)
          case 'm': return padding(date.getMinutes(), $0.length)
          case 's': return padding(date.getSeconds(), $0.length)
        }
      })
    },
    // string => date()
    parse: function (dateString, pattern) {
      var matches1 = pattern.match(SIGN_REGEXP)
      var matches2 = dateString.match(/(\d+)/g)
      if (matches1.length === matches2.length) {
        var _date = new Date(1970, 0, 1)
        for (let i = 0; i < matches1.length; i++) {
          var _int = parseInt(matches2[i])
          var sign = matches1[1]
          switch (sign.charAt(0)) {
            case 'y':
              _date.setFullYear(_int)
              break
            case 'M':
              _date.setMonth(_int - 1)
              break
            case 'd':
              _date.setDate(_int)
              break
            case 'h':
              _date.setHours(_int)
              break
            case 'm':
              _date.setMinutes(_int)
              break
            case 's':
              _date.setSeconds(_int)
              break
          }
        }
        return _date
      }
      return null
    }
  }
}
