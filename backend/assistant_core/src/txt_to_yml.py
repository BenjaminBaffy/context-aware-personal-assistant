############################################################################################
############################# yml creator for names ########################################
############################################################################################

# # Insert normal .txt/.csv path here
# file = open("names.txt", 'r')

# # Splits each row
# lines = file.read().splitlines()
# n = 0

# # Insert target output path here
# file2 = open("../data/names.yml", "w")

# # Writes the headers(?), remember to change the lookup name
# file2.write("version: \"2.0\"\nnlu:\n  - lookup: name  \n    examples: |\n")

# # Adds indent and dash to each line
# for line in lines:
#     file2.write("      - " + str(line) + "\n")

# # Closes the files
# file.close()
# file2.close()

############################################################################################
################################ yml creator for cities ####################################
############################################################################################

# Insert normal .txt/.csv path here
file = open("cities.txt", 'r')

# Splits each row
lines = file.read().splitlines()
n = 0

# Insert target output path here
file2 = open("../data/city.yml", "w")

# Writes the headers(?), remember to change the lookup name
file2.write("version: \"2.0\"\nnlu:\n  - lookup: city  \n    examples: |\n")

# Adds indent and dash to each line
for line in lines:
    file2.write("      - " + str(line) + "\n")

# Closes the files
file.close()
file2.close()