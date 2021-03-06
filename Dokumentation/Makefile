# %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
# %% Descr:       Vorlage für Berichte der DHBW-Karlsruhe,Makefile# %% Author:      Prof. Dr. Jürgen Vollmer, vollmer@dhbw-karlsruhe.de
# %% $Id: Makefile,v 1.12 2010/11/11 14:51:22 vollmer Exp $
# %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

# Dateiname der Haupt-Datei
BASE  = Projektbericht

LATEX  = pdflatex -file-line-error
BIBTEX = bibtex

# Wo werden die diversen Dateien gesucht:
# Ein // am Ende bedeutet: in allen Unterverzeichnissen
# BIBTEX-Style Files (*.bst)
export BSTINPUTS := .//:$(SRC_PATH)//:$(HOME)/tex/texmf/tex//:/usr/share/texmf//:$(BSTINPUTS)

# BIBTEX Datenbank(en) (*.bib)
export BIBINPUTS := .//:$(SRC_PATH)//:$(HOME)/tex/bib//:$(BIBINPUTS)

# LaTeX Styles und Klassen (*.sty, *.cls)
export TEXINPUTS := .//:$(SRC_PATH)//:$(HOME)/tex/texmf/tex//:/usr/share/texmf//:$(TEXINPUTS)

all: $(BASE).pdf

$(BASE).pdf: *.tex *.bib Makefile.
	$(LATEX)  $(BASE)
	$(BIBTEX) $(BASE)
	$(LATEX)  $(BASE)
	$(LATEX)  $(BASE)

# Die Datei "Makefile." muss unter Linux existieren, da ProTexT (Windows)
# einen Bug hat, der verhindert, dass eine Datei ohne einen Punkt mittels
# des listings-Paketets inkludiert werden kann. (Siehe "kapitel2.tex")
Makefile. : Makefile
	ln -s Makefile Makefile.

# Hilfsdateien löschen
clean:
	rm -f *.toc *.dvi *.aux *.log *.blg *.bbl *.out *.for \
	      *.lof *.lol *.lot				      \
	      *~

# Wirklich alles löschen
realclean: clean
	rm -f $(BASE).pdf

# Erstelle tar-Archiv des ganzen Verzeichnisses
tar: $(BASE).pdf
	$(MAKE) clean
	D=`pwd`; D=`basename $$D`;		\
	cd ..; 					\
	tar --exclude "*.tar.gz" --exclude RCS  \
	    --exclude PraxisDeckblatt.doc 	\
	    --exclude "*.jar"			\
	    --dereference			\
	    -czvf $$D/Studienarbeit_Nao-`date "+%Y-%m-%d"`.tar.gz $$D
