use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ExerciseAttributeValueEnum"
)]
pub enum ExerciseAttributeValueEnum {
    #[sea_orm(string_value = "BODYWEIGHT")]
    Bodyweight,
    #[sea_orm(string_value = "STRENGTH")]
    Strength,
    #[sea_orm(string_value = "POWERLIFTING")]
    Powerlifting,
    #[sea_orm(string_value = "CALISTHENIC")]
    Calisthenic,
    #[sea_orm(string_value = "PLYOMETRICS")]
    Plyometrics,
    #[sea_orm(string_value = "STRETCHING")]
    Stretching,
    #[sea_orm(string_value = "STRONGMAN")]
    Strongman,
    #[sea_orm(string_value = "CARDIO")]
    Cardio,
    #[sea_orm(string_value = "STABILIZATION")]
    Stabilization,
    #[sea_orm(string_value = "POWER")]
    Power,
    #[sea_orm(string_value = "RESISTANCE")]
    Resistance,
    #[sea_orm(string_value = "CROSSFIT")]
    Crossfit,
    #[sea_orm(string_value = "WEIGHTLIFTING")]
    Weightlifting,
    #[sea_orm(string_value = "BICEPS")]
    Biceps,
    #[sea_orm(string_value = "SHOULDERS")]
    Shoulders,
    #[sea_orm(string_value = "CHEST")]
    Chest,
    #[sea_orm(string_value = "BACK")]
    Back,
    #[sea_orm(string_value = "GLUTES")]
    Glutes,
    #[sea_orm(string_value = "TRICEPS")]
    Triceps,
    #[sea_orm(string_value = "HAMSTRINGS")]
    Hamstrings,
    #[sea_orm(string_value = "QUADRICEPS")]
    Quadriceps,
    #[sea_orm(string_value = "FOREARMS")]
    Forearms,
    #[sea_orm(string_value = "CALVES")]
    Calves,
    #[sea_orm(string_value = "TRAPS")]
    Traps,
    #[sea_orm(string_value = "ABDOMINALS")]
    Abdominals,
    #[sea_orm(string_value = "NECK")]
    Neck,
    #[sea_orm(string_value = "LATS")]
    Lats,
    #[sea_orm(string_value = "ADDUCTORS")]
    Adductors,
    #[sea_orm(string_value = "ABDUCTORS")]
    Abductors,
    #[sea_orm(string_value = "OBLIQUES")]
    Obliques,
    #[sea_orm(string_value = "GROIN")]
    Groin,
    #[sea_orm(string_value = "FULL_BODY")]
    FullBody,
    #[sea_orm(string_value = "ROTATOR_CUFF")]
    RotatorCuff,
    #[sea_orm(string_value = "HIP_FLEXOR")]
    HipFlexor,
    #[sea_orm(string_value = "ACHILLES_TENDON")]
    AchillesTendon,
    #[sea_orm(string_value = "FINGERS")]
    Fingers,
    #[sea_orm(string_value = "DUMBBELL")]
    Dumbbell,
    #[sea_orm(string_value = "KETTLEBELLS")]
    Kettlebells,
    #[sea_orm(string_value = "BARBELL")]
    Barbell,
    #[sea_orm(string_value = "SMITH_MACHINE")]
    SmithMachine,
    #[sea_orm(string_value = "BODY_ONLY")]
    BodyOnly,
    #[sea_orm(string_value = "OTHER")]
    Other,
    #[sea_orm(string_value = "BANDS")]
    Bands,
    #[sea_orm(string_value = "EZ_BAR")]
    EzBar,
    #[sea_orm(string_value = "MACHINE")]
    Machine,
    #[sea_orm(string_value = "DESK")]
    Desk,
    #[sea_orm(string_value = "PULLUP_BAR")]
    PullupBar,
    #[sea_orm(string_value = "NONE")]
    None,
    #[sea_orm(string_value = "CABLE")]
    Cable,
    #[sea_orm(string_value = "MEDICINE_BALL")]
    MedicineBall,
    #[sea_orm(string_value = "SWISS_BALL")]
    SwissBall,
    #[sea_orm(string_value = "FOAM_ROLL")]
    FoamRoll,
    #[sea_orm(string_value = "WEIGHT_PLATE")]
    WeightPlate,
    #[sea_orm(string_value = "TRX")]
    Trx,
    #[sea_orm(string_value = "BOX")]
    Box,
    #[sea_orm(string_value = "ROPES")]
    Ropes,
    #[sea_orm(string_value = "SPIN_BIKE")]
    SpinBike,
    #[sea_orm(string_value = "STEP")]
    Step,
    #[sea_orm(string_value = "BOSU")]
    Bosu,
    #[sea_orm(string_value = "TYRE")]
    Tyre,
    #[sea_orm(string_value = "SANDBAG")]
    Sandbag,
    #[sea_orm(string_value = "POLE")]
    Pole,
    #[sea_orm(string_value = "BENCH")]
    Bench,
    #[sea_orm(string_value = "WALL")]
    Wall,
    #[sea_orm(string_value = "BAR")]
    Bar,
    #[sea_orm(string_value = "RACK")]
    Rack,
    #[sea_orm(string_value = "CAR")]
    Car,
    #[sea_orm(string_value = "SLED")]
    Sled,
    #[sea_orm(string_value = "CHAIN")]
    Chain,
    #[sea_orm(string_value = "SKIERG")]
    Skierg,
    #[sea_orm(string_value = "ROPE")]
    Rope,
    #[sea_orm(string_value = "NA")]
    Na,
    #[sea_orm(string_value = "ISOLATION")]
    Isolation,
    #[sea_orm(string_value = "COMPOUND")]
    Compound,
}
