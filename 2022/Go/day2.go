package main

import (
	"fmt"
	"strings"
)

func DayTwoPartOne() {
	input := readInput("day2.txt")

	om := make(map[string]string)
	pm := make(map[string]string)

	om["A"] = "Rock"
	om["B"] = "Paper"
	om["C"] = "Scissors"
	pm["X"] = om["A"]
	pm["Y"] = om["B"]
	pm["Z"] = om["C"]

	w := make(map[string]string)

	w["Rock"] = "Scissors"
	w["Paper"] = "Rock"
	w["Scissors"] = "Paper"

	ms := make(map[string]int)

	ms["Rock"] = 1
	ms["Paper"] = 2
	ms["Scissors"] = 3

	var score int
	for _, line := range input {
		split := strings.Split(line, " ")
		o := om[split[0]]
		p := pm[split[1]]

		// We lose
		if w[o] == p {
			score += ms[p]
			continue
		}

		// We win
		if w[p] == o {
			score += ms[p]
			score += 6
			continue
		}

		// Draw
		score += ms[p]
		score += 3
	}

	fmt.Println(score)
}
