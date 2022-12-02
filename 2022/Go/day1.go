package main

import (
	"fmt"
	"sort"
	"strconv"
)

func DayOnePartOne() {
	elves := getCaloriesCarried()

	fmt.Println(max(elves))
}

func DayOnePartTwo() {
	elves := getCaloriesCarried()

	top := maxThree(elves)
	var total int

	for _, i := range top {
		total += i
	}

	fmt.Println(total)
}

func getCaloriesCarried() []int {
	input := readInput("day1.txt")

	var elves []int
	var cur int
	for _, l := range input {
		if l == "" {
			elves = append(elves, cur)
			cur = 0
			continue
		}

		i, _ := strconv.Atoi(l)

		cur += i
	}

	return elves
}

func max(input []int) int {
	sort.Ints(input)

	return input[len(input)-1]
}

func maxThree(input []int) []int {
	sort.Sort(sort.Reverse(sort.IntSlice(input[:])))

	return input[:3]
}
