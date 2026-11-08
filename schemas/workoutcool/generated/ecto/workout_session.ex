defmodule WorkoutCool.WorkoutSession do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          started_at: DateTime.t(),
          ended_at: DateTime.t() | nil,
          duration: integer() | nil,
          muscles: atom(),
          rating: integer() | nil,
          rating_comment: String.t() | nil,
          user: WorkoutCool.User.t() | nil,
          user_session_progress: WorkoutCool.UserSessionProgress.t() | nil,
          exercises: [WorkoutCool.WorkoutSessionExercise.t()]
        }

  schema "workout_sessions" do
    field(:id, :string, primary_key: true)
    field(:started_at, WorkoutCool.PrismaDateTime, source: :startedAt)
    field(:ended_at, WorkoutCool.PrismaDateTime, source: :endedAt)
    field(:duration, :integer)
    field(:muscles, {:array, Ecto.Enum}, values: [:BODYWEIGHT, :STRENGTH, :POWERLIFTING, :CALISTHENIC, :PLYOMETRICS, :STRETCHING, :STRONGMAN, :CARDIO, :STABILIZATION, :POWER, :RESISTANCE, :CROSSFIT, :WEIGHTLIFTING, :BICEPS, :SHOULDERS, :CHEST, :BACK, :GLUTES, :TRICEPS, :HAMSTRINGS, :QUADRICEPS, :FOREARMS, :CALVES, :TRAPS, :ABDOMINALS, :NECK, :LATS, :ADDUCTORS, :ABDUCTORS, :OBLIQUES, :GROIN, :FULL_BODY, :ROTATOR_CUFF, :HIP_FLEXOR, :ACHILLES_TENDON, :FINGERS, :DUMBBELL, :KETTLEBELLS, :BARBELL, :SMITH_MACHINE, :BODY_ONLY, :OTHER, :BANDS, :EZ_BAR, :MACHINE, :DESK, :PULLUP_BAR, :NONE, :CABLE, :MEDICINE_BALL, :SWISS_BALL, :FOAM_ROLL, :WEIGHT_PLATE, :TRX, :BOX, :ROPES, :SPIN_BIKE, :STEP, :BOSU, :TYRE, :SANDBAG, :POLE, :BENCH, :WALL, :BAR, :RACK, :CAR, :SLED, :CHAIN, :SKIERG, :ROPE, :NA, :ISOLATION, :COMPOUND])
    field(:rating, :integer)
    field(:rating_comment, :string, source: :ratingComment)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, WorkoutCool.User, foreign_key: :user_id, define_field: false, type: :string)
    has_one(:user_session_progress, WorkoutCool.UserSessionProgress, foreign_key: :workout_session_id, references: :id)
    has_many(:exercises, WorkoutCool.WorkoutSessionExercise, foreign_key: :workout_session_id, references: :id)
  end
end