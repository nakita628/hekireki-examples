defmodule WorkoutCool.ProgramSuggestedSet do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          set_index: integer(),
          types: atom(),
          values_int: [integer()],
          values_sec: [integer()],
          units: atom(),
          program_session_exercise: WorkoutCool.ProgramSessionExercise.t() | nil
        }

  schema "program_suggested_sets" do
    field(:id, :string, primary_key: true)
    field(:set_index, :integer, source: :setIndex)
    field(:types, {:array, Ecto.Enum}, values: [:TIME, :WEIGHT, :REPS, :BODYWEIGHT, :NA])
    field(:values_int, {:array, :integer}, source: :valuesInt)
    field(:values_sec, {:array, :integer}, source: :valuesSec)
    field(:units, {:array, Ecto.Enum}, values: [:kg, :lbs])
    field(:program_session_exercise_id, :string, source: :programSessionExerciseId)
    belongs_to(:program_session_exercise, WorkoutCool.ProgramSessionExercise, foreign_key: :program_session_exercise_id, define_field: false, type: :string)
  end
end