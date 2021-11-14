# Assistnat core module with Rasa

## Install

### Windows/Linux

Install miniconda from with the [installers](https://docs.conda.io/en/latest/miniconda.html).

RUN `conda env create -n capa python=3.8`

RUN `conda activate capa`

RUN `pip install rasa`
If this doesn't work out of the box, help can be:
RUN `pip install rasa --extra-index-url https://pypi.rasa.com/simple`
or <https://forum.rasa.com/t/rasa-x-install-stucked/39640>

### macOS 12+

install `miniconda` with Homebrew

`$ brew install --cask miniconda`

create virtual python environment

`$ conda create -n capa python=3.8`

if it's your first time with conda, initialize your shell:

`$ conda init <YOUR-SHELL-NAME>` (i.e.: `$ conda init zsh`)

activate venv

`conda activate capa`

maybe you don't want to auto-enable the base conda venv

`$ conda config --set auto_activate_base false`


## rasa train

TODO

## rasa run

TODO

## Assistant on the command line

Talk to the trained assistant on the command line.

RUN `conda activate capa`

RUN `rasa shell --model <path-to-trained-model>`
