#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum ExerciseAttributeValueEnum
{
    [PgName("BODYWEIGHT")]
    Bodyweight,

    [PgName("STRENGTH")]
    Strength,

    [PgName("POWERLIFTING")]
    Powerlifting,

    [PgName("CALISTHENIC")]
    Calisthenic,

    [PgName("PLYOMETRICS")]
    Plyometrics,

    [PgName("STRETCHING")]
    Stretching,

    [PgName("STRONGMAN")]
    Strongman,

    [PgName("CARDIO")]
    Cardio,

    [PgName("STABILIZATION")]
    Stabilization,

    [PgName("POWER")]
    Power,

    [PgName("RESISTANCE")]
    Resistance,

    [PgName("CROSSFIT")]
    Crossfit,

    [PgName("WEIGHTLIFTING")]
    Weightlifting,

    [PgName("BICEPS")]
    Biceps,

    [PgName("SHOULDERS")]
    Shoulders,

    [PgName("CHEST")]
    Chest,

    [PgName("BACK")]
    Back,

    [PgName("GLUTES")]
    Glutes,

    [PgName("TRICEPS")]
    Triceps,

    [PgName("HAMSTRINGS")]
    Hamstrings,

    [PgName("QUADRICEPS")]
    Quadriceps,

    [PgName("FOREARMS")]
    Forearms,

    [PgName("CALVES")]
    Calves,

    [PgName("TRAPS")]
    Traps,

    [PgName("ABDOMINALS")]
    Abdominals,

    [PgName("NECK")]
    Neck,

    [PgName("LATS")]
    Lats,

    [PgName("ADDUCTORS")]
    Adductors,

    [PgName("ABDUCTORS")]
    Abductors,

    [PgName("OBLIQUES")]
    Obliques,

    [PgName("GROIN")]
    Groin,

    [PgName("FULL_BODY")]
    FullBody,

    [PgName("ROTATOR_CUFF")]
    RotatorCuff,

    [PgName("HIP_FLEXOR")]
    HipFlexor,

    [PgName("ACHILLES_TENDON")]
    AchillesTendon,

    [PgName("FINGERS")]
    Fingers,

    [PgName("DUMBBELL")]
    Dumbbell,

    [PgName("KETTLEBELLS")]
    Kettlebells,

    [PgName("BARBELL")]
    Barbell,

    [PgName("SMITH_MACHINE")]
    SmithMachine,

    [PgName("BODY_ONLY")]
    BodyOnly,

    [PgName("OTHER")]
    Other,

    [PgName("BANDS")]
    Bands,

    [PgName("EZ_BAR")]
    EzBar,

    [PgName("MACHINE")]
    Machine,

    [PgName("DESK")]
    Desk,

    [PgName("PULLUP_BAR")]
    PullupBar,

    [PgName("NONE")]
    None,

    [PgName("CABLE")]
    Cable,

    [PgName("MEDICINE_BALL")]
    MedicineBall,

    [PgName("SWISS_BALL")]
    SwissBall,

    [PgName("FOAM_ROLL")]
    FoamRoll,

    [PgName("WEIGHT_PLATE")]
    WeightPlate,

    [PgName("TRX")]
    Trx,

    [PgName("BOX")]
    Box,

    [PgName("ROPES")]
    Ropes,

    [PgName("SPIN_BIKE")]
    SpinBike,

    [PgName("STEP")]
    Step,

    [PgName("BOSU")]
    Bosu,

    [PgName("TYRE")]
    Tyre,

    [PgName("SANDBAG")]
    Sandbag,

    [PgName("POLE")]
    Pole,

    [PgName("BENCH")]
    Bench,

    [PgName("WALL")]
    Wall,

    [PgName("BAR")]
    Bar,

    [PgName("RACK")]
    Rack,

    [PgName("CAR")]
    Car,

    [PgName("SLED")]
    Sled,

    [PgName("CHAIN")]
    Chain,

    [PgName("SKIERG")]
    Skierg,

    [PgName("ROPE")]
    Rope,

    [PgName("NA")]
    Na,

    [PgName("ISOLATION")]
    Isolation,

    [PgName("COMPOUND")]
    Compound,
}
