using UnityEngine;
using System.Collections;

public class ValueConstants : MonoBehaviour 
{
    public const float WORK_TIME = 5.0f;

	public const float CLOCK_DEGREES_AND_OFFSET = 180.0f;

    public const float PLAYER_MAX_TIME = 45.0f;
    public const float PLAYER_MAX_HUNGER = 100.0f;
    public const float PLAYER_HUNGER_PENALTY_LEVEL = 75.0f;
    public const float PLAYER_MAX_FOOD_PENALTY = 5.0f;
    public const float PLAYER_DEFAULT_SPEED = 10.0f;
    public const int PLAYER_DEFAULT_MONEY_SCALAR = 1;

    public const int WENCH_FLIRT_BASE_SUCCESS = 10;
    public const int WENCH_FLIRT_BASE_REPUTATION_PIVOT = 100;
    public const int WENCH_FLIRT_SUCCESS_ADJUST = 10;
    public const int WENCH_FLIRT_ONE_HUNDRED_PERCENT = 100;
    public const int WENCH_FLIRT_MAX_REPUTATION = 10000;
    public static AnimationCurve WENCH_FLIRT_WENCH_LEVEL = null;

    public const int CURE_PLAGUE_COST = 2000;

    public const int CHAPEL_DONATION_COST = 100;
    public const int CHAPEL_REPUTATION_GAIN = 50;

    public const int UNIVERSITY_TUITION_SCHILLING_COST = 25;
    public const float UNIVERSITY_TUITION_TIME_COST = 5.0f;
    public const int UNIVERSITY_TUITION_SKILL_GAIN = 25;

    public const float ZERO_STAR_HABITAT_PENALTY = 10.0F;
    public const float ONE_STAR_HABITAT_PENALTY = 8.0f;
    public const float TWO_STAR_HABITAT_PENALTY = 6.0f;
    public const float THREE_STAR_HABITAT_PENALTY = 4.0f;
    public const float FOUR_STAR_HABITAT_PENALTY = 2.0f;
    public const float FIVE_STAR_HABITAT_PENALTY = 0.0f;

	//Random Event Constant Values:::
	//Defualt Lowest Range Number
	public const int LOWEST_RANGE_NUMBER = 1;

	//Position Change
	public const float XPOS_CHANGE_MIN = -15.0f;
	public const float ZPOS_CHANGE_MIN = -15.0f;
	public const float XPOS_CHANGE_MAX = 26.0f;
	public const float ZPOS_CHANGE_MAX = 26.0f;

	//Time Change
	public const float TIME_CHANGE_FROM_HANGOVER = -15.0f;
	public const float TIME_CHANGE_FROM_RAT_BITES = -10.0f;
	public const float TIME_CHANGE_FROM_EXECUTION = -5.0f;
	public const float TIME_CHANGE_FROM_WITCH_BURNING = 10.0f;
	public const float TIME_CHANGE_FROM_STAMPEDE = -10.0f;

	//EXP Change
	public const int EXPERIENCE_FROM_WEEKEND_JOB_MIN = 5;
	public const int EXPERIENCE_FROM_WEEKEND_JOB_MAX = 25;

	//Money Change
	public const int DEFAULT_CIRCUS_TICKET_COST = 100;
	public const int TOURNAMENT_COST_MAX = 150;
	public const int TOURNAMENT_COST_MIN = 50;
	public const int MONEY_LOST_FROM_FIRE = 500;
	public const int MONEY_GIVEN_TO_STARVING_CHILD = 50;
	public const float EARNING_SCALAR_CHANGE_FOR_BARRACK_KAW = 2.0f;
	public const float EARNING_SCALAR_CHANGE_FOR_BUILDINGS_KAW = 0.5f;
	public const float EARNING_SCALAR_CHANGE_FOR_BARRACK_KAW_ENDS = 1.0f;
	public const float EARNING_SCALAR_CHANGE_FOR_BUILDINGS_KAW_ENDS = 1.0f;

