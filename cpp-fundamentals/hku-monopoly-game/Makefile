FLAGS = -pedantic-errors -std=c++11

player.o: player.cpp player.h
	g++ $(FLAGS) -c $<

property.o: property.cpp property.h
	g++ $(FLAGS) -c $<

print.o: print.cpp print.h player.h
	g++ $(FLAGS) -c $<

game.o: game.cpp game.h player.h print.h property.h
	g++ $(FLAGS) -c $<

main.o: main.cpp game.h
	g++ $(flags) -c $<

play: main.o game.o print.o property.o player.o
	g++ $(FLAGS) $^ -o $@

clean:
	rm -rf *.o play 

tar:
	tar -czvf play.tgz *.cpp *.h

.PHONY: clean tar
