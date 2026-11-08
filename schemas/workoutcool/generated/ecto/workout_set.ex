defmodule WorkoutCool.WorkoutSet do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          set_index: integer(),
          type: atom(),
          types: atom(),
          values_int: [integer()],
          values_sec: [integer()],
          units: atom(),
          completed: boolean(),
          workout_session_exercise: WorkoutCool.WorkoutSessionExercise.t() | nil
        }

  schema "workout_sets" do
    field(:id, :string, primary_key: true)
    field(:set_index, :integer, source: :setIndex)
    field(:type, Ecto.Enum, values: [:TIME, :WEIGHT, :REPS, :BODYWEIGHT, :NA])
    field(:types, {:array, Ecto.Enum}, values: [:TIME, :WEIGHT, :REPS, :BODYWEIGHT, :NA])
    field(:values_int, {:array, :integer}, source: :valuesInt)
    field(:values_sec, {:array, :integer}, source: :valuesSec)
    field(:units, {:array, Ecto.Enum}, values: [:kg, :lbs])
    field(:completed, :boolean, default: false)
    field(:workout_session_exercise_id, :string, source: :workoutSessionExerciseId)
    belongs_to(:workout_session_exercise, WorkoutCool.WorkoutSessionExercise, foreign_key: :workout_session_exercise_id, define_field: false, type: :string)
  end
end