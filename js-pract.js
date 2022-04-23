        const isPlainObject = (element) => typeof element === 'object' && !Array.isArray(element) && element !== null
        //2 {a:1,b:2} expected result [['a',1],['b',1]]
        const obj = { a: 1, b: 2 }
        const makePairs = (obj) => {
            const arr = []
            const keys = Object.keys(obj)
            const values = Object.values(obj)
            for (let i = 0; i < keys.length; i++) {
                const temp = [keys[i], values[i]]
                arr.push(temp)
            }
            return arr
        }
        const mkPair = (obj) => Object.keys(obj).map(el => [el, obj[el]])
        const testPair = mkPair({ a: 1, b: 2 })
        console.log(testPair)
