defmodule WorkoutCool.Program do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          slug_en: String.t(),
          slug_es: String.t(),
          slug_pt: String.t(),
          slug_ru: String.t(),
          slug_zh_cn: String.t(),
          title: String.t(),
          title_en: String.t(),
          title_es: String.t(),
          title_pt: String.t(),
          title_ru: String.t(),
          title_zh_cn: String.t(),
          description: String.t(),
          description_en: String.t(),
          description_es: String.t(),
          description_pt: String.t(),
          description_ru: String.t(),
          description_zh_cn: String.t(),
          category: String.t(),
          image: String.t(),
          level: atom(),
          type: atom(),
          duration_weeks: integer(),
          sessions_per_week: integer(),
          session_duration_min: integer(),
          equipment: atom(),
          is_premium: boolean(),
          is_active: boolean(),
          visibility: atom(),
          participant_count: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          weeks: [WorkoutCool.ProgramWeek.t()],
          enrollments: [WorkoutCool.UserProgramEnrollment.t()],
          coaches: [WorkoutCool.ProgramCoach.t()]
        }

  schema "programs" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:slug_en, :string, source: :slugEn)
    field(:slug_es, :string, source: :slugEs)
    field(:slug_pt, :string, source: :slugPt)
    field(:slug_ru, :string, source: :slugRu)
    field(:slug_zh_cn, :string, source: :slugZhCn)
    field(:title, :string)
    field(:title_en, :string, source: :titleEn)
    field(:title_es, :string, source: :titleEs)
    field(:title_pt, :string, source: :titlePt)
    field(:title_ru, :string, source: :titleRu)
    field(:title_zh_cn, :string, source: :titleZhCn)
    field(:description, :string)
    field(:description_en, :string, source: :descriptionEn)
    field(:description_es, :string, source: :descriptionEs)
    field(:description_pt, :string, source: :descriptionPt)
    field(:description_ru, :string, source: :descriptionRu)
    field(:description_zh_cn, :string, source: :descriptionZhCn)
    field(:category, :string)
    field(:image, :string)
    field(:level, Ecto.Enum, values: [:BEGINNER, :INTERMEDIATE, :ADVANCED, :EXPERT])
    field(:type, Ecto.Enum, values: [:BODYWEIGHT, :STRENGTH, :POWERLIFTING, :CALISTHENIC, :PLYOMETRICS, :STRETCHING, :STRONGMAN, :CARDIO, :STABILIZATION, :POWER, :RESISTANCE, :CROSSFIT, :WEIGHTLIFTING, :BICEPS, :SHOULDERS, :CHEST, :BACK, :GLUTES, :TRICEPS, :HAMSTRINGS, :QUADRICEPS, :FOREARMS, :CALVES, :TRAPS, :ABDOMINALS, :NECK, :LATS, :ADDUCTORS, :ABDUCTORS, :OBLIQUES, :GROIN, :FULL_BODY, :ROTATOR_CUFF, :HIP_FLEXOR, :ACHILLES_TENDON, :FINGERS, :DUMBBELL, :KETTLEBELLS, :BARBELL, :SMITH_MACHINE, :BODY_ONLY, :OTHER, :BANDS, :EZ_BAR, :MACHINE, :DESK, :PULLUP_BAR, :NONE, :CABLE, :MEDICINE_BALL, :SWISS_BALL, :FOAM_ROLL, :WEIGHT_PLATE, :TRX, :BOX, :ROPES, :SPIN_BIKE, :STEP, :BOSU, :TYRE, :SANDBAG, :POLE, :BENCH, :WALL, :BAR, :RACK, :CAR, :SLED, :CHAIN, :SKIERG, :ROPE, :NA, :ISOLATION, :COMPOUND])
    field(:duration_weeks, :integer, default: 4, source: :durationWeeks)
    field(:sessions_per_week, :integer, default: 3, source: :sessionsPerWeek)
    field(:session_duration_min, :integer, default: 30, source: :sessionDurationMin)
    field(:equipment, {:array, Ecto.Enum}, values: [:BODYWEIGHT, :STRENGTH, :POWERLIFTING, :CALISTHENIC, :PLYOMETRICS, :STRETCHING, :STRONGMAN, :CARDIO, :STABILIZATION, :POWER, :RESISTANCE, :CROSSFIT, :WEIGHTLIFTING, :BICEPS, :SHOULDERS, :CHEST, :BACK, :GLUTES, :TRICEPS, :HAMSTRINGS, :QUADRICEPS, :FOREARMS, :CALVES, :TRAPS, :ABDOMINALS, :NECK, :LATS, :ADDUCTORS, :ABDUCTORS, :OBLIQUES, :GROIN, :FULL_BODY, :ROTATOR_CUFF, :HIP_FLEXOR, :ACHILLES_TENDON, :FINGERS, :DUMBBELL, :KETTLEBELLS, :BARBELL, :SMITH_MACHINE, :BODY_ONLY, :OTHER, :BANDS, :EZ_BAR, :MACHINE, :DESK, :PULLUP_BAR, :NONE, :CABLE, :MEDICINE_BALL, :SWISS_BALL, :FOAM_ROLL, :WEIGHT_PLATE, :TRX, :BOX, :ROPES, :SPIN_BIKE, :STEP, :BOSU, :TYRE, :SANDBAG, :POLE, :BENCH, :WALL, :BAR, :RACK, :CAR, :SLED, :CHAIN, :SKIERG, :ROPE, :NA, :ISOLATION, :COMPOUND])
    field(:is_premium, :boolean, default: true, source: :isPremium)
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:visibility, Ecto.Enum, values: [:DRAFT, :PUBLISHED, :ARCHIVED], default: :DRAFT)
    field(:participant_count, :integer, default: 0, source: :participantCount)
    has_many(:weeks, WorkoutCool.ProgramWeek, foreign_key: :program_id, references: :id)
    has_many(:enrollments, WorkoutCool.UserProgramEnrollment, foreign_key: :program_id, references: :id)
    has_many(:coaches, WorkoutCool.ProgramCoach, foreign_key: :program_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end