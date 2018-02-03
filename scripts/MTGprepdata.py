import json
from pprint import pprint

data = json.load(open('AllCards-x.json'))

with open('Cards.json', 'w') as outfile:
    for oneset in data:
        #pprint(data[oneset])
        json.dump(data[oneset], outfile)
        