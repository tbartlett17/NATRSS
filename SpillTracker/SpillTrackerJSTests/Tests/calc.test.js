import { calculate } from "../../SpillTracker/wwwroot/js/calculate"

test('ensuring that each calculation function is returning all array elements', () => {
    let vals = calculate(10, 10, 1, 100);

    expect(vals.length).toBe(3);
});

test('test_that_a_total_released_is_10', () => {
    let vals = calculate(10, 10, 1, 100);

    expect(vals[0]).toBe("10.00");
});

test('test_that_a_function_with_same_released_asReportableWeight_has_time_till_report_as_0', () => {
    let vals = calculate(10, 10, 1, 100);

    expect(vals[2]).toBe(0);
});

test('test_that_release_per_hour_of_a_100lb_spill_in_4_hours_to_be_25lbs_per/hour', () => {
    let vals = calculate(10, 100, 4, 100);

    expect(vals[1]).toBe("25.00");
});

test('test_that_release_per_hour_of_a_100lb_spill_in_3_hours_to_be_33.33lbs_per/hour', () => {
    let vals = calculate(10, 100, 3, 100);

    expect(vals[1]).toBe("33.33");
});