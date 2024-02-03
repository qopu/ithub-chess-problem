export const formatPosition = (pos: string) => {
    const letters = ["A", "B", "C", "D", "E", "F", "G", "H"]
    const arr = pos.split("")
    return letters[+arr[1]]+(8-(+arr[0])).toString()
}