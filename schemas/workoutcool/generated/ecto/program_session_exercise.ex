defmodule WorkoutCool.ProgramSessionExercise do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          order: integer(),
          instructions: String.t(),
          instructions_en: String.t(),
          instructions_es: String.t(),
          instructions_pt: String.t(),
          instructions_ru: String.t(),
          instructions_zh_cn: String.t(),
          session: WorkoutCool.ProgramSession.t() | nil,
          exercise: WorkoutCool.Exercise.t() | nil,
          suggested_sets: [WorkoutCool.ProgramSuggestedSet.t()]
        }

  schema "program_session_exercises" do
    field(:id, :string, primary_key: true)
    field(:order, :integer)
    field(:instructions, :string)
    field(:instructions_en, :string, source: :instructionsEn)
    field(:instructions_es, :string, source: :instructionsEs)
    field(:instructions_pt, :string, source: :instructionsPt)
    field(:instructions_ru, :string, source: :instructionsRu)
    field(:instructions_zh_cn, :string, source: :instructionsZhCn)
    field(:session_id, :string, source: :sessionId)
    field(:exercise_id, :string, source: :exerciseId)
    belongs_to(:session, WorkoutCool.ProgramSession, foreign_key: :session_id, define_field: false, type: :string)
    belongs_to(:exercise, WorkoutCool.Exercise, foreign_key: :exercise_id, define_field: false, type: :string)
    has_many(:suggested_sets, WorkoutCool.ProgramSuggestedSet, foreign_key: :program_session_exercise_id, references: :id)
  end
end