# Unbeatable-TicTacToe
An unbeatable TicTacToe game developed in Xamarin. The player may draw, but cannot win.

Some Game Theory and Reasoning behind the SmartMove Method

The original assignment was to develop a Tic-tac-toe game in Xamarin. The constraints were loose enough for individual expression. Being aware of the relatively simple strategic depth of Tic-tac-toe, my aim from the beginning was to create the most sophisticated (solved) version of the game. I wanted the machine to be unbeatable; to play in such a way as to not put itself in a potential situation for loss, while itself aiming for victory. In TTT it is assumed the player’s aim is to win, and not lose. The purpose for playing a game is to solve a problem. In TTT and similar two-player games, the players represent opposing forces to each other. This force is not static, but dynamic, meaning both sides are aiming for a win condition. This point implies the players are not simply trying to race to a win condition but must outwit each other for it. This zero-sum win-condition combined with dynamic opposition, creates an environment where the only way to solve the problem is to defeat the opponent and take an unshared win. This illustrates the following reasoning. The machine must have an aim. While there are many scenarios for a draw within TTT, the aim of the machine is not to express itself in a manner which would permit the player the opportunity for a draw where the machine cannot have the opportunity for a win. So, the machine must play to win. In TTT, there is no way to win a game without a misplay from the opponent. The machine assumes the player will not make an oversight misplay; a misplay where the machine is a single move away from victory and the player fails to notice the two in a row pattern of the machine’s game symbol, X or O, in this case O. How then does the machine aim for victory if not to hope the player fails to see a simple alignment? In TTT there are common scenarios where a player will win no matter what move the opposing player can make. The one player has two possible moves where each of those moves will satisfy a win condition, and the opponent cannot block both moves with the single move they are able to make during their turn, nor can the opponent satisfy a win condition of their own in that turn, and so the one player is guaranteed a victory on their next play. This scenario I have deemed a “win-pin”; the player has pinned their opponent for a win. The aim of the machine is to play in such a way as to both achieve and avoid win-pins. The plays leading up to a win-pin are known as “tricks”, this is because all tricks can be avoided (the machine knows how) and so rely on the player to misplay for them to execute.

Discovering the tricks: In my search for win-pin scenarios I have found a common formula which all tricks have in common. The formula begins with a single move from the player who acts first, player A. Player B then makes a move of their own. It is this second move which sets a trick in motion and seals the fate of player B. There are few tricks the player acting second can execute on the player acting first, and this is naturally due to priority in going first, which gives the disadvantage of liability for compulsion to the second player. Compulsion is when a player must make a certain move in order to block their opponent from winning on their next play; they are compelled to make this move to remain playing. A compelling move can only exist when the player has but a single option for blocking the opponent; as two or more options would mean they are subject to a win-pin, and that is a check-mate scenario. A compelling move can also only exist when the player has no move which would satisfy their own win condition; as this would not render the player optionless. If not losing is the minimal way to play the game than a compelling move is one where the player has no choice but to make that move or cease playing. It is in the third move of the formula which initiates compulsion against player B. Player A makes a third move compelling player B. Player B is now compelled into a position for the fourth move. Player A makes the fifth move, which grants them a win-pin. Player B now makes a sixth move, which blocks player A from a single win condition path. Player A makes the seventh and final move which satisfies a win condition in the other unblocked path of their win-pin. This formula is identical for every trick. The compelling move is essential to achieving a trick, as the machine assumes the player will be aware enough to always block a single win condition path, and so the only guaranteed victory is with two simultaneous win condition paths where the opponent cannot block both paths and the opponent has no option of their own to satisfy a win condition; a win-pin. In the same way the machine derives its aim based on guaranteed win scenarios, and so reduces the luck of a victory to the simple misplay of a player’s second move, the machine also hedges the risk of achieving a win-pin by using compulsion. While there are ways to achieve a win condition without the use of compulsion, doing so guarantees a victory, rather than relying on the blind luck of scenario. Relying on blind luck, the machine would have no intelligent aim for playing the game. The use of tricks solves the problem of, “what does the machine do when it is not compelled to block and there is no win condition to seize?”; the tricks form an aim for the machine. Without aim the machine would not actually be playing to win, and as winning is the reason for playing the game than the machine would not best represent another player’s drive, and so lack in providing dynamic opposition for the user. By having a guaranteed win via a win-pin as an aim, and by achieving this aim only through states of compulsion, the machine is hedged against all avoidable luck and plays optimally. In the same stroke it also is protected against all the possible ways its opponent may win against it.

Regarding expression: This version of the game being the second iteration has come about at as the result of a personal inquiry into the limits of the machines play expression. Expression is the play path options the machine may act upon. For the machine to act upon a play path, this path cannot present a liability for it to fall into a trick, nor can the machine pursue the path without an aim for a win-pin, as this is its prime directive. The question then, was how many possible win-pins exist and how many ways can they be reached, without the machine falling into liability for loss? Answering this required walking through all the play paths, for example: A: center, B: corner, … or A: center, B: middle, … Once I recognized a formula involving compulsion, this became more efficient. I would only investigate those paths which presented a compulsion option for the third move, and from there any third move permitting a win-pin state for the fifth move. Applying this method, I found a total of 13 tricks. These tricks encompass all the starting moves and secondary moves, except for those within the play paths accessible when player A’s first move is a middle space, 2, 4, 6, or 8. This is still an area of exploration, but given time restraints as well as the curiously different liabilities when starting middle, I decided to save this work for a version 3 iteration. The purposer for finding all of the win-pins is to give the machine the most varied amount of expression possibile. Ideally the machine will be able to make every possible move it can without liability for loss and with aim toward a win-pin.

Regarding balance: For the machine to avoid the liability of loss, then it must avoid falling into a trick. Given a trick begins with the second move, the machine, when acting as player B, naturally is limited in valid moves. This discovery of an optimal play style means if both player A and player B are both making optimal moves, the game will always end in a draw. This illustrates the fact that every win in TTT results in a misplay, and this is good. If a win could be achieved without a misplay, then there would be a greater unbalance between players. I write greater, because there is already an imbalance as the player A, acting first, has priority is space selection and compulsion.

On the flow: The machine has a set of priorities it must act upon. The first priority of the machine is to win, and so if there is a move satisfying a win condition, the machine must take it. The second priority of the machine is to not lose, and so if a compelling move exists against the machine then it must take it. When the machine is not determined to take a win, nor compelled to block an opponent’s win, it must act based on its own interest. If neither a win move nor compelled move were executed, then the machine makes a smart move based on the board condition. If no board conditions call for a smart move then the machine will take an available center or corner. If no available corners exist the machine will take an available middle. 

Going forward: I would appreciate any constructive criticism regarding the architecture and will consider any pushes for modification. This was a class project I became infatuated with, and so it is for fun only, but it would be neat to see if this could be written in a more optimal form.
