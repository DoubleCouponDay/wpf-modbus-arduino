FUNCTION GET_ON_TIME : TIME
  VAR_INPUT
    duty_cycle : UINT;
  END_VAR
  VAR
    milliseconds : REAL;
    rounding1 : INT;
    rounding2 : REAL;
  END_VAR

  milliseconds := 2.0 / 100.0 * UINT_TO_REAL(duty_cycle);

  (* get all individual unit values *)
  rounding1 := REAL_TO_INT(milliseconds * 100.0);
  rounding2 := INT_TO_REAL(rounding1) / 100000.0;

  (* create string of a real number 2 decimal places *)
  GET_ON_TIME := REAL_TO_TIME(rounding2);
END_FUNCTION

FUNCTION GET_OFF_TIME : TIME
  VAR_INPUT
    duty_cycle : UINT;
  END_VAR
  VAR
    milliseconds : REAL;
    rounding1 : INT;
    rounding2 : REAL;
  END_VAR

  milliseconds := 2.0 - (2.0 / 100.0 * UINT_TO_REAL(duty_cycle));

  (* get all individual unit values *)
  rounding1 := REAL_TO_INT(milliseconds * 100.0);
  rounding2 := INT_TO_REAL(rounding1) / 100000.0;

  (* create string of a real number 2 decimal places *)
  GET_OFF_TIME := REAL_TO_TIME(rounding2);
END_FUNCTION

PROGRAM motor_driver
  VAR
    led AT %QX0.3 : BOOL := true;
  END_VAR
  VAR
    led_on_timer : TON;
    led_off_timer : TOF;
    output_on_timer : TON;
    output_off_timer : TOF;
    input_angle : INT := 90;
    real_angle : REAL := 90.0;
    duty_cycle : UINT := 50;
    on_PT : TIME := T#2ms;
    off_PT : TIME := T#2ms;
  END_VAR
  VAR
    output_pin AT %QW1 : UINT := 65535;
  END_VAR

  (* Blink the builtin LED *)
  led_on_timer(IN := NOT(led_off_timer.Q), PT := T#50ms);
  led_off_timer(IN := led_on_timer.Q, PT := T#50ms);
  led := led_off_timer.Q;

  (* validate the servo angle *)
  IF(input_angle < 0) THEN
    input_angle := 0;

  ELSIF(input_angle > 180) THEN
    input_angle := 180;

  END_IF;

  (* control the servo *)
  real_angle := INT_TO_REAL(input_angle);
  duty_cycle := REAL_TO_UINT(100.0 / 180.0 * real_angle);

  on_PT := GET_ON_TIME(duty_cycle);
  off_PT := GET_OFF_TIME(duty_cycle);

  output_on_timer(IN := NOT(output_off_timer.Q), PT := on_PT);
  output_off_timer(IN := output_on_timer.Q, PT := off_PT);

  IF output_on_timer.Q THEN
    output_pin := 65535;
    
  ELSE
    output_pin := 0;
    
  END_IF;
END_PROGRAM


CONFIGURATION Config0

  RESOURCE Res0 ON PLC
    TASK task0(INTERVAL := T#1ms,PRIORITY := 0);
    PROGRAM instance0 WITH task0 : motor_driver;
  END_RESOURCE
END_CONFIGURATION
