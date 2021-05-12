Feature: Calculator
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](SpillTrackerBDDTests/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

Scenario Outline: Calculator can divide two numbers
	Given the first number is <leftOp>
	  And the second number is <rightOp>
	When the two numbers are divided
	Then the result should be <answer>
	Examples: 
		| leftOp | rightOp | answer |
		| 10     | 5       | 2      |
		| 100    | 5       | 20     |
		| 15     | 3       | 5      |
		| 25     | 5       | 5      |
		| 40     | 2       | 20     |
