defmodule WorkoutCool.ProgramSession do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          session_number: integer(),
          title: String.t(),
          title_en: String.t(),
          title_es: String.t(),
          title_pt: String.t(),
          title_ru: String.t(),
          title_zh_cn: String.t(),
          slug: String.t(),
          slug_en: String.t(),
          slug_es: String.t(),
          slug_pt: String.t(),
          slug_ru: String.t(),
          slug_zh_cn: String.t(),
          description: String.t(),
          description_en: String.t(),
          description_es: String.t(),
          description_pt: String.t(),
          description_ru: String.t(),
          description_zh_cn: String.t(),
          equipment: atom(),
          estimated_minutes: integer(),
          is_premium: boolean(),
          week: WorkoutCool.ProgramWeek.t() | nil,
          exercises: [WorkoutCool.ProgramSessionExercise.t()],
          user_progress: [WorkoutCool.UserSessionProgress.t()]
        }

  schema "program_sessions" do
    field(:id, :string, primary_key: true)
    field(:session_number, :integer, source: :sessionNumber)
    field(:title, :string)
    field(:title_en, :string, source: :titleEn)
    field(:title_es, :string, source: :titleEs)
    field(:title_pt, :string, source: :titlePt)
    field(:title_ru, :string, source: :titleRu)
    field(:title_zh_cn, :string, source: :titleZhCn)
    field(:slug, :string)
    field(:slug_en, :string, source: :slugEn)
    field(:slug_es, :string, source: :slugEs)
    field(:slug_pt, :string, source: :slugPt)
    field(:slug_ru, :string, source: :slugRu)
    field(:slug_zh_cn, :string, source: :slugZhCn)
    field(:description, :string)
    field(:description_en, :string, source: :descriptionEn)
    field(:description_es, :string, source: :descriptionEs)
    field(:description_pt, :string, source: :descriptionPt)
    field(:description_ru, :string, source: :descriptionRu)
    field(:description_zh_cn, :string, source: :descriptionZhCn)
    field(:equipment, {:array, Ecto.Enum}, values: [:BODYWEIGHT, :STRENGTH, :POWERLIFTING, :CALISTHENIC, :PLYOMETRICS, :STRETCHING, :STRONGMAN, :CARDIO, :STABILIZATION, :POWER, :RESISTANCE, :CROSSFIT, :WEIGHTLIFTING, :BICEPS, :SHOULDERS, :CHEST, :BACK, :GLUTES, :TRICEPS, :HAMSTRINGS, :QUADRICEPS, :FOREARMS, :CALVES, :TRAPS, :ABDOMINALS, :NECK, :LATS, :ADDUCTORS, :ABDUCTORS, :OBLIQUES, :GROIN, :FULL_BODY, :ROTATOR_CUFF, :HIP_FLEXOR, :ACHILLES_TENDON, :FINGERS, :DUMBBELL, :KETTLEBELLS, :BARBELL, :SMITH_MACHINE, :BODY_ONLY, :OTHER, :BANDS, :EZ_BAR, :MACHINE, :DESK, :PULLUP_BAR, :NONE, :CABLE, :MEDICINE_BALL, :SWISS_BALL, :FOAM_ROLL, :WEIGHT_PLATE, :TRX, :BOX, :ROPES, :SPIN_BIKE, :STEP, :BOSU, :TYRE, :SANDBAG, :POLE, :BENCH, :WALL, :BAR, :RACK, :CAR, :SLED, :CHAIN, :SKIERG, :ROPE, :NA, :ISOLATION, :COMPOUND])
    field(:estimated_minutes, :integer, source: :estimatedMinutes)
    field(:is_premium, :boolean, default: true, source: :isPremium)
    field(:week_id, :string, source: :weekId)
    belongs_to(:week, WorkoutCool.ProgramWeek, foreign_key: :week_id, define_field: false, type: :string)
    has_many(:exercises, WorkoutCool.ProgramSessionExercise, foreign_key: :session_id, references: :id)
    has_many(:user_progress, WorkoutCool.UserSessionProgress, foreign_key: :session_id, references: :id)
  end
end