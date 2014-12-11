using UnityEngine;
using System.Collections;

public class ValueConstants : MonoBehaviour 
{
    public const float WORK_TIME = 5.0f;

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

    void Start()
    {
        WENCH_FLIRT_WENCH_LEVEL = new AnimationCurve(new Keyframe(0.0f, 0.0f, 0.1847229f, 0.1847229f),
                                                    new Keyframe(0.8054937f, 0.5025045f, 1.007495f, 1.007495f),
                                                    new Keyframe(0.8084006f, 0.9632897f, 0.3790588f, 0.3790588f),
                                                    new Keyframe(1.0f, 1.0f, 0.0f, 0.0f));
    }
}