	//Money Range values
	public const int MIN_SHILLINGS_FOUND = 5;
	public const int MAX_SHILLINGS_FOUND = 1000;
	public const int MIN_SHILLINGS_ROBBER_TAKES = 5;
	public const int MAX_SHILLINGS_ROBBER_TAKES = 500;

	//Happiness Ranges
	public const int WITCH_BURNING_HAPPINESS_HAPPY_CHANGE_MIN = 5;
	public const int WITCH_BURNING_HAPPINESS_HAPPY_CHANGE_MAX = 20;

	//Days until Event Expiry
	public const int HANGOVER_DAY = 1;
	public const int MIN_DAYS_UNTIL_WAR_ENDS = 1;
	public const int MAX_DAYS_UNTIL_WAR_ENDS = 6;

	//Values to check when each message is displayed
	public const int EXECUTION_MESSAGE_HAPPY = 5;
	public const int EXECUTION_MESSAGE_ALRIGHT = 0;
	public const int BURNING_MESSAGE_HAPPY = 15;
	public const int BURNING_MESSAGE_ALRIGHT = 5;

	//Random Event Range Values:::
	public const int RANDOM_EVENT_HAPPENS_AT_LESS_THAN_THIS = 51;
	public const int RANDOM_EVENT_CHANCE_MAX = 101;
	public const int D100_MAX = 101;

	public const int MAX_RANDOM_CHANCE_TO_FIND_SHILLINGS = 6;
	public const int MAX_RANDOM_CHANCE_TO_BE_BITTEN_BITTEN_BY_RATS = 11;
	public const int MAX_RANDOM_CHANCE_TO_FIND_A_SACK = 21;
	public const int MAX_RANDOM_CHANCE_TO_BE_ROBBED = 26;
	public const int MAX_RANDOM_CHANCE_FOR_A_TOURNAMENT = 31;
	public const int MAX_RANDOM_CHANCE_FOR_A_WEEKEND_JOB = 41;
	public const int MAX_RANDOM_CHANCE_FOR_KINGDOM_AT_WAR = 46;
	public const int MAX_RANDOM_CHANCE_FOR_A_STAMPEDE = 51;
	public const int MAX_RANDOM_CHANCE_TO_FIND_STARVING_CHILD = 56;
	public const int MAX_RANDOM_CHANCE_FOR_THE_PLAGUE = 61;
	public const int MAX_RANDOM_CHANCE_FOR_FOOT_AND_MOUTH_DISEASE = 66;
	public const int MAX_RANDOM_CHANCE_FOR_EXPERIMENT_GONE_WRONG = 71;
	public const int MAX_RANDOM_CHANCE_FOR_THE_CIRCUS = 76;
	public const int MAX_RANDOM_CHANCE_TO_WITNESS_AN_EXECUTION = 86;
	public const int MAX_RANDOM_CHANCE_TO_WITNESS_A_WITCH_BURNING = 91;
	public const int MAX_RANDOM_CHANCE_FOR_A_FIRE = 96;
	public const int MAX_RANDOM_CHANCE_FOR_BARREL_OF_MEAD = 100;
	
	//Found Sack Random Values
	public const int ELIXIR_INDEX = 0;
	public const int FOOD_INDEX = 1;
	public const int DRINK_INDEX = 2;
	public const int REPUTATION_INDEX = 3;



    void Start()
    {
        WENCH_FLIRT_WENCH_LEVEL = new AnimationCurve(new Keyframe(0.0f, 0.0f, 0.1847229f, 0.1847229f),
                                                    new Keyframe(0.8054937f, 0.5025045f, 1.007495f, 1.007495f),
                                                    new Keyframe(0.8084006f, 0.9632897f, 0.3790588f, 0.3790588f),
                                                    new Keyframe(1.0f, 1.0f, 0.0f, 0.0f));
    }
}
