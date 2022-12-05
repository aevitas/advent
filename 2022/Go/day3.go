package main

import "fmt"

func DayThreePartOne() {
	input := readInput("day2.txt")

	var found []rune
	for _, line := range input {
		left := line[:len(line)/2]
		right := line[len(line)/2:]

		var a []rune
		var b []rune
		for _, c := range left {
			a = append(a, c)
		}

		for _, c := range right {
			b = append(b, c)
		}

		for _, ca := range a {
			for _, cb := range b {
				if cb == ca {
					found = append(found, cb)
				}
			}
		}

		for _, cb := range b {
			for _, ca := range a {
				if ca == cb {
					found = append(found, ca)
				}
			}
		}
	}

	for _, c := range found {
		fmt.Println(c)
	}
}
