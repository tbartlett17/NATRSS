import { calculate } from "../../SpillTracker/wwwroot/js/calculation"

test('ensuring that each calculation function is returning all array elements', () => {
    let vals = calculate(10, 10, 1, 100);

    expect(vals.length()).toBe(3);
});

test('test_that_a_total_released_is_10', () => {
    let vals = calculate(10, 10, 1, 100);

    expect(vals[0]).toBe("10");
});

test('test_that_a_function_with_same_released_asReportableWeight_has_time_till_report_as_0', () => {
    let vals = calculate(10, 10, 1, 100);

    expect(vals[2]).toBe("0");
});

