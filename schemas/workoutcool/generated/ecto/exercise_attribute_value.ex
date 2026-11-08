defmodule WorkoutCool.ExerciseAttributeValue do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          value: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          attribute_name: WorkoutCool.ExerciseAttributeName.t() | nil,
          attributes: [WorkoutCool.ExerciseAttribute.t()]
        }

  schema "exercise_attribute_values" do
    field(:id, :string, primary_key: true)
    field(:value, Ecto.Enum, values: [:BODYWEIGHT, :STRENGTH, :POWERLIFTING, :CALISTHENIC, :PLYOMETRICS, :STRETCHING, :STRONGMAN, :CARDIO, :STABILIZATION, :POWER, :RESISTANCE, :CROSSFIT, :WEIGHTLIFTING, :BICEPS, :SHOULDERS, :CHEST, :BACK, :GLUTES, :TRICEPS, :HAMSTRINGS, :QUADRICEPS, :FOREARMS, :CALVES, :TRAPS, :ABDOMINALS, :NECK, :LATS, :ADDUCTORS, :ABDUCTORS, :OBLIQUES, :GROIN, :FULL_BODY, :ROTATOR_CUFF, :HIP_FLEXOR, :ACHILLES_TENDON, :FINGERS, :DUMBBELL, :KETTLEBELLS, :BARBELL, :SMITH_MACHINE, :BODY_ONLY, :OTHER, :BANDS, :EZ_BAR, :MACHINE, :DESK, :PULLUP_BAR, :NONE, :CABLE, :MEDICINE_BALL, :SWISS_BALL, :FOAM_ROLL, :WEIGHT_PLATE, :TRX, :BOX, :ROPES, :SPIN_BIKE, :STEP, :BOSU, :TYRE, :SANDBAG, :POLE, :BENCH, :WALL, :BAR, :RACK, :CAR, :SLED, :CHAIN, :SKIERG, :ROPE, :NA, :ISOLATION, :COMPOUND])
    field(:attribute_name_id, :string, source: :attributeNameId)
    belongs_to(:attribute_name, WorkoutCool.ExerciseAttributeName, foreign_key: :attribute_name_id, define_field: false, type: :string)
    has_many(:attributes, WorkoutCool.ExerciseAttribute, foreign_key: :attribute_value_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end