'yyyy-MM-dd'.replace(/([yMdhsm])(\1*)/g, function (match, p1, p2, offset) {
  console.log('$0: ' + match)
  console.log('p1' + p1)
  console.log('p2 ' + p2)
  console.log(offset)
})
