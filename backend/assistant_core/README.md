# Assistnat core module with Rasa

## Install

Install miniconda from with the [installers](https://docs.conda.io/en/latest/miniconda.html).

RUN `conda env create -n capa python=3.8`

RUN `conda activate capa`

RUN `pip install rasa`
If this doesn't work out of the box, help can be:
RUN `pip install rasa --extra-index-url https://pypi.rasa.com/simple`
or <https://forum.rasa.com/t/rasa-x-install-stucked/39640>


## rasa train

TODO

## rasa run

TODO

## Assistant on the command line

Talk to the trained assistant on the command line.

RUN `conda activate capa`

RUN `rasa shell --model <path-to-trained-model>`
