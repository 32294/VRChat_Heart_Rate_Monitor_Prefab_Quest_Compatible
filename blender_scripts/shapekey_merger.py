import bpy

#& config vars
#~ set the max number the display will hold
max_display_number = 255
#~ whether digits should be centered
center_digits = True

#& Get the active object
obj = bpy.context.object

#& Check if the object has shape keys
if not obj.data.shape_keys:
    raise Exception("Object has no shape keys")

#& Get the shape key block
key_blocks = obj.data.shape_keys.key_blocks

#& loop through display numbers
for display_number in range(max_display_number + 1):
    
    #~ determine the list of shape keys we need to use 
    #? determine number of needed digits
    #^ if less than 10 then 1 digit
    if display_number < 10:
        
        #* get shape keys based on if center digits is set or not
        if center_digits:
            shape_keys = ["2_" + str(display_number) + "_on"]
        else:
            shape_keys = ["1_" + str(display_number) + "_on"]

    #^ if between 10 and 99 then 2 digits
    elif display_number < 100:
    
        #* make the number a string so it can be split
        display_number_str = str(display_number)

        #* get shape keys based on if center digits is set or not
        shape_keys = ["2_" + display_number_str[0] + "_on", "1_" + display_number_str[1] + "_on"]
        if center_digits:
            shape_keys.append("2digit")
        
    #^ if over 100 then assume it's 3 digits for now... this will be fun if i need a commically high number display in the future....
    else:

        #* make the number a string so it can be split
        display_number_str = str(display_number)

        #* get shape keys
        shape_keys = ["3_" + display_number_str[0] + "_on", "2_" + display_number_str[1] + "_on", "1_" + display_number_str[2] + "_on"]

    print(shape_keys)

    #~ create the new shape key
    #? set all shape keys to value 0
    for key in key_blocks:
        key.value = 0.0

    #? set shape keys from the selected list to max value
    for name in shape_keys:
        key_blocks[name].value = 1.0

    #? create a new shape key that is a mix of the selected shape keys
    bpy.ops.object.shape_key_add(from_mix=True)
    key_blocks[-1].name = str(display_number)