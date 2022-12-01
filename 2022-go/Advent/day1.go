package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
)

func main() {
	dayOne()
}

func dayOne() {
	input := readInput("day1.txt")

	var elves []int
	var cur int
	for _, l := range input {
		if l == "" {
			elves = append(elves, cur)
			cur = 0
			continue
		}

		i, err := strconv.Atoi(l)

		if err != nil {
			log.Fatal(err)
		}

		cur += i
	}

	fmt.Println(max(elves))
}

func readInput(file string) []string {
	content, err := os.Open(file)

	if err != nil {
		log.Fatal(err)
	}

	scanner := bufio.NewScanner(content)

	var lines []string
	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}

	return lines
}

func max(input []int) int {
	cur := 0

	for _, val := range input {
		if val > cur {
			cur = val
		}
	}

	return cur
}
