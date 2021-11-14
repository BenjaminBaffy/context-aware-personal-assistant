# Assistant core module with Rasa

## Setup virtual environment

### Windows/Linux

Install miniconda from with the [installers](https://docs.conda.io/en/latest/miniconda.html).

RUN `conda env create -n capa python=3.8`

RUN `conda activate capa`


### macOS 12+

Install `miniconda` with Homebrew

`$ brew install --cask miniconda`

Create virtual python environment

`$ conda create -n capa python=3.8`

If it's your first time with conda, initialize your shell:

`$ conda init <YOUR-SHELL-NAME>` (i.e.: `$ conda init zsh`)

Activate venv

`conda activate capa`

\+ Maybe you don't want to auto-enable the base conda venv

`$ conda config --set auto_activate_base false`


## Install

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
